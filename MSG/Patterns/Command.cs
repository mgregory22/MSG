//
// MSG/Patterns/Command.cs
//

using System;

namespace MSG.Patterns
{
    /// <summary>
    ///   Command design pattern.
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        ///   Should perform the command with parameters initialized in the constructor.
        /// </summary>
        public abstract void Do();

        /// <summary>
        ///   Undoes a previously performed command.
        /// </summary>
        public virtual void Undo()
        {
            throw new NotSupportedException("This operation cannot be undone");
        }
    }
}
