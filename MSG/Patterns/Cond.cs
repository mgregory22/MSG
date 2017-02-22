//
// MSG/Patterns/Cond.cs
//

namespace MSG.Patterns
{
    /// <summary>
    /// Condition functor
    /// </summary>
    public abstract partial class Cond {
        /// <summary>
        /// Performs boolean test
        /// </summary>
        public abstract bool Test();
    }
}
