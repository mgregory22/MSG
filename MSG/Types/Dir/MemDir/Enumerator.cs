//
// MSG/Types/Dir/MemDir/Enumerator.cs
//

using System.Collections;
using System.Collections.Generic;

namespace MSG.Types.Dir
{
    public partial class MemDir<T> : Dir<T>
    {
        public class Enumerator : IEnumerator
        {
            protected List<Node> items;
            protected List<Node>.Enumerator iterator;

            public Enumerator(List<Node> items)
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
                    return new Enumerated<T> {
                        Name = iterator.Current.Name,
                        Item = iterator.Current.Item
                    };
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
    }
}
