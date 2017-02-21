//
// MSG/Patterns/UnCmd.cs
//

namespace MSG.Patterns
{
    /// <summary>
    /// Undoable Command
    /// </summary>
    public abstract class UnCmd : Cmd
    {
        /// <summary>
        /// Undoes a previously performed command.
        /// </summary>
        public virtual Result Undo()
        {
            return UnCmd.CANTUNDO;
        }
    }

    // Add more Result classes to Cmd so they're all accessed from one place
    public abstract partial class Cmd
    {
        #region Stock Cmd.Result classes

        public class CantUndo : Result
        {
            public CantUndo()
            {
                message = "Cmd cannot be undone\n";
            }
        }

        public class CantRedo : Result
        {
            public CantRedo()
            {
                message = "Nothing to redo\n";
            }
        }

        #endregion

        #region Stock Cmd.Result objects

        public static CantUndo CANTUNDO = new CantUndo();
        public static CantRedo CANTREDO = new CantRedo();

        #endregion
    }

}
