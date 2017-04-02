//
// MSG/Types/Dir/DbDir.cs
//

using System;
using System.Collections;
using System.Data.Common;
using System.Diagnostics;
using MSG.IO;

namespace MSG.Types.Dir
{
    /// <summary>
    /// A Dir is a list whose nodes can hold both a data item
    /// and another dir (e.g. task and its subtasks).
    /// </summary>
    public partial class DbDir<T> : Dir<T> where T : new()
    {
        protected Database Db { get; set; }
        protected string TablePrefix { get; set; }
        protected string TableName { get; set; }
        protected ObjectToSQL ObjectToSQL { get; set; }
        protected int? CurDirParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tablePrefix"></param>
        /// <param name="columnDefs">
        /// The SQLite column definitions for your data items corresponding
        /// to the fields of T to be saved.  Someday I'll analyze T with
        /// reflection to get the column defs, or maybe create classes to
        /// represent fields that know how to generate SQL for themselves.
        /// </param>
        public DbDir(Database db, string tablePrefix)
        {
            Db = db;
            TablePrefix = tablePrefix;
            TableName = tablePrefix + "Dir";
            ObjectToSQL = new ObjectToSQL(typeof(T));
            CreateDirTableIfNotExists();
            CurDirParentId = null;
        }

        /// <summary>
        /// Adds an item to the end of the parent dir.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        public virtual int Add(string name, T item)
        {
            if (ItemExists(item)) {
                throw new InvalidOperationException("Cannot add same item twice");
            }
            return Insert(GetMaxIndex() + 1, name, item);
        }

        /// <summary>
        /// Closes a gap in the indexes of the current directory
        /// where an item was deleted.
        /// </summary>
        /// <param name="index">
        /// 0-based index of gap in the current directory
        /// </param>
        protected void CloseIndexGap(int index)
        {
            string closeGapSQL = string.Format(
                "UPDATE [{0}] SET [Index] = [Index] - 1 WHERE [Index] > {1}"
                , TableName
                , index
            );
            Db.ExecuteUpdate(closeGapSQL);
        }

        /// <summary>
        /// The number of nodes in this dir
        /// </summary>
        public int Count
        {
            get {
                return Db.CountQuery(TableName);
            }
        }

        protected void CreateDirTableIfNotExists()
        {
            string tableDef = "\n"
                + "    [Id] INTEGER PRIMARY KEY,\n"
                + "    [Name] VARCHAR(255),\n"
                + "    [ParentDirId] INTEGER,\n"
                + "    [Index] INTEGER NOT NULL,\n";
            tableDef += ObjectToSQL.GetColumnDefs();
            tableDef += string.Format(
                "    FOREIGN KEY([ParentDirId]) REFERENCES [{0}]([Id])\n"
                , TableName
            );
            Db.CreateTableIfNotExists(TableName, tableDef);
        }

        /// <summary>
        /// Creates a subdir on an item and assigns its
        /// parent to the current dir.
        /// </summary>
        public virtual void CreateSubdir(int index)
        {
            // CreateSubdir doesn't really make sense for DbDir
            // A subdir exists if there are children, so this
            // method is no-op.
        }

        public virtual void DeleteSubdir(int index)
        {
            throw new NotImplementedException();
        }

        public void DownDir(int index)
        {
            if (!ItemExistsAt(index)) {
                throw new InvalidOperationException(
                    string.Format(
                        "Item does not exist at index {0}"
                        , index
                    )
                );
            }
            CurDirParentId = GetChildIdOf(CurDirParentId, index);
        }

        protected int GetChildIdOf(int? parentId, int childIndex)
        {
            string selectSQL = string.Format(
                "SELECT [Id] FROM [{0}] WHERE [ParentDirId] {1} AND [Index] = {2}"
                , TableName
                , ObjectToSQL.GetEqualsExpr(parentId)
                , childIndex
            );
            return Db.ExecuteIntQuery(selectSQL);
        }

        protected string GetCurPathAux(int? parentId)
        {
            if (parentId == null) {
                return "/";
            }
            int id = parentId.Value;
            return GetCurPathAux(GetParentIdOf(id)) + GetNameById(id) + "/";
        }

        public string GetCurPath()
        {
            return GetCurPathAux(CurDirParentId);
        }

        /// <summary>
        /// Dir enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return new Enumerator(Db, TableName, CurDirParentId, ObjectToSQL);
        }

        public T GetItemAt(int index)
        {
            string selectSQL = string.Format(
                "SELECT * FROM [{0}] WHERE [ParentDirId] {1} AND [Index] = {2}"
                , TableName
                , ObjectToSQL.GetEqualsExpr(CurDirParentId)
                , index
            );
            IEnumerator e = Db.ExecuteSelect(selectSQL);
            if (!e.MoveNext()) {
                throw new ArgumentOutOfRangeException(
                    string.Format(
                        "No item exists at index {0}"
                        , index
                    )
                );
            }
            DbDataRecord dto = (DbDataRecord) e.Current;
            // Use reflection to construct the right object,
            // and populate its fields with database data
            T t = new T();
            ObjectToSQL.InitializeObjectFromDTO(t, dto);
            Db.EndQuery();
            return t;
        }

        public int GetItemIndex(T item)
        {
            string selectSQL = string.Format(
                "SELECT [Index] FROM [{0}] WHERE [ParentDirId] {1} AND {2}"
                , TableName
                , ObjectToSQL.GetEqualsExpr(CurDirParentId)
                , ObjectToSQL.GetQueryWhereDefs(item)
            );
            return Db.ExecuteIntQuery(selectSQL);
        }

        protected int GetMaxIndex()
        {
            string maxSQL = string.Format(
                "SELECT MAX([Index]) FROM [{0}] WHERE [ParentDirId] {1}"
                , TableName
                , ObjectToSQL.GetEqualsExpr(CurDirParentId)
            );
            return Db.ExecuteIntQuery(maxSQL, -1);
        }

        /// <summary>
        /// Returns the name of the item at the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetNameAt(int index)
        {
            string selectSQL = string.Format(
                "SELECT [Name] FROM [{0}] WHERE [ParentDirId] {1} AND [Index] = {2}"
                , TableName
                , ObjectToSQL.GetEqualsExpr(CurDirParentId)
                , index
            );
            return Db.ExecuteStringQuery(selectSQL);
        }

        protected string GetNameById(int id)
        {
            string selectSQL = string.Format(
                "SELECT [Name] FROM [{0}] WHERE [Id] = {1}"
                , TableName
                , id
            );
            return Db.ExecuteStringQuery(selectSQL);
        }

        protected int? GetParentIdOf(int id)
        {
            string selectSQL = string.Format(
                "SELECT [ParentDirId] FROM [{0}] WHERE [Id] = {1}"
                , TableName
                , id
            );
            int parentId = Db.ExecuteIntQuery(selectSQL);
            if (parentId == 0) {
                return null;
            }
            return parentId;
        }

        public bool HasParent()
        {
            return CurDirParentId != null;
        }

        /// <summary>
        /// True if a subdir exists at the given index
        /// </summary>
        public bool HasSubdirAt(int index)
        {
            // All items have subdirectories.  There's no separate
            // structure needed.
            return true;
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
            if (ItemExists(item)) {
                throw new InvalidOperationException("Cannot add same item twice");
            }
            int maxIndex = GetMaxIndex();
            if (index > maxIndex + 1) {
                index = maxIndex + 1;
            }
            MakeIndexGap(index);
            string insertSQL = string.Format(
                "INSERT INTO [{0}] ([ParentDirId], [Index], [Name], {1}) VALUES ({2}, {3}, '{4}', {5})"
                , TableName
                , ObjectToSQL.GetCommaDelimitedColumnNames()
                , ObjectToSQL.SQLValue(CurDirParentId)
                , index
                , name
                , ObjectToSQL.GetCommaDelimitedValues(item)
            );
            return Db.ExecuteInsert(insertSQL);
        }

        /// <summary>
        /// True if the reference is somewhere in the dir
        /// </summary>
        public virtual bool ItemExists(T item)
        {
            string where = string.Format(
                "[ParentDirId] {0} AND {1}"
                , ObjectToSQL.GetEqualsExpr(CurDirParentId)
                , ObjectToSQL.GetQueryWhereDefs(item)
            );
            int count = Db.CountQuery(TableName, where);
            if (count > 1) {
                // This cheesy error is just to make sure dupes aren't sneaking in
                throw new DuplicateWaitObjectException(
                    string.Format(
                        "{0} returned more than 1 row"
                        , where
                    )
                );
            }
            return count > 0;
        }

        /// <summary>
        /// True if an item exists at the given index
        /// </summary>
        public virtual bool ItemExistsAt(int index)
        {
            string where = string.Format(
                "[ParentDirId] {0} AND [Index] = {1}"
                , ObjectToSQL.GetEqualsExpr(CurDirParentId)
                , index
            );
            int count = Db.CountQuery(TableName, where);
            if (count > 1) {
                // This cheesy error is just to make sure dupes aren't sneaking in
                throw new DuplicateWaitObjectException(
                    string.Format(
                        "{0} returned more than 1 row"
                        , where
                    )
                );
            }
            return count > 0;
        }

        /// <summary>
        /// Makes a gap in the indexes of the current directory so
        /// an item can be inserted there.
        /// </summary>
        /// <param name="index">
        /// 0-based index in the current directory
        /// </param>
        protected void MakeIndexGap(int index)
        {
            string makeGapSQL = string.Format(
                "UPDATE [{0}] SET [Index] = [Index] + 1 WHERE [ParentDirId] {1} AND [Index] >= {2}"
                , TableName
                , ObjectToSQL.GetEqualsExpr(CurDirParentId)
                , index
            );
            Db.ExecuteUpdate(makeGapSQL);
        }

        /// <summary>
        /// Moves an item to another slot in the same dir
        /// </summary>
        public virtual void Move(int srcIndex, int destIndex)
        {
            MakeIndexGap(destIndex);
            if (srcIndex > destIndex) {
                srcIndex++;
            }
            string moveSQL = string.Format(
                "UPDATE [{0}] SET [Index] = {1} WHERE [Index] = {2}"
                , TableName
                , destIndex
                , srcIndex
            );
            Db.ExecuteUpdate(moveSQL);
        }

        /// <summary>
        /// Deletes an item by reference from the dir
        /// </summary>
        public virtual void Remove(T item)
        {
            int index = GetItemIndex(item);
            string deleteSQL = string.Format(
                "DELETE FROM [{0}] WHERE [Index] = {1}"
                , TableName
                , index
            );
            Db.ExecuteDelete(deleteSQL);
            CloseIndexGap(index);
        }

        /// <summary>
        /// Deletes an item by index from the dir
        /// </summary>
        public virtual void RemoveAt(int index)
        {
            string deleteSQL = string.Format(
                "DELETE FROM [{0}] WHERE [Index] = {1}"
                , TableName
                , index
            );
            Db.ExecuteDelete(deleteSQL);
            CloseIndexGap(index);
        }

        /// <summary>
        /// Changes the name of the item at the given index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        public void SetNameAt(int index, string name)
        {
            string setNameSQL = string.Format(
                "UPDATE [{0}] SET [Name] = '{1}' WHERE [ParentDirId] = {2} AND [Index] = {3}"
                , TableName
                , name
                , CurDirParentId
                , index
            );
            Db.ExecuteUpdate(setNameSQL);
        }

        /// <summary>
        /// Sets the current directory to the parent directory.
        /// </summary>
        public void UpDir()
        {
            if (CurDirParentId == null) {
                throw new InvalidOperationException("Cannot UpDir() from root");
            }
            CurDirParentId = GetParentIdOf(CurDirParentId.Value);
        }
    }
}
