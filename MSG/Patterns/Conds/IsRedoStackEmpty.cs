//
// MSG/Patterns/Conds/IsRedoStackEmpty.cs
//

namespace MSG.Patterns.Conds
{
    public class IsRedoStackEmpty : Cond
    {
        protected UndoAndRedo undoMan;

        public IsRedoStackEmpty(UndoAndRedo undoMan)
        {
            this.undoMan = undoMan;
        }

        public override bool Test()
        {
            return undoMan.RedoStackIsEmpty();
        }
    }
}
