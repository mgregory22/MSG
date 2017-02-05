//
// MSGTest/Patterns/TestCommand.cs
//

using MSG.IO;
using MSG.Patterns;

namespace MSGTest.Patterns
{
    public class TestDialogCommand : DialogCommand
    {
        public int doCount;
        public int undoCount;

        public TestDialogCommand(Print print, Read read, UndoManager undoManager)
            : base(print, read, undoManager)
        {
        }

        public override void Do()
        {
            doCount++;
        }

        public override void Undo()
        {
            undoCount++;
        }

        public override bool IsEnabled()
        {
            return true;
        }
    }
}
