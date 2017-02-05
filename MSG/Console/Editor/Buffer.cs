//
// MSG/Console/Editor/Buffer.cs
//

using MSG.Types.String;
using System;

namespace MSG.Console
{
    public partial class Editor
    {
        /// <summary>
        ///   Console editor memory buffer class.
        /// </summary>
        public class Buffer
        {
            /// <summary>
            ///   Insertion point
            /// </summary>
            int point;

            /// <summary>
            ///   Text being edited
            /// </summary>
            string text;

            /// <summary>
            ///   Initializes a buffer for creating new text.
            /// </summary>
            public Buffer()
            {
                Clear();
            }

            /// <summary>
            ///   Initializes a memory buffer for editing previously
            ///   existing text and for testing.
            /// </summary>
            /// <param name="text">
            ///   Text to edit
            /// </param>
            /// <param name="point">
            ///   Initial insertion point
            /// </param>
            public Buffer(string text, int point)
            {
                Text = text;
                Point = point;
            }

            /// <summary>
            ///   Moves the point right one character, ensuring it can go
            ///   just to the end of the text and no further.
            /// </summary>
            public void AdvancePoint()
            {
                if (point < text.Length)
                {
                    point++;
                }
            }

            /// <summary>
            ///   Removes all the text and resets the point.
            /// </summary>
            public void Clear()
            {
                text = "";
                point = 0;
            }

            /// <summary>
            ///   Obliterates the buffer to signify a cancel operation.
            /// </summary>
            public void Delete()
            {
                text = null;
            }

            /// <summary>
            ///   Deletes the character at the point.
            /// </summary>
            public void DeleteChar()
            {
                if (point < text.Length)
                {
                    text = text.Remove(point, 1);
                }
            }

            public char GetChar(int point)
            {
                return text[point];
            }

            /// <summary>
            ///   Inserts the given character at the point.
            /// </summary>
            /// <param name="c">
            ///   Character to insert
            /// </param>
            public void InsertChar(char c)
            {
                text = text.Insert(point++, c.ToString());
            }

            /// <summary>
            ///   Returns true if the text is empty.
            /// </summary>
            public bool IsEmpty()
            {
                return text.Length == 0;
            }

            /// <summary>
            ///   Moves the point to the given position.
            /// </summary>
            /// <param name="point">
            ///   New insertion point
            /// </param>
            public void MovePoint(int point)
            {
                if (point < 0)
                    throw new ArgumentOutOfRangeException("Insertion point must be nonnegative");
                if (point > Text.Length)
                    throw new ArgumentOutOfRangeException("Insertion point cannot be set past end of text");
                Point = point;
            }

            /// <summary>
            ///   Gets or sets the insertion point.
            /// </summary>
            public int Point
            {
                get { return point; }
                set { point = value; }
            }

            /// <summary>
            ///   Moves the point left one character, ensuring it can go
            ///   just to the beginning of the text and no further.
            /// </summary>
            public void RetreatPoint()
            {
                if (point > 0)
                {
                    point--;
                }
            }

            /// <summary>
            ///   Gets or sets the edited text.
            /// </summary>
            public string Text
            {
                get { return text; }
                set { text = value; }
            }

            /// <summary>
            ///   Returns the text being edited.
            /// </summary>
            public override string ToString()
            {
                return text;
            }

            /// <summary>
            ///   Moves the cursor back one word.
            /// </summary>
            public void WordBack()
            {
                while (point > 0)
                {
                    point--;
                    if (Scan.IsPointOnWordBeginning(text, point)) break;
                }
            }

            /// <summary>
            ///   Moves the cursor forward one word.
            /// </summary>
            public void WordForward()
            {
                while (point < text.Length)
                {
                    point++;
                    if (Scan.IsPointOnWordBeginning(text, point)) break;
                }
            }
        }
    }
}
