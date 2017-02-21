//
// MSG/IO/Io.cs
//

namespace MSG.IO
{
    public class Io
    {
        public Print print;
        public Read read;

        public Io(Print print, Read read)
        {
            this.print = print;
            this.read = read;
        }
    }
}
