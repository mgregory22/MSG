//
// MSG/Console/CharPrompt.cs
//

using MSG.IO;
using System.Collections.Generic;
using System.Linq;

namespace MSG.Console
{
    /// <summary>
    /// Prompts the users for a char (without having to hit enter) until
    /// the char passes validation.
    /// </summary>
    public class CharPrompt : InputPrompt
    {
        public const string helpMsg = "Error: Invalid selection. Press ? for help.";

        /// <summary>
        /// Initialize a prompt with message, print and read objects.
        /// </summary>
        /// <param name="print">Used to print the prompt</param>
        /// <param name="read">Used to read the user input</param>
        /// <param name="prompt">The prompt string to use when requesting user input</param>
        public CharPrompt(string prompt = "! ")
            : base(prompt)
        {
        }

        /// <summary>
        /// Does all validation on the char.
        /// </summary>
        /// <param name="c">Char to validate</param>
        /// <param name="errors">Output object for error messages</param>
        /// <returns>True if char is invalid</returns>
        private bool CharIsInvalid(char c, List<char> validKeys, Print errors)
        {
            if (validKeys != null && !validKeys.Contains(c)) {
                errors.StringNL(helpMsg);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Displays the prompt and reads a valid character.
        /// </summary>
        /// <param name="io">Object for I/O</param>
        /// <param name="validKeys">Optional list of valid keys.
        /// If not provided all keys are valid.</param>
        /// <returns>The char entered by the user</returns>
        public char PromptAndInput(Io io, List<char> validKeys = null)
        {
            char c;
            do {
                PrintPrompt(io.print);
                c = io.read.GetNextChar(io.print);
                io.print.Newline();
            } while (CharIsInvalid(c, validKeys, io.print));
            return c;
        }
    }
}
