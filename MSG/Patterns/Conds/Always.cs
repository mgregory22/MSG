//
// MSG/Patterns/Conds/Always.cs
//

namespace MSG.Patterns.Conds
{
    public class Always : Cond
    {
        public override bool Test()
        {
            return true;
        }
    }
}

namespace MSG.Patterns
{
    public abstract partial class Cond
    {
        public static Conds.Always ALWAYS = new Conds.Always();
    }
}
