//
// MSG/Types/Array/EndlessArray.cs
//

using System;
using System.Collections.Generic;

namespace MSG.Types.Array
{
    /// <summary>
    ///   An array that returns the last element if the index
    ///   is larger than the length of the array.
    /// </summary>
    public class EndlessArray<T> : List<T>
    {
        public EndlessArray(params T[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                this.Add(items[i]);
            }
        }

        public new T this[int i]
        {
            get { return base[Math.Min(i, this.Count - 1)]; }
        }
    }
}
