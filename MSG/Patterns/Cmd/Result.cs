//
// MSG/Patterns/Cmd/Result.cs
//

namespace MSG.Patterns
{
    public abstract partial class Cmd
    {
        /// <summary>
        /// Return status of Do() and Undo()
        /// </summary>
        public class Result
        {
            protected bool isReturnable = false;
            protected string message = "";

            public Result()
            {
            }

            public Result(string message) : this()
            {
                this.message = message;
            }

            /// <summary>
            /// If ToString() returns a message that should be printed.
            /// </summary>
            public virtual bool IsPrintable {
                get {
                    return message != "";
                }
            }

            /// <summary>
            /// If the menu loop should be broken out of and this result
            /// returned to the previous menu or driver.
            /// </summary>
            /// <returns></returns>
            public virtual bool IsReturnable {
                get {
                    return isReturnable;
                }
            }

            public override string ToString()
            {
                return message;
            }
        }
    }
}
