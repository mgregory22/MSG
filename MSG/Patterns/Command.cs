//
// MSG/Patterns/Command.cs
//

namespace MSG.Patterns
{
    /// <summary>
    /// Command design pattern.
    /// </summary>
    public abstract class Command
    {
        #region Command.Result class

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

        #region Stock Command.Result classes

        public class Ok : Result
        {
        }

        public class CannotDo : Result
        {
            public CannotDo()
            {
                message = "Command cannot be done\n";
            }
        }

        public class CannotUndo : Result
        {
            public CannotUndo()
            {
                message = "Command cannot be undone\n";
            }
        }

        public class NothingToRedo : Result
        {
            public NothingToRedo()
            {
                message = "Nothing to redo\n";
            }
        }

        public class NothingToUndo : Result
        {
            public NothingToUndo()
            {
                message = "Nothing to undo\n";
            }
        }

        public class UpMenu : Result
        {
            public UpMenu()
            {
                isReturnable = true;
            }
        }

        public class QuitProgram : Result
        {
            public QuitProgram()
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

        #region Stock Command.Result objects

        // Use these to signal some basic condition to avoid creating and
        // garbage-collecting a bunch of little objects.
        public static Ok ok = new Ok();
        public static CannotDo cannotDo = new CannotDo();
        public static CannotUndo cannotUndo = new CannotUndo();
        public static NothingToRedo nothingToRedo = new NothingToRedo();
        public static NothingToUndo nothingToUndo = new NothingToUndo();
        public static UpMenu upMenu = new UpMenu();
        public static QuitProgram quitProgram = new QuitProgram();

        #endregion

        #endregion

        /// <summary>
        /// Should perform the command with parameters initialized in the constructor.
        /// </summary>
        public virtual Result Do()
        {
            return Command.cannotDo;
        }

        /// <summary>
        /// Undoes a previously performed command.
        /// </summary>
        public virtual Result Undo()
        {
            return Command.cannotUndo;
        }
    }

    #region Stock commands

    public class Nothing : Command
    {
        public override Result Do()
        {
            return Command.ok;
        }
    }

    public class UpMenu : Command
    {
        public override Result Do()
        {
            return Command.upMenu;
        }
    }

    public class QuitProgram : Command
    {
        public override Result Do()
        {
            return Command.quitProgram;
        }
    }

    #endregion

}
