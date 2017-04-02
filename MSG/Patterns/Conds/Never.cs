//
// MSG/Patterns/Conds/Never.cs
//

namespace MSG.Patterns.Conds
{
    public class Never : Cond
    {
        public override bool Test()
        {
            return false;
        }
    }
}

namespace MSG.Patterns
{
    public abstract partial class Cond
    {
        public static Conds.Never NEVER = new Conds.Never();
    }
}
