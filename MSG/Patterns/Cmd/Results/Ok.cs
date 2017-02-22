//
// MSG/Patterns/Cmd/Results/Ok.cs
//

namespace MSG.Patterns
{
    public abstract partial class Cmd
    {
        /// <summary>
        /// Stock result class
        /// </summary>
        public class Ok : Result
        {
        }

        public static Ok OK = new Ok();
    }
}

