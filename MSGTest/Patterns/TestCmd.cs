//
// MSGTest/Patterns/TestCmd.cs
//

using MSG.Patterns;

namespace MSGTest.Patterns
{
    public class TestCmd : Cmd
    {
        public int doCount;
        public int undoCount;

        public TestCmd()
        {
        }

        public override Result Do()
        {
            doCount++;
            return MSG.Patterns.Cmd.OK;
        }

        public override Result Undo()
        {
            undoCount++;
            return MSG.Patterns.Cmd.OK;
        }
    }
}
