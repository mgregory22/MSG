//
// MSGTest/Patterns/TestCommand.cs
//

using MSG.Patterns;

namespace MSGTest.Patterns
{
    public class TestCommand : Command
    {
        public int doCount;
        public int undoCount;

        public TestCommand()
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
