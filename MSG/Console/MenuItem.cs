//
// MSG/Console/MenuItem.cs
//

using MSG.IO;
using MSG.Patterns;
using System;
using System.Collections.Generic;

namespace MSG.Console
{
    /// <summary>
    ///   The responsibilities of this class are:
    ///   1. Generate the display of a shortcut and description.
    ///   2. Receive a user-entered shortcut and if it matches the 
    ///      shortcut defined by the menu item, perform the action(s)
    ///      stated in the description.  The action can be a function
    ///      or submenu.
    /// </summary>
    public class MenuItem
    {
        protected char keystroke;
        protected string description;
        protected DialogCommand dialogCommand;
        protected int maxWidth;
        protected List<string> lines;

        /// <summary>
        ///   Initializes a menu item object.
        /// </summary>
        /// <param name="keystroke">
        ///   Keystroke to activate command.
        /// </param>
        /// <param name="description">
        ///   Description of the command.
        /// </param>
        /// <param name="dialogCommand">
        ///   Command to be performed by the Do() call.
        /// </param>
        /// <param name="enabler">
        ///   Functoid to determine whether to enable the menu item.
        /// </param>
        public MenuItem(char keystroke, string description, DialogCommand dialogCommand)
        {
            this.keystroke = keystroke;
            this.description = description;
            this.dialogCommand = dialogCommand;
            this.maxWidth = 80;
            this.lines = new List<string>();
            UpdateLines();
        }

        /// <summary>
        ///   An object that encapsulates the action to perform when the menu item 
        ///   is selected.
        /// </summary>
        public virtual DialogCommand DialogCommand
        {
            get { return dialogCommand; }
            set { dialogCommand = value; }
        }

        /// <summary>
        ///   Name or short description of the menu item.
        /// </summary>
        public virtual string Description
        {
            get { return description; }
            set
            {
                description = value;
                UpdateLines();
            }
        }

        /// <summary>
        ///   Performs the action associated with the menu item.
        /// </summary>
        public virtual void Do()
        {
            this.dialogCommand.Do();
        }

        /// <summary>
        ///   True if the given keystroke matches the menu item keystroke.
        /// </summary>
        /// <param name="keystroke"></param>
        /// <returns>
        ///   True if there was a match and the action was executed, whether
        ///   the action was successful or not.
        /// </returns>
        public bool DoesMatch(char keystroke)
        {
            return keystroke == this.keystroke;
        }

        /// <summary>
        ///   The keystroke to activate the menu item.
        /// </summary>
        public virtual char Keystroke
        {
            get { return keystroke; }
            set
            {
                keystroke = value;
                UpdateLines();
            }
        }

        /// <summary>
        ///   The number of lines in the description when it's word
        ///   wrapped according to the MaxWidth property.
        /// </summary>
        public virtual int LineCount
        {
            get { return lines.Count; }
        }

        /// <summary>
        ///   The amount of horizontal space available to print the
        ///   description.
        /// </summary>
        public virtual int MaxWidth
        {
            get { return maxWidth; }
            set
            {
                maxWidth = value;
                UpdateLines();
            }
        }

        /// <summary>
        ///   Returns the string representation of the menu item.  If the
        ///   description is long enough to be wrapped, an index less than
        ///   LineCount can be given to retrieve the associated line of text.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual string ToString(int index = 0)
        {
            return dialogCommand.IsEnabled() ? lines[index] + "\n" : "";
        }

        /// <summary>
        ///   Creates the display text of the menu item.  Takes bracketed keystroke
        ///   and description, calculates line breaks and indents, and stores result
        ///   in lines[].
        /// </summary>
        private void UpdateLines()
        {
            lines.Clear();
            string k = String.Format("[{0}] ", keystroke.ToString());
            // Indent any wrapped description lines
            string p = new String(' ', k.Length);
            int maxDescWidth = maxWidth - p.Length;
            string d = String.Format("{0}", description);

            if (d.Length <= maxDescWidth)
            {
                lines.Add(k + d);
            }
            else
            {
                // Wrap the description
                WrapSplit(d, maxDescWidth, lines);
                // Insert the key and indent prefixes into the description lines
                lines[0] = k + lines[0];
                for (int i = 1; i < lines.Count; i++)
                {
                    lines[i] = p + lines[i];
                }
            }
        }

        /// <summary>
        ///   Splits a string (s) based on line length (maxWidth) and word wrap
        ///   and inserts the lines into the given string list (lines).
        /// </summary>
        /// <param name="s"></param>
        /// <param name="maxWidth"></param>
        /// <param name="lines"></param>
        /// <remarks>
        ///   This method has nothing to do with menu items per se and should
        ///   probably be moved to a more general lib.
        /// </remarks>
        public virtual void WrapSplit(string s, int maxWidth, List<string> lines)
        {
            while (s.Length > maxWidth)
            {
                // Find the position of the line break
                int wrapPos = s.LastIndexOf(' ', maxWidth);
                // Add the front of the string to the line list
                lines.Add(s.Substring(0, wrapPos));
                // Remove the front of the string
                s = s.Remove(0, wrapPos + 1);
            }
            lines.Add(s);
        }
    }

}
