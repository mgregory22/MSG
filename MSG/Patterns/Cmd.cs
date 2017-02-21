//
// MSG/Patterns/Cmd.cs
//

namespace MSG.Patterns
{
    /// <summary>
    /// Command design pattern.
    /// </summary>
    public abstract partial class Cmd
    {
        #region Cmd.Result class

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

        #region Stock Cmd.Result classes

        public class Ok : Result
        {
        }

        public class CantDo : Result
        {
            public CantDo()
            {
                message = "Cmd cannot be done\n";
            }
        }

        public class UpMenu : Result
        {
            public UpMenu()
            {
                isReturnable = true;
            }
        }

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
        #endregion

        #region Stock Cmd.Result objects

        // Use these to signal some basic condition to avoid creating and
        // garbage-collecting a bunch of little objects.
        public static Ok OK = new Ok();
        public static CantDo CANTDO = new CantDo();
        public static UpMenu UPMENU = new UpMenu();
        public static QuitProg QUITPROG = new QuitProg();

        #endregion

        #endregion

        /// <summary>
        /// Should perform the command with parameters initialized in the constructor.
        /// </summary>
        public virtual Result Do()
        {
            return Cmd.CANTDO;
        }
    }

    #region Stock commands

    public class Nothing : Cmd
    {
        public override Result Do()
        {
            return Cmd.OK;
        }
    }

    public class UpMenu : Cmd
    {
        public override Result Do()
        {
            return Cmd.UPMENU;
        }
    }

    public class QuitProg : Cmd
    {
        public override Result Do()
        {
            return Cmd.QUITPROG;
        }
    }

    #endregion

}
