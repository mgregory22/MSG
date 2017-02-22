//
// MSG/Patterns/Cmd/Results/CantRedo.cs
//

namespace MSG.Patterns
{
    public abstract partial class Cmd
    {
        /// <summary>
        /// Stock result class
        /// </summary>
        public class CantRedo : Result
        {
            public CantRedo()
            {
                message = "Nothing to redo";
            }
        }

        public static CantRedo CANTREDO = new CantRedo();
    }
}

