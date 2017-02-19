//
// MSG/Types/Tree/Tree.cs
//

using System;
using System.Collections;
using System.Collections.Generic;

namespace MSG.Types.Tree
{
    public class TreeNode<T>
    {
        public T item;
        public Tree<T> subtree;

        public TreeNode(T item, Tree<T> subtree = null)
        {
            this.item = item;
            this.subtree = subtree;
        }
    }

    /// <summary>
    /// A list of nodes which are data nodes
    /// </summary>
    public class Tree<T> : IEnumerable
    {
        public List<TreeNode<T>> items;

        public Tree()
        {
            // Create an initial, parent, list of T.
            this.items = new List<TreeNode<T>>();
        }

        /// <summary>
        /// Tree[i] access
        /// </summary>
        /// <param name="index">0-based item number</param>
        /// <returns></returns>
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
        /// Adds an item to the end of the parent branch.
        /// </summary>
        /// <param name="item"></param>
        public virtual void Add(T item)
        {
            TreeNode<T> node = new TreeNode<T>(item);
            items.Add(node);
        }

        /// <summary>
        /// Returns a item's subtree.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual Tree<T> GetSublist(int index)
        {
            return items[index].subtree;
        }

        /// <summary>
        /// Inserts a new item into the parent branch of the tree.
        /// The zero-based index determines where it is to be inserted.
        /// </summary>
        /// <param name="item">
        /// Item to add.
        /// </param>
        /// <param name="index">
        /// 0-based index in the list to add.
        /// </param>
        public virtual void Insert(T item, int position)
        {
            if (ItemExists(item)) {
                throw new InvalidOperationException("Cannot add same item twice");
            }
            TreeNode<T> node = new TreeNode<T>(item);
            items.Insert(position, node);
        }

        public int Count
        {
            get { return items.Count; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public virtual bool IndexExists(int index)
        {
            return index >= 0 && index < items.Count;
        }

        public virtual bool ItemExists(T item)
        {
            return items.Find(i => i.item.Equals(item)) != null;
        }

        public virtual void Move(int srcPosition, int destPosition)
        {
            TreeNode<T> item = items[srcPosition];
            items.RemoveAt(srcPosition);
            items.Insert(destPosition, item);
        }

        public virtual void Remove(T item)
        {
            if (item == null) {
                throw new NullReferenceException("<item> cannot be null in call to Remove(T item)");
            }
            if (items.Remove(items.Find(i => i.item.Equals(item))) == false) {
                throw new ApplicationException("Item \"" + item.ToString() + "\" could not be removed");
            }
        }

         public virtual void Remove(int index)
        {
            if (!IndexExists(index))
                throw new InvalidOperationException("Cannot remove nonexistent item");
            items.RemoveAt(index);
        }

        public virtual void SetSublist(int index, Tree<T> subtree)
        {
            items[index].subtree = subtree;
        }

    }
}
