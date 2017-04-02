//
// MSG/Types/Dir/Dir.cs
//

using System.Collections;

namespace MSG.Types.Dir
{
    /// <summary>
    /// Dir isn't really a perfect name for this data structure.
    /// A Dir is a tree where:
    ///   * Each node is stored in an indexed list (dir)
    ///   * Each node has a name, item (user data), and child list (subdir)
    ///   * There's always a current dir where any changes are made
    /// </summary>
    public interface Dir<T> : IEnumerable
    {
        /// <summary>
        /// Adds an item to the end of the parent dir
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        int Add(string name, T item);

        /// <summary>
        /// The number of nodes in the current dir
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Creates a subdir on an item and assigns its
        /// parent to the current dir
        /// </summary>
        void CreateSubdir(int index);

        /// <summary>
        /// Deletes an item's subdir
        /// </summary>
        /// <param name="index">
        /// Index in current dir of child subdir to delete
        /// </param>
        void DeleteSubdir(int index);

        /// <summary>
        /// Change the current dir to the given child's subdir
        /// </summary>
        /// <param name="index">
        /// Index in current dir of child dir to change to.
        /// </param>
        void DownDir(int index);

        /// <summary>
        /// Returns a string containing the path to the current dir
        /// </summary>
        /// <returns>
        /// Slash-delimited path of names to the current dir
        /// </returns>
        string GetCurPath();

        /// <summary>
        /// Returns the item at the given index
        /// </summary>
        /// <param name="index">
        /// Index of item to get
        /// </param>
        /// <returns>
        /// The item
        /// </returns>
        T GetItemAt(int index);

        /// <summary>
        /// Finds an item in the current dir
        /// </summary>
        /// <param name="item">
        /// Item to find
        /// </param>
        /// <returns>
        /// The index
        /// </returns>
        int GetItemIndex(T item);

        /// <summary>
        /// Returns the name of the item at the given index
        /// </summary>
        /// <param name="index">
        /// Index of child in the current dir to get the name of
        /// </param>
        /// <returns>
        /// The name string
        /// </returns>
        string GetNameAt(int index);

        /// <returns>
        /// True if current dir has a parent (is not the root)
        /// </returns>
        bool HasParent();

        /// <returns>
        /// True if a subdir exists at the given index
        /// </returns>
        bool HasSubdirAt(int index);

        /// <summary>
        /// Inserts a new item into the dir
        /// The zero-based index determines where it is to be inserted
        /// </summary>
        /// <param name="index">
        /// 0-based index in the current dir to add
        /// If index points past end of list, adds item to the end
        /// </param>
        /// <param name="item">
        /// Row to insert
        /// </param>
        /// <param name="name">
        /// Name of item
        /// </param>
        int Insert(int index, string name, T item);

        /// <summary>
        /// True if the reference is somewhere in the dir
        /// </summary>
        bool ItemExists(T item);

        /// <summary>
        /// True if an item exists at the given index
        /// </summary>
        bool ItemExistsAt(int index);

        /// <summary>
        /// Moves an item to another slot in the same dir
        /// </summary>
        void Move(int srcIndex, int destIndex);

        /// <summary>
        /// Deletes an item by reference from the dir
        /// </summary>
        void Remove(T item);

        /// <summary>
        /// Deletes an item by index from the dir
        /// </summary>
        void RemoveAt(int index);

        /// <summary>
        /// Changes the name of the item at the given index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        void SetNameAt(int index, string name);

        /// <summary>
        /// Change the current dir to the parent dir, if it exists
        /// </summary>
        void UpDir();
    }
}
