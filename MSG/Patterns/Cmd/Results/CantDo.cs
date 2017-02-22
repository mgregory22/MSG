//
// MSG/Patterns/Cmd/Results/CantDo.cs
//

namespace MSG.Patterns
{
    public abstract partial class Cmd
    {
        /// <summary>
        /// Stock result class
        /// </summary>
        public class CantDo : Result
        {
            public CantDo()
            {
                message = "Command cannot be done";
            }
        }

        public static CantDo CANTDO = new CantDo();
    }
}

