//
// MSG/Patterns/DialogCommand.cs
//

//

using MSG.IO;

namespace MSG.Patterns
{
    /// <summary>
    /// A DialogCommand is an interactive command factory pattern:
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
    public abstract class DialogCommand : Command
    {
        protected Print print;
        protected Read read;
        protected UndoManager undoManager;
        protected Command command;

        /// <summary>
        /// Sets up the basic facilities of a DialogCommand: output, input, and undo.
        /// </summary>
        /// <param name="print">Print destination</param>
        /// <param name="read">Read source, which can be null if the
        /// command just prints or signals and doesn't need to read anything</param>
        /// <param name="undoManager">The undo queue manager, which can be omitted
        /// if the command can't be undone</param>
        public DialogCommand(Print print, Read read = null, UndoManager undoManager = null)
        {
            this.print = print;
            this.read = read;
            this.undoManager = undoManager;
        }

        /// <summary>
        /// Creates a command object to call Do() on (see CreateAndDo() below).
        /// </summary>
        public virtual Command Create()
        {
            return null;
        }

        /// <summary>
        /// The dialog is to be executed here.
        /// </summary>
        /// <remarks>
        /// All classes that inherit from DialogCommand should call
        /// this at the end of their Do() to execute the command,
        /// add it to the undo list, and save the printed prompt
        /// text for testing the output.
        /// </remarks>
        public override Result Do()
        {
            // If the command already exists, then it has
            // already been done and can't be redone.
            if (command != null)
            {
                return Command.cannotDo;
            }

            // Attempt to create the command object, by
            // whatever means defined in the derived class.
            command = Create();

            // If the command creation failed, it's ok, just return.
            if (command == null) {
                return Command.ok;
            }

            // Perform the command.
            Result result = command.Do();

            // If the command is undoable, add it to the undo manager
            // queue.
            if (undoManager != null)
            {
                undoManager.Do(command);
            }

            // I'm not sure? This means Do() can be called repeatedly,
            // but I don't think that's right.
            command = null;

            return result;
        }
    }

    /// <summary>
    /// The HelpDialog object just prints the ToString() output of
    /// whatever object you send it.
    /// 
    /// Also acts as an example of how to construct a DialogCommand.
    /// 
    /// First, a Command class needs to be defined for HelpDialog to
    /// construct.  All the parameters that Do() needs to function
    /// are passed in via the constructor.
    /// </summary>
    public class Help : Command
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
            return Command.ok;
        }
    }

    public class HelpDialog : DialogCommand
    {
        object target;

        public HelpDialog(Print print, object target) : base(print, null, null)
        {
            this.target = target;
        }

        public override Command Create()
        {
            return new Help(print, target);
        }
    }

}

