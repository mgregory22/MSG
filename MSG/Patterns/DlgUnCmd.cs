//
// MSG/Patterns/DlgUnCmd.cs
//

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
    public abstract class DlgUnCmd : DlgCmd
    {
        protected UndoManager undoManager;

        public DlgUnCmd(Io io, UndoManager undoManager) : base(io)
        {
            this.undoManager = undoManager;
        }

        /// <summary>
        /// Creates a command object to call Do() on (see CreateAndDo() below).
        /// </summary>
        public new virtual UnCmd Create()
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
        public virtual Cmd.Result Do(Io io, UndoManager undoManager)
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

            // Add the command to the undo manager queue.
            undoManager.Do(cmd);

            // I'm not sure? This means Do() can be called repeatedly,
            // but I don't think that's right.
            cmd = null;

            return result;
        }

        public virtual Cmd.Result Undo(Io io, UndoManager undoManager)
        {
            return Cmd.OK;
        }
    }
}

