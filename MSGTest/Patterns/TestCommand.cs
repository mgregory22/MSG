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

        public override void Do()
        {
            doCount++;
        }

        public override void Undo()
        {
            undoCount++;
        }
    }
}
