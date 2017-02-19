//
// MSG/Console/InputPrompt.cs
//

using MSG.IO;

namespace MSG.Console
{
    /// <summary>
    /// Base class for prompt objects.  Just holds the
    /// print/read objects and prompt string.
    /// Can print the prompt and pause for keypress.
    /// </summary>
    public abstract class InputPrompt
    {
        protected Print print;
        protected string prompt;
        protected Read read;

        /// <summary>
        /// Initialize a prompt with message, print and read objects.
        /// </summary>
        /// <param name="print">Used to print the prompt</param>
        /// <param name="read">Used to read the user input</param>
        /// <param name="prompt">The prompt string to use when requesting user input</param>
        public InputPrompt(Print print, Read read, string prompt = "> ")
        {
            this.print = print;
            this.read = read;
            this.prompt = prompt;
        }

        /// <summary>
        /// Wait for key to keep the window open.
        /// </summary>
        public void Pause()
        {
            print.String("Press a key");
            read.GetNextChar(true);
        }

        /// <summary>
        /// The object used to display information to the user.
        /// </summary>
        public Print Print
        {
            get { return print; }
            set { print = value; }
        }

        /// <summary>
        /// Uses the print object to print the prompt message (without newline).
        /// </summary>
        public void PrintPrompt()
        {
            Print.String(prompt);
        }

        /// <summary>
        /// The text that prompts the user for input.
        /// </summary>
        public string Prompt
        {
            get { return prompt; }
            set { prompt = value; }
        }

        /// <summary>
        /// The object used to get user input.
        /// </summary>
        public Read Read
        {
            get { return read; }
            set { read = value; }
        }
    }
}
