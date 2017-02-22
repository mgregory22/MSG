//
// MSG/Patterns/Cmd/Results/QuitProg.cs
//

namespace MSG.Patterns
{
    public abstract partial class Cmd
    {
        /// <summary>
        /// Stock result class
        /// </summary>
        public class QuitProg : Result
        {
            public QuitProg()
            {
                isReturnable = true;
            }

            public override bool IsReturnable {
                get {
                    // Always return true to break out of all menus no
                    // matter how deeply nested.
                    return true;
                }
            }
        }

        public static QuitProg QUITPROG = new QuitProg();
    }
}

