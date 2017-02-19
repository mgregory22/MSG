//
// MSG/Types/String/Format.cs
//

using System.Text;

namespace MSG.Types.String
{
    /// <summary>
    /// String formatting procedures
    /// </summary>
    public class Format
    {
        public static string ToLiteral(string input)
        {
            var sb = new StringBuilder();
            foreach (var c in input)
            {
                switch (c)
                {
                    case '\b':
                        sb.Append(@"\b");
                        break;
                    case '\t':
                        sb.Append(@"\t");
                        break;
                    case '\r':
                        sb.Append(@"\r");
                        break;
                    case '\n':
                        sb.Append(@"\n");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }

        public static string Padding(int length)
        {
            return new string(' ', length);
        }
    }
}
