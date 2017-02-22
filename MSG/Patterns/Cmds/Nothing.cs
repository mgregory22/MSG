//
// MSG/Patterns/Cmds/Nothing.cs
//

namespace MSG.Patterns.Cmds
{
    /// <summary>
    /// Stock command for doing nothing
    /// </summary>
    public class Nothing : Cmd
    {
        public override Result Do()
        {
            return Patterns.Cmd.OK;
        }
    }

}
