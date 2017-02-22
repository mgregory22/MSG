//
// MSGTest/Patterns/TestDlgCmd.cs
//

using MSG.IO;
using MSG.Patterns;

namespace MSGTest.Patterns
{
    public class TestDlgCmd : DlgCmd
    {
        TestCmd testCmd;

        public TestDlgCmd(Io io, UndoAndRedo undoAndRedo)
            : base(io, undoAndRedo)
        {
        }

        public override Cmd Create()
        {
            testCmd = new TestCmd();
            return testCmd;
        }

        public int DoCount {
            get {
                return (testCmd == null) ? 0 : testCmd.doCount;
            }
        }

        public int UndoCount {
            get {
                return (testCmd == null) ? 0 : testCmd.undoCount;
            }
        }
    }
}
