//
// MSG/Console/CharPrompt.cs
//

using MSG.IO;
using System.Linq;

namespace MSG.Console
{
    /// <summary>
    ///   Prompts the users for a char (without having to hit enter) until
    ///   the char passes validation.
    /// </summary>
    /// <remarks>
    ///   I really hate this class because all the methods are called "Prompt"
    /// </remarks>
    public class CharPrompt : InputPrompt
    {
        private char[] validList;
        public const string helpMsg = "Invalid selection. Try again. Press ? for help.";

        /// <summary>
        ///   Initialize a prompt with message, print and read objects.
        /// </summary>
        /// <param name="print">Used to print the prompt</param>
        /// <param name="read">Used to read the user input</param>
        /// <param name="prompt">The prompt string to use when requesting user input</param>
        public CharPrompt(Print print, Read read, string prompt = "! ")
            : base(print, read, prompt)
        {
        }

        /// <summary>
        ///   Does all validation on the char.
        /// </summary>
        /// <param name="c">Char to validate</param>
        /// <returns>True if char is invalid</returns>
        private bool CharIsInvalid(char c)
        {
            if (validList != null && !validList.Contains(c))
            {
                Print.StringNL(helpMsg);
                return true;
            }
            return false;
        }

        /// <summary>
        ///   Displays the prompt and reads a valid character.
        /// </summary>
        /// <returns>The char entered by the user</returns>
        public char? PromptAndInput()
        {
            char c;
            do
            {
                PrintPrompt();
                c = Read.GetNextChar();
                if (c == '\x1B') return null;
                Print.Newline();
            } while (CharIsInvalid(c));
            return c;
        }

        /// <summary>
        ///   If this property is set, the key the user enters must be
        ///   in this list, otherwise an error message will be displayed
        ///   and the user reprompted.
        /// </summary>
        public char[] ValidList
        {
            get { return validList; }
            set { validList = value; }
        }
    }
}
