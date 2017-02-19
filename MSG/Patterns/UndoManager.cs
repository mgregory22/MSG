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
        /// virtual is for testing
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

        public virtual Command.Result Redo()
        {
            if (!CanRedo())
            {
                return new Command.NothingToRedo();
            }
            Command command = redoStack.Pop();
            Command.Result result = command.Do();
            undoStack.Push(command);
            return result;
        }

        public virtual Command.Result Undo()
        {
            if (!CanUndo())
            {
                return new Command.NothingToUndo();
            }
            Command command = undoStack.Pop();
            Command.Result result = command.Undo();
            redoStack.Push(command);
            return result;
        }
    }
}
