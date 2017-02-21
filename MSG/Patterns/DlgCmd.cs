//
// MSG/Patterns/DlgCmd.cs
//

using MSG.IO;

namespace MSG.Patterns
{
    /// <summary>
    /// A DlgCmd is an interactive command factory pattern:
    /// the user enters information and out comes a new command
    /// object initialized with that user-entered data.
    /// 
    /// It also optionally manages undo and redo.
    /// 
    /// Just derive from this class and override Create() with your
    /// interaction and command object creation code.
    /// 
    /// Calling the derived object's Do() method (the first time) will
    /// call your custom Create() method to create the command object,
    /// call its Do() method, and push the command object on the undo
    /// stack.
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

        public DlgCmd(Io io)
        {
            this.io = io;
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
        public virtual Cmd.Result Do(Io io)
        {
            // If the command already exists, then it has
            // already been done and can't be redone.
            if (cmd != null)
            {
                return Cmd.CANTDO;
            }

            // Attempt to create the command object, by
            // whatever means defined in the derived class.
            cmd = Create();

            // If the command creation failed, it's ok, just return.
            if (cmd == null) {
                return Cmd.OK;
            }

            // Perform the command.
            Cmd.Result result = cmd.Do();

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
            return Cmd.OK;
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

