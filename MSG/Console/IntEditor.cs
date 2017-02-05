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
        ///   Initialize print and read objects.
        /// </summary>
        /// <param name="print">Used to print the prompt</param>
        /// <param name="read">Used to read the user input</param>
        public IntEditor(Print print, Read read)
            : base(print, read)
        {
        }

        /// <summary>
        ///   True if input is an integer.
        /// </summary>
        public override bool InputIsValid(string input)
        {
            return intRe.IsMatch(input);
        }

        public override bool KeyIsValid(ConsoleKeyInfo keyInfo)
        {
            return (keyInfo.Key >= ConsoleKey.D0 && keyInfo.Key <= ConsoleKey.D9)
                || keyInfo.Key == ConsoleKey.OemMinus;
        }

        /// <summary>
        ///   Prints a prompt and gets an int from the user
        /// </summary>
        /// <param name="prompt">The prompt string</param>
        public int? IntPrompt(string prompt = "# ")
        {
            string input = base.StringPrompt(prompt);
            if (input == null) return null;
            return Convert.ToInt32(input);
        }

        public int? RangePrompt(int min, int max, string prompt = "# ")
        {
            int? input;
            do
            {
                input = IntPrompt(prompt);
                if (input == null) return null;
                if (input < min || input > max)
                    print.StringNL(String.Format("Enter a number between {0} and {1} (Esc to quit)", min, max));
            } while (input < min || input > max);
            return input;
        }
    }
}
