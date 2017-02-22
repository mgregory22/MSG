//
// MSG/Patterns/Cmd.cs
//

namespace MSG.Patterns
{
    /// <summary>
    /// Command
    /// </summary>
    public abstract partial class Cmd
    {
        /// <summary>
        /// Should perform the command with parameters initialized in the constructor.
        /// </summary>
        public virtual Result Do()
        {
            return CANTDO;
        }

        /// <summary>
        /// Undoes a previously performed command.
        /// </summary>
        public virtual Result Undo()
        {
            return CANTUNDO;
        }
    }

}
