//
// MSG/IO/Database.cs
//

using System;
using System.IO;
using System.Collections;
using System.Data.SQLite;

namespace MSG.IO
{
    public class Database : IEnumerable, IDisposable
    {
        public enum OpenStyle
        {
            OpenExistent,
            RecreateExistent
        }

        protected SQLiteConnection Con { get; set; }
        protected SQLiteCommand Com { get; set; }
        protected SQLiteDataReader Reader { get; set; }

        public Database(string fileName, OpenStyle openStyle)
        {
            if (openStyle == OpenStyle.RecreateExistent
                && File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            Con = new SQLiteConnection(
                string.Format(
                    "DataSource='{0}'; Version=3;"
                    , fileName
                )
            );
            Con.Open();
            Com = Con.CreateCommand();
            // Turn on foreign key support
            Com.CommandText = "PRAGMA foreign_keys = ON";
            Com.ExecuteNonQuery();
        }

        public void Dispose()
        {
            Com.Dispose();
            Con.Close();
            Con.Dispose();
        }

        public int CountQuery(string tableName, string where = "")
        {
            string countSQL = string.Format(
                "SELECT COUNT(*) FROM [{0}]"
                , tableName
            );
            if (!string.IsNullOrEmpty(where)) {
                countSQL += string.Format(
                    " WHERE {0}"
                    , where
                );
            }
            return ExecuteIntQuery(countSQL);
        }

        public int CreateTableIfNotExists(string tableName, string columnDefs)
        {
            Com.CommandText = string.Format(
                "CREATE TABLE IF NOT EXISTS [{0}] ({1})"
                , tableName
                , columnDefs
            );
            int rows = Com.ExecuteNonQuery();
            return rows;
        }

        public bool DoesTableExist(string tableName)
        {
            string countSQL = string.Format(
                "SELECT COUNT(*) FROM [sqlite_master] WHERE [type] = 'table' AND [name] = '{0}'"
                , tableName
            );
            int count = ExecuteIntQuery(countSQL);
            return count > 0;
        }

        public void EndQuery()
        {
            Com.Dispose();
        }

        #region Enumerator of SQLiteDataReader

        public class Enumerator : IEnumerator
        {
            protected SQLiteDataReader r;
            protected IEnumerator e;

            public Enumerator(SQLiteDataReader reader)
            {
                r = reader;
                e = reader.GetEnumerator();
            }

            object IEnumerator.Current {
                get {
                    return e.Current;
                }
            }

            bool IEnumerator.MoveNext()
            {
                bool result = false;

                if (!r.IsClosed) {
                    result = e.MoveNext();
                    if (!result) {
                        r.Close();
                    }
                }

                return result;
            }

            void IEnumerator.Reset()
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        public int ExecuteDelete(string deleteSQL)
        {
            Com.CommandText = deleteSQL;
            int rows = Com.ExecuteNonQuery();
            return rows;
        }

        public int ExecuteInsert(string insertSQL)
        {
            Com.CommandText = insertSQL;
            int rows = Com.ExecuteNonQuery();
            return rows;
        }

        public int ExecuteIntQuery(string selectSQL, int nullValue = 0)
        {
            Com.CommandText = selectSQL;
            var o = Com.ExecuteScalar(System.Data.CommandBehavior.SingleRow);
            if (o is System.DBNull) {
                return nullValue;
            }
            return (int) (long) o;
        }

        public IEnumerator ExecuteSelect(string selectSQL)
        {
            Com.CommandText = selectSQL;
            Reader = Com.ExecuteReader();
            return new Enumerator(Reader);
        }

        public string ExecuteStringQuery(string selectSQL)
        {
            Com.CommandText = selectSQL;
            return (string) Com.ExecuteScalar(System.Data.CommandBehavior.SingleRow);
        }

        public int ExecuteUpdate(string updateSQL)
        {
            Com.CommandText = updateSQL;
            int rows = Com.ExecuteNonQuery();
            return rows;
        }

        public IEnumerator GetEnumerator()
        {
            return new Enumerator(Reader);
        }
    }
}
