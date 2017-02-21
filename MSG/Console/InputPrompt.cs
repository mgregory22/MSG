//
// MSG/Console/InputPrompt.cs
//

using MSG.IO;
using MSG.Patterns;

namespace MSG.Console
{
    /// <summary>
    /// Base class for prompt objects.  Just holds the
    /// print/read objects and prompt string.
    /// Can print the prompt and pause for keypress.
    /// </summary>
    public abstract class InputPrompt : Cmd
    {
        protected string prompt;
        protected string pausePrompt;

        /// <summary>
        /// Initialize a prompt with message, print and read objects.
        /// </summary>
        /// <param name="io">Used for input and output</param>
        /// <param name="prompt">The prompt string to use when requesting user input</param>
        public InputPrompt(string prompt = "> ")
        {
            this.prompt = prompt;
            this.pausePrompt = "Press a key";
        }

        /// <summary>
        /// Wait for key to keep the window open.
        /// </summary>
        public void Pause(Io io)
        {
            io.print.String(PausePrompt);
            io.read.GetNextChar(io.print);
        }

        public string PausePrompt {
            get {
                return pausePrompt;
            }
            set {
                pausePrompt = value;
            }
        }

        /// <summary>
        /// Uses the print object to print the prompt message (without newline).
        /// </summary>
        public void PrintPrompt(Print print)
        {
            print.String(prompt);
        }

        /// <summary>
        /// The text that prompts the user for input.
        /// </summary>
        public string Prompt
        {
            get { return prompt; }
            set { prompt = value; }
        }
    }
}
