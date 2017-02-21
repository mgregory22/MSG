//
// MSGTest/Patterns/TestCmd.cs
//

using MSG.Patterns;

namespace MSGTest.Patterns
{
    public class TestCmd : UnCmd
    {
        public int doCount;
        public int undoCount;

        public TestCmd()
        {
        }

        public override Result Do()
        {
            doCount++;
            return Cmd.OK;
        }

        public override Result Undo()
        {
            undoCount++;
            return Cmd.OK;
        }
    }
}
