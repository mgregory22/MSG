//
// MSG/Types/String/Draw.cs
//

using System.Text;

namespace MSG.Types.String
{
    /// <summary>
    /// Screen drawing utility procedures.
    /// </summary>
    public class Draw
    {
        /// <summary>
        /// Returns a line of dashes width long and interspersed with
        /// digits every 10 dashes.
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        public static string Ruler(int width)
        {
            StringBuilder s = new StringBuilder(width);
            s.Append('-', width);
            for (int i = 1; i <= width / 10; i++)
            {
                s[i * 10 - 1] = (i % 10).ToString()[0];
            }
            return s.ToString();
        }

        /// <summary>
        /// Returns a string consisting of the given text, a newline, and
        /// a line of dashes exactly as long as the text.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UnderlinedText(string text)
        {
            return text + '\n' + new string('-', text.Length) + '\n';
        }
    }
}
