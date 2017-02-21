//
// MSG/Console/EditorAux/WordWrapper.cs
//

using MSG.IO;
using MSG.Types.Array;
using MSG.Types.String;
using System;
using System.Collections.Generic;

namespace MSG.Console.EditorAux
{
    public class WordWrapper
    {
        public enum BolPositionPreference
        {
            BeforeBol,
            AfterBol
        }

        /// <summary>
        /// The Update() method will set this to true if the number of lines
        /// displayed was reduced by the update.
        /// </summary>
        bool dewrap;
        /// <summary>
        /// Marks the buffer position of the end of each line exclusive.  So a
        /// first lineBreak of 6 would put the first char of the next line at
        /// position 6 and the first line would be length 6.
        /// </summary>
        List<int> lineBreaks;
        /// <summary>
        /// Total number of lines that have been scrolled into view since the
        /// start of editing, whether or not those lines still exist.
        /// </summary>
        int linesScrolledFromWrapping;
        /// <summary>
        /// List of line widths of usable screen space.
        /// </summary>
        EndlessArray<int> lineWidths;
        /// <summary>
        /// Turns out to be helpful to have a gutter on the right where the cursor
        /// can go at the end of a full line.
        /// </summary>
        const int rightMargin = 1;

        /// <summary>
        /// Scans the given text with respect to the given window width
        /// and determines the substring coordinates needed to wordwrap
        /// the text in the window.
        /// </summary>
        /// <param name="text">
        /// Text to scan for line breaks
        /// </param>
        /// <param name="point">
        /// The current cursor in the text
        /// </param>
        /// <param name="lineWidths">
        /// The maximum width of each line in characters.  If there are
        /// more lines than there are entries in lineWidths, then the
        /// last entry is used for subsequent lines.
        /// </param>
        public WordWrapper(string text, EndlessArray<int> lineWidths)
        {
            this.dewrap = false;
            this.lineBreaks = new List<int>();
            this.lineWidths = lineWidths;
            this.linesScrolledFromWrapping = 0;
            Update(text);
        }

        /// <summary>
        /// Adds a line break to the list.
        /// </summary>
        /// <param name="lineBreak">
        /// The position of the char right after the line break.
        /// </param>
        private void AddLineBreak(int lineBreak)
        {
            lineBreaks.Add(lineBreak);
            linesScrolledFromWrapping = Math.Max(linesScrolledFromWrapping, lineBreaks.Count - 1);
        }

        /// <summary>
        /// Converts a point in the buffer to a console cursor position.
        /// </summary>
        /// <param name="point">
        /// Buffer point
        /// </param>
        /// <param name="bolPositionPreference">
        /// The point at the beginning of a line (other than the first and
        /// last) can represent one of two cursor positions: the position
        /// at the BOL and the position after the last character of the
        /// previous line.  If this flag is set to true, then the cursor
        /// will be set at the end of the previous line (before the soft
        /// linebreak).
        /// </param>
        /// <returns>
        /// The (left, top) position within the editor (eg (0,0) is the first column on the
        /// first line, etc)
        /// </returns>
        public ConsolePos BufferPointToEditorPos(int point, BolPositionPreference bolPositionPreference)
        {
            // Find the line index of the point
            int line = lineBreaks.FindIndex(
                lineBreak => point < lineBreak
            );
            // If the cursor is sitting at the end of the text, then top will be the last line,
            // which will be missed by the FindIndex() call above because then point == lineBreak
            if (line == -1)
            {
                line = lineBreaks.Count - 1;
            }
            ConsolePos editorPos;
            // The left coordinate = the point - the start of the line it's on
            editorPos.left = point - GetLineStart(line);
            // Cursor wraps around if it's at the end of a full line
            if (editorPos.left >= lineWidths[line])
            {
                editorPos.left = 0;
                line++;
            }
            // Respect the BOL position preference
            if (bolPositionPreference == BolPositionPreference.BeforeBol
                    && editorPos.left == 0
                    && line > 0)
            {
                line--;
                editorPos.left = GetLineBreak(line) - GetLineStart(line) - 1;
            }
            editorPos.top = line;
            return editorPos;
        }

        /// <summary>
        /// Returns the number of lines of wrapped text.
        /// </summary>
        public int Count
        {
            get { return lineBreaks.Count; }
        }

        /// <summary>
        /// This is set to true when the last operation reduced the
        /// number of lines to display (ie when the previously last
        /// line needs to be erased).
        /// </summary>
        public bool Dewrap
        {
            get { return dewrap; }
        }

        /// <summary>
        /// Returns the maximum valid value of the point.
        /// </summary>
        public int GetBufferLen()
        {
            return lineBreaks[lineBreaks.Count - 1];
        }

        /// <summary>
        /// Returns the text of the given wrapped line.
        /// </summary>
        public string GetLine(string text, int wrappedLineIndex)
        {
            int startIndex = GetLineStart(wrappedLineIndex);
            return text.Substring(startIndex, GetLineBreak(wrappedLineIndex) - startIndex);
        }

        /// <summary>
        /// Returns the break position of the line given by
        /// _lineIndex_, which is one past the last character
        /// on the line.
        /// </summary>
        public int GetLineBreak(int wrappedLineIndex)
        {
            return lineBreaks[wrappedLineIndex];
        }

        /// <summary>
        /// Returns the length of the given wrapped line.
        /// </summary>
        public int GetLineLen(int wrappedLineIndex)
        {
            return GetLineBreak(wrappedLineIndex) - GetLineStart(wrappedLineIndex);
        }

        /// <summary>
        /// Returns the position of the start of the line
        /// given by _lineIndex_.
        /// </summary>
        public int GetLineStart(int wrappedLineIndex)
        {
            // The start of the first line is zero.  The start of
            // any other line is the previous break.
            return wrappedLineIndex == 0 ? 0 : lineBreaks[wrappedLineIndex - 1];
        }

        /// <summary>
        /// For symmetry
        /// </summary>
        public bool IsFirstLine(int wrappedLineIndex)
        {
            return wrappedLineIndex == 0;
        }

        /// <summary>
        /// Returns true if the given line index refers to the last
        /// wrapped line of input.
        /// </summary>
        public bool IsLastLine(int wrappedLineIndex)
        {
            return wrappedLineIndex == lineBreaks.Count - 1;
        }

        /// <summary>
        /// Returns true if the given line index refers to an existent
        /// line.
        /// </summary>
        public bool IsValidLine(int wrappedLineIndex)
        {
            return wrappedLineIndex >= 0 && wrappedLineIndex < lineBreaks.Count;
        }

        /// <summary>
        /// Returns true if the length of the wrapped line text is equal
        /// to the width of the window.
        /// </summary>
        private bool IsWindowLineCompletelyFilled(int wrappedLineLen, int wrappedLineIndex, int rightMargin)
        {
            return wrappedLineLen == this.lineWidths[wrappedLineIndex] - rightMargin;
        }

        /// <summary>
        /// The maximum number of lines that were needed for editing, minus one
        /// </summary>
        public int LinesScrolledFromWrapping
        {
            get { return linesScrolledFromWrapping; }
        }

        /// <summary>
        /// Performs the word wrapping algorithm and creates
        /// a list of word break positions for each extra line
        /// that needs to be displayed.
        /// </summary>
        /// <param name="text">
        /// Text to scan for line breaks
        /// </param>
        public void Update(string text)
        {
            // Shortcuts
            int textLen = text.Length;
            // Set dewrap flag at the end of this method if the user deleted
            // enough characters to reduce the number of lines (ie the lost
            // line needs to be erased).
            int previousLineCnt = lineBreaks.Count;
            // Clear previous calculations
            lineBreaks.Clear();
            // Start index of first line is ALWAYS 0
            int lineStart = 0;
            // Save the cursor position of last scanned word break
            int lastWordBreak = 0;
            // Scan through all points
            for (int point = 0; point < textLen; point++)
            {
                // Save the latest line break candidate
                if (Scan.IsPointOnWordBeginning(text, point))
                {
                    lastWordBreak = point;
                }
                // Length of line so far in the scan
                int lineLen = point - lineStart;
                // Break the line unconditionally at a line break char
                if (Scan.IsPointAtAHardLineReturn(text, point))
                {
                    // Advance cursor past line return
                    point = Scan.SkipHardReturn(text, point);
                    // Break line for hard return
                    AddLineBreak(point);
                    // Next start of line
                    lineStart = point;
                    // Save word break position
                    lastWordBreak = lineStart;
                }
                // When the window edge is reached, break at a word if possible
                else if (IsWindowLineCompletelyFilled(lineLen, lineBreaks.Count, rightMargin))
                {
                    // If a word just ended, break right here before the space
                    if (Scan.IsPointJustAfterAWord(text, point))
                    {
                        ; // All set
                    }
                    // Found word break during scan?
                    else if (lastWordBreak > lineStart)
                    {
                        // Back up cursor to last word break
                        point = lastWordBreak;
                        // Set eol at word break
                        lineLen = point - lineStart;
                    }
                    // Add the line indexes
                    AddLineBreak(point);
                    // Next bol
                    lineStart = point;
                    // Save word break position
                    lastWordBreak = lineStart;
                }
                // End of scanning loop
            }
            // Add a break for the end of the text
            AddLineBreak(textLen);
            // If the number of lines decreased since the last update, flag it
            dewrap = lineBreaks.Count < previousLineCnt;
        }
    }
}
