//
// MSG/Types/Dir/MemDir.cs
//

using System.Collections;

namespace MSG.Types.Dir
{
    /// <summary>
    /// A Dir is a hierarchical data structure similar to a file
    /// directory system, except for subdirectories are always
    /// attached to items rather than standing by themselves
    /// like folders.
    /// </summary>
    public partial class MemDir<T> : Dir<T>
    {
        protected NodeStem root;
        protected NodeStem cur;

        public MemDir()
        {
            this.cur = this.root = new NodeStem();
        }

        public virtual int Add(string name, T item)
        {
            return cur.Add(name, item);
        }

        /// <summary>
        /// The number of nodes in the current dir
        /// </summary>
        public int Count {
            get { return cur.Count; }
        }

        /// <summary>
        /// Creates a subdir on an item and assigns its parent to the current
        /// dir.
        /// </summary>
        public virtual void CreateSubdir(int index)
        {
            cur.CreateSubdir(index);
        }

        public virtual void DeleteSubdir(int index)
        {
            cur.DeleteSubdir(index);
        }

        public void DownDir(int index)
        {
            // If entering a nonexistent directory is attempted, create it.
            if (!HasSubdirAt(index)) {
                CreateSubdir(index);
            }
            cur = cur.GetSubdir(index);
        }

        public string GetCurPath()
        {
            return cur.GetCurPath();
        }

        /// <summary>
        /// Returns the item at the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetItemAt(int index)
        {
            return cur.GetItemAt(index);
        }

        /// <summary>
        /// Returns the name of the item at the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetNameAt(int index)
        {
            return cur.GetNameAt(index);
        }

        /// <returns>
        /// Dir enumerator
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return cur.GetEnumerator();
        }

        public virtual int GetItemIndex(T item)
        {
            return cur.GetItemIndex(item);
        }

        public bool HasParent()
        {
            return cur != root;
        }

        /// <summary>
        /// True if a subdir exists at the given index
        /// </summary>
        public bool HasSubdirAt(int index)
        {
            return cur.HasSubdirAt(index);
        }

        /// <summary>
        /// Inserts a new item into the dir.
        /// The zero-based index determines where it is to be inserted.
        /// </summary>
        /// <param name="index">
        /// 0-based index in the list to add.
        /// If index points past end of list, adds item to the end.
        /// </param>
        /// <param name="name">
        /// Name of item
        /// </param>
        /// <param name="item">
        /// Item to add
        /// </param>
        public virtual int Insert(int index, string name, T item)
        {
            return cur.Insert(index, name, item);
        }

        /// <summary>
        /// True if the reference is somewhere in the dir
        /// </summary>
        public virtual bool ItemExists(T item)
        {
            return cur.ItemExists(item);
        }

        /// <summary>
        /// True if an item exists at the given index
        /// </summary>
        public virtual bool ItemExistsAt(int index)
        {
            return cur.ItemExistsAt(index);
        }

        /// <summary>
        /// Moves an item to another slot in the same dir
        /// </summary>
        public virtual void Move(int srcIndex, int destIndex)
        {
            cur.Move(srcIndex, destIndex);
        }

        /// <summary>
        /// Deletes an item by reference from the dir
        /// </summary>
        public virtual void Remove(T item)
        {
            cur.Remove(item);
        }

        /// <summary>
        /// Deletes an item by index from the dir
        /// </summary>
        public virtual void RemoveAt(int index)
        {
            cur.RemoveAt(index);
        }

        /// <summary>
        /// Changes the name of the item at the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        public void SetNameAt(int index, string name)
        {
            cur.SetNameAt(index, name);
        }

        public void UpDir()
        {
            // TODO: throw if null?
            if (cur.ParentStem != null) {
                cur = cur.ParentStem;
            }
        }
    }
}
