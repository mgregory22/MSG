//
// MSG/Patterns/Cmds/UpMenu.cs
//

namespace MSG.Patterns.Cmds
{
    /// <summary>
    /// Stock command for navigating up to a parent menu
    /// </summary>
    public class UpMenu : Cmd
    {
        public override Result Do()
        {
            return Patterns.Cmd.UPMENU;
        }
    }

}
