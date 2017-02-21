//
// MSG/Types/Dir/Dir.cs
//

using System;
using System.Collections;
using System.Collections.Generic;

namespace MSG.Types.Dir
{
    /// <summary>
    /// A node can simultaneously hold a data item and
    /// another dir.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Node<T>
    {
        public T item;
        public Dir<T> subdir;

        public Node(T item, Dir<T> subdir = null)
        {
            this.item = item;
            this.subdir = subdir;
        }
    }

    /// <summary>
    /// A Dir is a list whose nodes can hold both a data item
    /// and another dir (e.g. task and its subtasks).
    /// </summary>
    public class Dir<T> : IEnumerable
    {
        protected List<Node<T>> items;
        protected Dir<T> parent;

        #region Enumerator

        public class Enumerator : IEnumerator
        {
            protected List<Node<T>> items;
            protected List<Node<T>>.Enumerator iterator;

            public Enumerator(List<Node<T>> items)
            {
                this.items = items;
                this.iterator = items.GetEnumerator();
            }

            ~Enumerator()
            {
                iterator.Dispose();
            }

            object IEnumerator.Current {
                get {
                    return iterator.Current.item;
                }
            }

            bool IEnumerator.MoveNext()
            {
                return iterator.MoveNext();
            }

            void IEnumerator.Reset()
            {
                iterator.Dispose();
                this.iterator = items.GetEnumerator();
            }
        }

        #endregion

        public Dir()
        {
            this.items = new List<Node<T>>();
            this.parent = null;
        }

        /// <summary>
        /// Dir[i] access
        /// </summary>
        /// <param name="index">0-based item number</param>
        /// <returns>The item</returns>
        public virtual T this[int index] {
            // Get item by position
            get {
                return items[index].item;
            }
            // Replace item with given index
            set {
                items[index].item = value;
            }
        }

        /// <summary>
        /// Adds an item to the end of the parent dir.
        /// </summary>
        /// <param name="item"></param>
        public virtual void Add(T item)
        {
            if (ItemExists(item)) {
                throw new InvalidOperationException("Cannot add same item twice");
            }
            Node<T> node = new Node<T>(item);
            items.Add(node);
        }

        /// <summary>
        /// The number of nodes in this dir
        /// </summary>
        public int Count {
            get { return items.Count; }
        }

        /// <summary>
        /// Dir enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return new Enumerator(items);
        }

        /// <summary>
        /// Returns a item's subdir.
        /// </summary>
        public virtual Dir<T> GetSubdir(int index)
        {
            return items[index].subdir;
        }

        /// <summary>
        /// True if a subdir exists at the given index
        /// </summary>
        public bool HasSubdirAt(int index)
        {
            return items[index].subdir != null;
        }

        /// <summary>
        /// Inserts a new item into the dir.
        /// The zero-based index determines where it is to be inserted.
        /// </summary>
        /// <param name="index">
        /// 0-based index in the list to add.
        /// If index points past end of list, adds item to the end.
        /// </param>
        /// <param name="item">
        /// Item to add.
        /// </param>
        public virtual void Insert(int index, T item)
        {
            if (ItemExists(item)) {
                throw new InvalidOperationException("Cannot add same item twice");
            }
            Node<T> node = new Node<T>(item);
            // If index points past end of list, add item to end.
            if (index > items.Count) {
                index = items.Count;
            }
            items.Insert(index, node);
        }

        /// <summary>
        /// True if the reference is somewhere in the dir
        /// </summary>
        public virtual bool ItemExists(T item)
        {
            return items.Find(i => i.item.Equals(item)) != null;
        }

        /// <summary>
        /// True if an item exists at the given index
        /// </summary>
        public virtual bool ItemExistsAt(int index)
        {
            return index >= 0 && index < items.Count;
        }

        /// <summary>
        /// Moves an item to another slot in the same dir
        /// </summary>
        public virtual void Move(int srcIndex, int destIndex)
        {
            Node<T> item = items[srcIndex];
            items.RemoveAt(srcIndex);
            items.Insert(destIndex, item);
        }

        public Dir<T> Parent {
            get {
                return parent;
            }
            set {
                this.parent = value;
            }
        }

        /// <summary>
        /// Deletes an item by reference from the dir
        /// </summary>
        public virtual void Remove(T item)
        {
            if (item == null) {
                throw new NullReferenceException("<item> cannot be null in call to Remove(T item)");
            }
            if (items.Remove(items.Find(i => i.item.Equals(item))) == false) {
                throw new ApplicationException("Item \"" + item.ToString() + "\" could not be removed");
            }
        }

        /// <summary>
        /// Deletes an item by index from the dir
        /// </summary>
        public virtual void RemoveAt(int index)
        {
            if (!(index >= 0 && index < items.Count)) {
                throw new InvalidOperationException("Cannot remove nonexistent item");
            }
            items.RemoveAt(index);
        }

        /// <summary>
        /// Assigns a subdir to an item and assigns its
        /// parent to the current dir.
        /// </summary>
        public virtual void SetSubdir(int index, Dir<T> subdir)
        {
            items[index].subdir = subdir;
            subdir.Parent = this;
        }
    }
}
