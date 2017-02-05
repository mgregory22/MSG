//
// MSG/Console/Editor.cs
//

using MSG.IO;
using System;

namespace MSG.Console
{
    /// <summary>
    ///   More featureful console input editor.
    /// </summary>
    public partial class Editor
    {
        protected Buffer buffer;
        protected Print print;
        protected Read read;
        protected View view;
        protected string lastPrompt;

        /// <param name="print">
        ///   Object used for printing
        /// </param>
        /// <param name="read">
        ///   Object used for reading
        /// </param>
        public Editor(Print print, Read read)
        {
            this.print = print;
            this.read = read;
        }

        /// <summary>
        ///   Gets one or more lines of input from the user.
        /// </summary>
        public string GetAndProcessKeys()
        {
            bool done = false;
            ConsoleKeyInfo keyInfo;
            while (!done)
            {
                keyInfo = read.GetNextKey();
                done = ProcessKey(keyInfo, buffer, view);
            }
            return buffer == null ? null : buffer.ToString();
        }

        /// <summary>
        ///   Override that allows heir to provide custom validation
        ///   method for when the user presses enter
        /// </summary>
        /// <param name="input">
        ///   Complete input the user entered in
        /// </param>
        virtual public bool InputIsValid(string input)
        {
            return true;
        }

        /// <summary>
        ///   Override that allows heir to provide custom validation
        ///   method for each keystroke
        /// </summary>
        /// <param name="keyInfo">
        ///   Last keystroke entered by the user within the input loop
        /// </param>
        virtual public bool KeyIsValid(ConsoleKeyInfo keyInfo)
        {
            return true;
        }

        /// <summary>
        ///   Returns the last prompt that was printed on the screen (mostly for testing).
        /// </summary>
        public string LastPrompt
        {
            get { return this.lastPrompt; }
        }

        /// <summary>
        ///   Displays the prompt and reads a string.
        /// </summary>
        /// <returns>
        ///   The string entered by the user
        /// </returns>
        virtual public string StringPrompt(string prompt = "$ ")
        {
            string s;
            this.buffer = new Buffer();
            do
            {
                PrintPrompt(prompt);
                s = GetAndProcessKeys();
            } while (!InputIsValid(s));
            return s;
        }

        public void PrintPrompt(string prompt)
        {
            print.String(prompt);
            // The view has an internal startCursorPos property
            // that needs to be set after the prompt is printed.
            this.view = new View(buffer, print);
            // For testing
            this.lastPrompt = prompt;
        }

        /// <summary>
        ///   Performs the key command action
        /// </summary>
        /// <param name="keyInfo">
        ///   Key command/input
        /// </param>
        /// <param name="buffer">
        ///   Editor buffer
        /// </param>
        /// <param name="view">
        ///   Editor console
        /// </param>
        /// <returns>
        ///   True if the user quit
        /// </returns>
        virtual public bool ProcessKey(ConsoleKeyInfo keyInfo, Buffer buffer, View view)
        {
            bool done = false;

            if (IsPrintable(keyInfo))
            {
                if (KeyIsValid(keyInfo))
                {
                    // insert any of the printable keys
                    buffer.InsertChar(keyInfo.KeyChar);
                    view.RedrawEditor(buffer.Text, buffer.Point);
                }
            }
            else if (IsShiftEnter(keyInfo))
            {
                buffer.InsertChar('\n');
                view.RedrawEditor(buffer.Text, buffer.Point);
            }
            else if (IsBackspace(keyInfo))
            {
                buffer.RetreatPoint();
                buffer.DeleteChar();
                view.RedrawEditor(buffer.Text, buffer.Point);
            }
            else if (IsDown(keyInfo))
            {
                int point = view.CursorDown(buffer.Point);
                buffer.MovePoint(point);
            }
            else if (IsLeft(keyInfo))
            {
                int point = view.CursorLeft(buffer.Point);
                buffer.MovePoint(point);
            }
            else if (IsRight(keyInfo))
            {
                int point = view.CursorRight(buffer.Point);
                buffer.MovePoint(point);
            }
            else if (IsUp(keyInfo))
            {
                int point = view.CursorUp(buffer.Point);
                buffer.MovePoint(point);
            }
            else if (IsDelete(keyInfo))
            {
                buffer.DeleteChar();
                view.RedrawEditor(buffer.Text, buffer.Point);
            }
            else if (IsEnd(keyInfo))
            {
                int point = view.CursorEnd();
                buffer.MovePoint(point);
            }
            else if (IsEnter(keyInfo))
            {
                // ignore enter on empty line
                if (!buffer.IsEmpty())
                {
                    view.ExitEditor();
                    done = true;
                }
            }
            else if (IsEscape(keyInfo))
            {
                buffer.Delete();
                view.ExitEditor();
                done = true;
            }
            else if (IsHome(keyInfo))
            {
                int point = view.CursorHome();
                buffer.MovePoint(point);
            }
            else if (IsCtrlLeft(keyInfo))
            {
                buffer.WordBack();
                view.RedrawEditor(buffer.Text, buffer.Point);
            }
            else if (IsCtrlRight(keyInfo))
            {
                buffer.WordForward();
                view.RedrawEditor(buffer.Text, buffer.Point);
            }
            else if (IsPause(keyInfo))
            {
                done = true;
            }
            else
            {
                // Ignore everything else
            }
            return done;
        }
    }
}
