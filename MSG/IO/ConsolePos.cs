//
// MSG/IO/ConsolePos.cs
//

namespace MSG.IO
{
    public struct ConsolePos
    {
        public int left, top;

        public ConsolePos(int left, int top)
        {
            this.left = left;
            this.top = top;
        }

        public void Set(int left, int top)
        {
            this.left = left;
            this.top = top;
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", this.left, this.top);
        }
    }
}
