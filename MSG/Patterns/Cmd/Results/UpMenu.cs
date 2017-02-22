//
// MSG/Patterns/Cmd/Results/UpMenu.cs
//

namespace MSG.Patterns
{
    public abstract partial class Cmd
    {
        /// <summary>
        /// Stock result class
        /// </summary>
        public class UpMenu : Result
        {
            public UpMenu()
            {
                isReturnable = true;
            }
        }

        public static UpMenu UPMENU = new UpMenu();
    }
}

