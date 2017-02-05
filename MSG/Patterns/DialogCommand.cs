//
// MSG/Patterns/DialogCommand
//
using System;
using MSG.Console;
using MSG.IO;

namespace MSG.Patterns
{
    /// <summary>
    ///   A DialogCommand is an interactive factory pattern: the user
    ///   enters information and out comes a new object initialized with
    ///   that user-entered data.
    ///   
    ///   Just derive from this class and override Create() with your
    ///   interaction and command object creation code.
    ///   
    ///   Calling the derived object's Do() method (the first time) will
    ///   call your custom Create() method to create the command object,
    ///   call its Do() method, and push the command object on the undo
    ///   stack.
    /// </summary>
    public abstract class DialogCommand : Command
    {
        protected Print print;
        protected Read read;
        protected UndoManager undoManager;
        protected Editor editor;
        protected Command command;
        protected string lastPrompt; // this exists only for testing

        public DialogCommand(Print print, Read read, UndoManager undoManager)
        {
            this.print = print;
            this.read = read;
            this.undoManager = undoManager;
            editor = new Editor(print, read);
        }

        public string LastPrompt
        {
            get { return this.lastPrompt; }
        }

        /// <summary>
        ///   Creates a command object to call Do() on.
        /// </summary>
        /// <returns>
        ///   Return this to just call your Do() fn.
        /// </returns>
        public virtual Command Create()
        {
            return null;
        }

        /// <summary>
        ///   The dialog is to be executed here.
        /// </summary>
        /// <remarks>
        ///   All classes that inherit from DialogCommand should call this at the
        ///   end of their Do() to execute the command, add it to the undo list,
        ///   and save the printed prompt text for testing the output.
        /// </remarks>
        public override void Do()
        {
            if (command != null)
                throw new InvalidOperationException("Task is already done");
            command = Create();
            if (command == null)
                return;
            command.Do();
            if (undoManager != null)
                undoManager.Do(command);
            command = null;
            this.lastPrompt = editor.LastPrompt;
        }

        public virtual bool IsEnabled()
        {
            return true;
        }
    }
}
