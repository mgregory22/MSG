//
// MSG/Patterns/UndoManager.cs
//

using System;
using System.Collections.Generic;
using MSG.Patterns;

namespace MSG.Patterns
{
    public class UndoManager
    {
        Stack<Command> undoStack;
        Stack<Command> redoStack;

        public UndoManager()
        {
            this.undoStack = new Stack<Command>();
            this.redoStack = new Stack<Command>();
        }

        /// <remarks>
        ///   virtual is for testing
        /// </remarks>
        public virtual bool CanRedo()
        {
            return redoStack.Count > 0;
        }

        public virtual bool CanUndo()
        {
            return undoStack.Count > 0;
        }

        public virtual void Do(Command command)
        {
            undoStack.Push(command);
            redoStack.Clear();
        }

        public virtual void Redo()
        {
            if (!CanRedo())
                throw new InvalidOperationException("Nothing to redo");

            Command command = redoStack.Pop();
            command.Do();
            undoStack.Push(command);
        }

        public virtual void Undo()
        {
            if (!CanUndo())
                throw new InvalidOperationException("Nothing to undo");

            Command command = undoStack.Pop();
            command.Undo();
            redoStack.Push(command);
        }
    }
}
