//
// MSGTest/Patterns/TestDlgCmd.cs
//

using MSG.IO;
using MSG.Patterns;

namespace MSGTest.Patterns
{
    public class TestDlgCmd : DlgUnCmd
    {
        public int doCount;
        public int undoCount;

        public TestDlgCmd(Io io, UndoManager undoManager)
            : base(io, undoManager)
        {
        }

        public override UnCmd Create()
        {
            return new TestCmd();
        }

        public override Cmd.Result Do(Io io, UndoManager undoManager)
        {
            doCount++;
            return Cmd.OK;
        }

        public override Cmd.Result Undo(Io io, UndoManager undoManager)
        {
            undoCount++;
            return Cmd.OK;
        }
    }
}
