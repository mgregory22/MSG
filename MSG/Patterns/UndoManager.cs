//
// MSG/Patterns/UndoManager.cs
//

using System.Collections.Generic;

namespace MSG.Patterns
{
    public class UndoManager
    {
        Stack<UnCmd> undoStack;
        Stack<UnCmd> redoStack;

        public UndoManager()
        {
            this.undoStack = new Stack<UnCmd>();
            this.redoStack = new Stack<UnCmd>();
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

        public virtual void Do(Cmd cmd)
        {
            redoStack.Clear();
        }

        public virtual void Do(UnCmd cmd)
        {
            undoStack.Push(cmd);
            redoStack.Clear();
        }

        public virtual Cmd.Result Redo()
        {
            if (!CanRedo())
            {
                return new UnCmd.CantRedo();
            }
            UnCmd cmd = redoStack.Pop();
            Cmd.Result result = cmd.Do();
            undoStack.Push(cmd);
            return result;
        }

        public virtual Cmd.Result Undo()
        {
            if (!CanUndo())
            {
                return UnCmd.CANTUNDO;
            }
            UnCmd cmd = undoStack.Pop();
            Cmd.Result result = cmd.Undo();
            redoStack.Push(cmd);
            return result;
        }
    }
}
