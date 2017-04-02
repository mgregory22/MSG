//
// MSG/Types/Dir/MemDir/Node.cs
//

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
        /// <summary>
        /// A node holds a data item and a subdirectory
        /// that holds the children of that item.
        /// </summary>
        public class Node
        {
            protected string name;
            protected T item;
            protected NodeStem sub;

            public Node(string name, T item, NodeStem sub = null)
            {
                Name = name;
                Item = item;
                Sub = sub;
            }

            public T Item { get; set; }

            public string Name { get; set; }

            public NodeStem Sub { get; set; }
        }
    }
}
