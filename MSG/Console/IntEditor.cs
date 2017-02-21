//
// MSG/Console/IntEditor.cs
//

using MSG.IO;
using System;
using System.Text.RegularExpressions;

namespace MSG.Console
{
    public class IntEditor : Editor
    {
        protected static Regex intRe = new Regex(@"^-?[0-9]+$");

        /// <summary>
        /// True if input is an integer.
        /// </summary>
        public override bool InputIsValid(Io io, string input)
        {
            // If input is null, then user hit escape, so stop looping.
            if (input == null) {
                return true;
            }
            return intRe.IsMatch(input);
        }

        public override bool KeyIsValid(ConsoleKeyInfo keyInfo)
        {
            return (keyInfo.Key >= ConsoleKey.D0 && keyInfo.Key <= ConsoleKey.D9)
                || keyInfo.Key == ConsoleKey.OemMinus;
        }

        /// <summary>
        /// Prints a prompt and gets an int from the user
        /// </summary>
        /// <param name="prompt">The prompt string</param>
        public int? IntPrompt(Io io, string prompt = "# ")
        {
            string input = base.StringPrompt(io, prompt);
            if (input == null) {
                return null;
            }
            return Convert.ToInt32(input);
        }

        public int? RangePrompt(Io io, int min, int max, string prompt = "# ")
        {
            int? input;
            do {
                input = IntPrompt(io, prompt);
                if (input == null) {
                    return null;
                }
                if (input < min || input > max) {
                    io.print.StringNL(String.Format("Enter a number between {0} and {1} (Esc to quit)", min, max));
                }
            } while (input < min || input > max);
            return input;
        }
    }
}
