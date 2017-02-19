//
// MSG/IO/Print.cs
//

namespace MSG.IO
{
    /// <summary>
    /// Encapsulates the raw printing to the console.  This was intended
    /// to be easy to derive from and override its methods for testing
    /// and for printing to other devices and situations.
    /// </summary>
    public class Print
    {
        /// <summary>
        /// Returns the size of the console buffer.
        /// </summary>
        virtual public int BufferWidth
        {
            get { return System.Console.BufferWidth; }
            set { System.Console.BufferWidth = value; }
        }

        /// <summary>
        /// Prints a char to the console.
        /// </summary>
        /// <param name="c">
        /// Char to print
        /// </param>
        virtual public void Char(char c)
        {
            if (c != '\0') {
                System.Console.Write(c);
            }
        }

        /// <summary>
        /// Prints a char and newline to the console.
        /// </summary>
        /// <param name="c">
        /// Char to print
        /// </param>
        virtual public void CharNL(char c)
        {
            Char(c);
            Newline();
        }

        /// <summary>
        /// Returns the cursor left coordinate.
        /// </summary>
        virtual public int CursorLeft
        {
            get { return System.Console.CursorLeft; }
            set { System.Console.CursorLeft = value; }
        }

        /// <summary>
        /// Moves or returns the cursor position.
        /// </summary>
        /// <param name="pos">
        /// The cursor position
        /// </param>
        virtual public ConsolePos CursorPos
        {
            get { return new ConsolePos(System.Console.CursorLeft, System.Console.CursorTop); }
            set { System.Console.SetCursorPosition(value.left, value.top); }
        }

        /// <summary>
        /// Returns the cursor top coordinate.
        /// </summary>
        virtual public int CursorTop
        {
            get { return System.Console.CursorTop; }
            set { System.Console.CursorTop = value; }
        }

        /// <summary>
        /// Shows/Hides or returns cursor visibility.
        /// </summary>
        virtual public bool IsCursorVisible
        {
            get { return System.Console.CursorVisible; }
            set { System.Console.CursorVisible = value; }
        }

        /// <summary>
        /// Prints one or more newlines to the console.
        /// </summary>
        virtual public void Newline(int n = 1)
        {
            for (int i = 0; i < n; i++) {
                System.Console.WriteLine();
            }
        }

        /// <summary>
        /// Sets the cursor position
        /// </summary>
        /// <param name="left">
        /// Cursor left coordinate
        /// </param>
        /// <param name="top">
        /// Cursor top coordinate
        /// </param>
        virtual public void SetCursorPos(int left, int top)
        {
            System.Console.CursorLeft = left;
            System.Console.CursorTop = top;
        }

        /// <summary>
        /// Prints a string to the console.
        /// </summary>
        /// <param name="s">
        /// String to print
        /// </param>
        virtual public void String(string s)
        {
            System.Console.Write(s);
        }

        /// <summary>
        /// Prints a string and newline to the console.
        /// </summary>
        /// <param name="s">
        /// String to print
        /// </param>
        virtual public void StringNL(string s)
        {
            String(s);
            Newline();
        }
    }
}
