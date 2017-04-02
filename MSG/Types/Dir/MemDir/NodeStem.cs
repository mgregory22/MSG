//
// MSG/Types/Dir/MemDir/NodeStem.cs
//

using System;
using System.Collections;
using System.Collections.Generic;

namespace MSG.Types.Dir
{
    /// <summary>
    /// A Dir is a hierarchical data structure similar to a file
    /// directory system, except for items within a directory have
    /// an order, and subdirectories are always attached to items
    /// rather than standing by themselves like folders.
    /// </summary>
    public partial class MemDir<T> : Dir<T>
    {
        /// <summary>
        /// This class represents a "stem", which is a list of nodes
        /// with relational links (parent stem, parent item).
        /// Any item can have an associated subdir.
        /// </summary>
        public class NodeStem
        {
            protected List<Node> nodes;
            protected NodeStem parentStem;
            protected Node parentItem;

            public NodeStem(NodeStem parentStem = null, Node parentItem = null)
            {
                this.nodes = new List<Node>();
                // Having these names differ by only one letter is so great
                ParentStem = parentStem;
                ParentItem = parentItem;
            }

            /// <summary>
            /// Adds an item to the end of the current dir.
            /// </summary>
            /// <param name="item"></param>
            public virtual int Add(string name, T item)
            {
                if (ItemExists(item)) {
                    throw new InvalidOperationException("Cannot add same item twice");
                }
                Node node = new Node(name, item);
                nodes.Add(node);
                return 1;
            }

            /// <summary>
            /// The number of nodes in the current dir
            /// </summary>
            public int Count {
                get { return nodes.Count; }
            }

            /// <summary>
            /// Creates a subdir on an item and assigns its
            /// parent to the current dir.
            /// </summary>
            public virtual void CreateSubdir(int index)
            {
                nodes[index].Sub = new NodeStem(this, nodes[index]);
            }

            public virtual void DeleteSubdir(int index)
            {
                nodes[index].Sub = null;
            }

            public string GetCurPath()
            {
                if (ParentStem == null) {
                    return "/";
                }
                return ParentStem.GetCurPath() + ParentItem.Name + "/";
            }

            public T GetItemAt(int index)
            {
                return nodes[index].Item;
            }

            public int GetItemIndex(T item)
            {
                return nodes.FindIndex(i => i.Item.Equals(item));
            }

            public string GetNameAt(int index)
            {
                return nodes[index].Name;
            }

            /// <summary>
            /// Dir enumerator
            /// </summary>
            /// <returns></returns>
            public IEnumerator GetEnumerator()
            {
                return new Enumerator(nodes);
            }

            public NodeStem GetSubdir(int index)
            {
                if (!ItemExistsAt(index)) {
                    throw new InvalidOperationException(
                        string.Format(
                            "Item does not exist at {0}"
                            , index
                        )
                    );
                }
                return nodes[index].Sub;
            }

            /// <summary>
            /// True if a subdir exists at the given index
            /// </summary>
            public bool HasSubdirAt(int index)
            {
                return nodes[index].Sub != null;
            }

            /// <summary>
            /// Inserts a new item into the dir.
            /// The zero-based index determines where it is to be inserted.
            /// </summary>
            /// <param name="index">
            /// 0-based index in the current directory to insert.
            /// If index points past end of list, adds item to the end.
            /// </param>
            /// <param name="item">
            /// Item to add.
            /// </param>
            public virtual int Insert(int index, string name, T item)
            {
                if (ItemExists(item)) {
                    throw new InvalidOperationException("Cannot add same item twice");
                }
                Node node = new Node(name, item);
                // If index points past end of list, add item to end.
                if (index > nodes.Count) {
                    index = nodes.Count;
                }
                nodes.Insert(index, node);
                return 1;
            }

            /// <summary>
            /// True if the reference is somewhere in the dir
            /// </summary>
            public virtual bool ItemExists(T item)
            {
                return nodes.Find(i => i.Item.Equals(item)) != null;
            }

            /// <summary>
            /// True if an item exists at the given index
            /// </summary>
            public virtual bool ItemExistsAt(int index)
            {
                return index >= 0 && index < nodes.Count;
            }

            /// <summary>
            /// Moves an item to another slot in the same dir
            /// </summary>
            public virtual void Move(int srcIndex, int destIndex)
            {
                Node item = nodes[srcIndex];
                nodes.RemoveAt(srcIndex);
                nodes.Insert(destIndex, item);
            }

            public Node ParentItem {
                get {
                    return this.parentItem;
                }
                set {
                    this.parentItem = value;
                }
            }

            public NodeStem ParentStem {
                get {
                    return this.parentStem;
                }
                set {
                    this.parentStem = value;
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
                if (nodes.Remove(nodes.Find(i => i.Item.Equals(item))) == false) {
                    throw new ApplicationException("Item \"" + item.ToString() + "\" could not be removed");
                }
            }

            /// <summary>
            /// Deletes an item by index from the dir
            /// </summary>
            public virtual void RemoveAt(int index)
            {
                if (!(index >= 0 && index < nodes.Count)) {
                    throw new InvalidOperationException("Cannot remove nonexistent item");
                }
                nodes.RemoveAt(index);
            }

            public void SetNameAt(int index, string name)
            {
                nodes[index].Name = name;
            }
        }
    }
}
