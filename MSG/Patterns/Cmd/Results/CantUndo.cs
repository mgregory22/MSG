//
// MSG/Patterns/Cmd/Results/CantUndo.cs
//

namespace MSG.Patterns
{
    public abstract partial class Cmd
    {
        /// <summary>
        /// Stock result class
        /// </summary>
        public class CantUndo : Result
        {
            public CantUndo()
            {
                message = "Command cannot be undone";
            }
        }

        public static CantUndo CANTUNDO = new CantUndo();
    }
}

