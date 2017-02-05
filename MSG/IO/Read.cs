//
// MSG/IO/Read.cs
//

using System;

namespace MSG.IO
{
    /// <summary>
    ///   Encapsulates the raw reading from the console.  This was intended
    ///   to be easy to derive from and override its methods for testing
    ///   and for reading from other devices and situations.
    /// </summary>
    public class Read
    {
        protected Print print;

        /// <summary>
        ///   The read object needs the ability to output the stuff read.
        /// </summary>
        /// <param name="print">
        ///   Everything read will be printed with this object, unless that is
        ///   disabled by setting print to null.
        /// </param>
        public Read(Print print)
        {
            this.print = print;
        }

        /// <summary>
        ///   Reads a character from the console (immediately, without the user
        ///   hitting enter).
        /// </summary>
        /// <param name="intercept">
        ///   Suppresses output
        /// </param>
        /// <returns>
        ///   Key the user typed
        /// </returns>
        virtual public char GetNextChar(bool intercept = false)
        {
            ConsoleKeyInfo key = System.Console.ReadKey(true);
            if (!intercept && print != null) print.Char(key.KeyChar);
            return key.KeyChar;
        }

        /// <summary>
        ///   Reads a key from the console (immediately, without the user
        ///   hitting enter).
        /// </summary>
        /// <param name="intercept">
        ///   Suppresses output
        /// </param>
        /// <returns>
        ///   Key the user typed
        /// </returns>
        virtual public ConsoleKeyInfo GetNextKey(bool intercept = false)
        {
            ConsoleKeyInfo key = System.Console.ReadKey(true);
            if (!intercept && print != null) print.Char(key.KeyChar);
            return key;
        }
    }
}
