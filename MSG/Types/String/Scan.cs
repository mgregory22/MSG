//
// MSG/Types/String/Scan.cs
//

namespace MSG.Types.String
{
    /// <summary>
    /// String scanning utility procedures
    /// </summary>
    public class Scan
    {
        /// <summary>
        /// Returns true if the point is on a LF or CR char.
        /// </summary>
        public static bool IsPointAtAHardLineReturn(string text, int point)
        {
            return text[point] == '\n' || text[point] == '\r';
        }

        /// <summary>
        /// Returns true if the point is just after a word
        /// (ie point is on a space just after a nonspace).
        /// </summary>
        public static bool IsPointJustAfterAWord(string text, int point)
        {
            if (point == 0) return true;
            return !char.IsWhiteSpace(text[point - 1]) && char.IsWhiteSpace(text[point]);
        }

        /// <summary>
        /// Returns true if the point is on the beginning
        /// of a word (ie point is on a nonspace just after
        /// a space).
        /// </summary>
        public static bool IsPointOnWordBeginning(string text, int point)
        {
            if (point == 0) return true;
            return char.IsWhiteSpace(text[point - 1]) && !char.IsWhiteSpace(text[point]);
        }

        /// <summary>
        /// Given that the point is on a newline character, returns
        /// the index of the next character that is not that newline.
        /// If the point is not on a newline, it returns the same point.
        /// </summary>
        public static int SkipHardReturn(string text, int point)
        {
            // Skip only a single newline.
            // Works whether the newline is \n, \r, \r\n, or \n\r
            bool newlineSkipped = false;
            if (point == text.Length) return point;
            if (text[point] == '\n')
            {
                point++;
                if (point == text.Length)
                    return point;
                newlineSkipped = true;
            }
            if (text[point] == '\r')
            {
                point++;
                if (point == text.Length)
                    return point;
            }
            if (!newlineSkipped && text[point] == '\n')
            {
                point++;
                if (point == text.Length)
                    return point;
            }
            return point;
        }

        /// <summary>
        /// Given that the point is on a whitespace character, returns
        /// the index of the next character that is not whitespace.
        /// </summary>
        public static int SkipWhiteSpace(string text, int point)
        {
            if (point == text.Length) return point;
            while (char.IsWhiteSpace(text[point]))
            {
                point++;
                if (point == text.Length)
                    return point;
            }
            return point;
        }

    }
}
