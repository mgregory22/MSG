//
// MSGTest/Patterns/TestDialogCommand.cs
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

        public override Result Do()
        {
            doCount++;
            return new Ok();
        }

        public override Result Undo()
        {
            undoCount++;
            return new Ok();
        }
    }
}
