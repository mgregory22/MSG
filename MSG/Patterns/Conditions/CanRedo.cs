//
// MSG/Patterns/Conditions/CanRedo.cs
//

namespace MSG.Patterns.Conditions
{
    public class CanRedo : Condition
    {
        protected UndoManager undoManager;

        public CanRedo(UndoManager undoManager)
        {
            this.undoManager = undoManager;
        }

        public override bool Test()
        {
            return undoManager.CanRedo();
        }
    }
}
