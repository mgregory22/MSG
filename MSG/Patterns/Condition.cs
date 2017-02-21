//
// MSG/Patterns/Condition.cs
//

namespace MSG.Patterns
{
    /// <summary>
    /// Condition design pattern.
    /// </summary>
    public abstract class Cond {
        /// <summary>
        /// Should perform the test.
        /// </summary>
        public abstract bool Test();

        public static Always ALWAYS = new Always();
        public static Never NEVER = new Never();
    }

    public class Always : Cond {
        public override bool Test()
        {
            return true;
        }
    }

    public class Never : Cond {
        public override bool Test()
        {
            return false;
        }
    }
}
