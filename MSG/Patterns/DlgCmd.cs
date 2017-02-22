//
// MSG/Patterns/DlgDo.cs
//

using MSG.IO;

namespace MSG.Patterns
{
    /// <summary>
    /// Dialog Command - Do() calls overridable Create() which
    /// returns a fully-configured Cmd object which is then
    /// executed.  The reason for the indirection is so the
    /// base class Do() can do the undo and redo management,
    /// so you don't have to write it, and you just have to
    /// set the this.cmd field from Create() to the object
    /// to be executed.
    /// </summary>
    public abstract class DlgCmd
    {
        /// <summary>
        /// The command to be performed
        /// </summary>
        protected Cmd cmd;

        /// <summary>
        /// Input-output object
        /// </summary>
        protected Io io;

        /// <summary>
        /// Undo-and-redo manager
        /// </summary>
        protected UndoAndRedo undoAndRedo;

        public DlgCmd(Io io, UndoAndRedo undoAndRedo = null)
        {
            this.io = io;
            this.undoAndRedo = undoAndRedo;
        }

        /// <summary>
        /// Creates a command object to call Do() on (see CreateAndDo() below).
        /// </summary>
        public virtual Cmd Create()
        {
            return null;
        }

        /// <summary>
        /// The dialog is to be executed here.
        /// </summary>
        /// <remarks>
        /// All classes that inherit from DlgCmd should call
        /// this at the end of their Do() to perform the process
        /// and add it to the undo list.
        /// </remarks>
        public Cmd.Result Do(Io io)
        {
            // If the command already exists, then it has
            // already been done and can't be redone.
            if (cmd != null) {
                return Patterns.Cmd.CANTDO;
            }

            // Attempt to create the command object, by
            // whatever means defined in the derived class.
            cmd = Create();

            // If the command creation failed, it's ok, just return.
            if (cmd == null) {
                return Patterns.Cmd.OK;
            }

            // Perform the command.
            Cmd.Result result = cmd.Do();

            // Add the command to the undo manager queue.
            if (undoAndRedo != null) {
                undoAndRedo.Do(cmd);
            }

            // I'm not sure? This means Do() can be called repeatedly,
            // but I don't think that's right.
            cmd = null;

            return result;
        }
    }

    /// <summary>
    /// The HelpDialog object just prints the ToString() output of
    /// whatever object you send it.
    /// 
    /// Also acts as an example of how to construct a DlgCmd.
    /// 
    /// First, a Cmd class needs to be defined for HelpDialog to
    /// construct.  All the parameters that Do() needs to function
    /// are passed in via the constructor.
    /// </summary>
    public class Help : Cmd
    {
        Print print;
        object target;

        public Help(Print print, object target)
        {
            this.print = print;
            this.target = target;
        }

        public override Result Do()
        {
            print.String(target.ToString());
            return Patterns.Cmd.OK;
        }
    }

    public class HelpDlgCmd : DlgCmd
    {
        object target;

        public HelpDlgCmd(Io io, object target) : base(io)
        {
            this.target = target;
        }

        public override Cmd Create()
        {
            return new Help(io.print, target);
        }
    }

}

