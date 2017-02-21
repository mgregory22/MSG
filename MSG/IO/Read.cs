//
// MSG/IO/Read.cs
//

using System;

namespace MSG.IO
{
    /// <summary>
    /// Encapsulates the raw reading from the console.  This was intended
    /// to be easy to derive from and override its methods for testing
    /// and for reading from other devices and situations.
    /// </summary>
    public class Read
    {
        /// <summary>
        /// Reads a character from the console (immediately, without the user
        /// hitting enter).
        /// </summary>
        /// <param name="print">
        /// Optional output object for echo
        /// </param>
        /// <returns>
        /// Key the user typed
        /// </returns>
        virtual public char GetNextChar(Print print = null)
        {
            ConsoleKeyInfo key = System.Console.ReadKey(true);
            if (print != null) {
                print.Char(key.KeyChar);
            }
            return key.KeyChar;
        }

        /// <summary>
        /// Reads a key from the console (immediately, without the user
        /// hitting enter).
        /// </summary>
        /// <param name="print">
        /// Optional output object for echo
        /// </param>
        /// <returns>
        /// Key the user typed
        /// </returns>
        virtual public ConsoleKeyInfo GetNextKey(Print print = null)
        {
            ConsoleKeyInfo key = System.Console.ReadKey(true);
            if (print != null) {
                print.Char(key.KeyChar);
            }
            return key;
        }
    }
}
