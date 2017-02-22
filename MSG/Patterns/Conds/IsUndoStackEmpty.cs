//
// MSG/Patterns/Conds/IsUndoStackEmpty.cs
//

namespace MSG.Patterns.Conds
{
    public class IsUndoStackEmpty : Cond
    {
        protected UndoAndRedo undoAndRedo;

        public IsUndoStackEmpty(UndoAndRedo undoAndRedo)
        {
            this.undoAndRedo = undoAndRedo;
        }

        public override bool Test()
        {
            return undoAndRedo.UndoStackIsEmpty();
        }
    }
}
