//
// MSG/Patterns/Cmds/QuitProg.cs
//

namespace MSG.Patterns.Cmds
{
    /// <summary>
    /// Stock command for quitting the prog
    /// </summary>
    public class QuitProg : Cmd
    {
        public override Result Do()
        {
            return QUITPROG;
        }
    }
}
