//
// MSG/Types/Dir/DbDir/Enumerator.cs
//

using System;
using System.Collections;
using System.Data.Common;
using MSG.IO;

namespace MSG.Types.Dir
{
    public partial class DbDir<T> : Dir<T>
    {
        public class Enumerator : IEnumerator
        {
            IEnumerator sqliteEnumerator;
            ObjectToSQL objectToSQL;

            public Enumerator(Database db, string tableName, int? curDirParentId, ObjectToSQL objectToSQL)
            {
                sqliteEnumerator = db.ExecuteSelect(
                    string.Format(
                        "SELECT * FROM [{0}] WHERE [ParentDirId] {1} ORDER BY [Index]"
                        , tableName
                        , ObjectToSQL.GetEqualsExpr(curDirParentId)
                    )
                );
                this.objectToSQL = objectToSQL;
            }

            object IEnumerator.Current {
                get {
                    DbDataRecord dto = (DbDataRecord) sqliteEnumerator.Current;
                    // Use reflection to construct the right object,
                    // and populate its fields with database data
                    T t = new T();
                    objectToSQL.InitializeObjectFromDTO(t, dto);
                    return t;
                }
            }

            bool IEnumerator.MoveNext()
            {
                return sqliteEnumerator.MoveNext();
            }

            void IEnumerator.Reset()
            {
                // Microsoft advises that this call isn't really necessary
                throw new NotSupportedException();
            }
        }
    }
}
