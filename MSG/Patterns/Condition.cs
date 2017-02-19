//
// MSG/Patterns/Condition.cs
//

namespace MSG.Patterns
{
    /// <summary>
    /// Condition design pattern.
    /// </summary>
    public abstract class Condition {
        /// <summary>
        /// Should perform the test.
        /// </summary>
        public abstract bool Test();

        public static Always always = new Always();
        public static Never never = new Never();
    }

    public class Always : Condition {
        public override bool Test()
        {
            return true;
        }
    }

    public class Never : Condition {
        public override bool Test()
        {
            return false;
        }
    }
}
