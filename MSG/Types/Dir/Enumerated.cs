//
// MSG/Types/Dir/Enumerated.cs
//

using System;

namespace MSG.Types.Dir
{
    public class Enumerated<T>
    {
        public string Name { get; set; }
        public T Item { get; set; }
    }
}
