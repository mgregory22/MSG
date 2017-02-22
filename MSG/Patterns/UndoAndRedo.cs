//
// MSG/Patterns/UndoAndRedo.cs
//

using System.Collections.Generic;

namespace MSG.Patterns
{
    /// <summary>
    /// UndoAndRedo takes an undoable command and
    /// performs and tracks the doing and undoing.
    /// </summary>
    public class UndoAndRedo
    {
        Stack<Cmd> undoStack;
        Stack<Cmd> redoStack;

        public UndoAndRedo()
        {
            this.undoStack = new Stack<Cmd>();
            this.redoStack = new Stack<Cmd>();
        }

        /// <remarks>
        /// virtual is for testing
        /// </remarks>
        public virtual bool RedoStackIsEmpty()
        {
            return redoStack.Count > 0;
        }

        public virtual bool UndoStackIsEmpty()
        {
            return undoStack.Count > 0;
        }

        public virtual void Do(Cmd cmd)
        {
            undoStack.Push(cmd);
            redoStack.Clear();
        }

        public virtual Cmd.Result Redo()
        {
            if (!RedoStackIsEmpty())
            {
                return new Cmd.CantRedo();
            }
            Cmd cmd = redoStack.Pop();
            Cmd.Result result = cmd.Do();
            undoStack.Push(cmd);
            return result;
        }

        public virtual Cmd.Result Undo()
        {
            if (!UndoStackIsEmpty())
            {
                return Patterns.Cmd.CANTUNDO;
            }
            Cmd cmd = undoStack.Pop();
            Cmd.Result result = cmd.Undo();
            redoStack.Push(cmd);
            return result;
        }
    }
}
