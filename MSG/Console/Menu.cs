//
// MSG/Console/Menu.cs
//

using MSG.IO;
using MSG.Types.String;
using System;
using System.Collections.Generic;

namespace MSG.Console
{
    /// <summary>
    ///   The responsibilities of this class are:
    ///   1. Organize menu items together to create a menu and generate its display.
    ///   2. Receive a user-entered shortcut and perform the action(s)
    ///      of the corresponding menu item.
    /// </summary>
    public class Menu
    {
        private List<MenuItem> menuItems;
        private CharPrompt charPrompt;
        private Print print;
        private Read read;
        private string title;

        /// <summary>
        ///   Initializes a new menu with the given array of menu items.  The items
        ///   are displayed in the order given in the array.
        /// </summary>
        /// <param name="menuItems"></param>
        public Menu(string title, MenuItem[] menuItems, CharPrompt charPrompt)
        {
            this.title = title;
            this.menuItems = new List<MenuItem>(menuItems);
            this.charPrompt = charPrompt;
            this.print = charPrompt.Print;
            this.read = charPrompt.Read;
        }

        public void AddMenuItem(MenuItem menuItem)
        {
            this.menuItems.Add(menuItem);
        }

        /// <summary>
        ///   Find the menu item that matches the keystroke.
        /// </summary>
        /// <param name="keystroke"></param>
        /// <returns>
        ///   The menu item of the matching keystroke or null if there was no match.
        /// </returns>
        public MenuItem FindMatchingItem(char keystroke)
        {
            foreach (MenuItem menuItem in menuItems)
            {
                if (menuItem.DoesMatch(keystroke))
                {
                    return menuItem;
                }
            }
            return null;
        }

        /// <summary>
        ///    Returns the list of menu item keystrokes.
        /// </summary>
        private char[] GetKeystrokeList()
        {
            List<char> keystrokeList = new List<char>();
            foreach (MenuItem menuItem in this.menuItems)
                keystrokeList.Add(menuItem.Keystroke);
            return keystrokeList.ToArray();
        }

        /// <summary>
        ///   Returns the number of items in the menu.
        /// </summary>
        public int ItemCount
        {
            get { return menuItems.Count; }
        }

        /// <summary>
        ///   Performs the menu input/action loop.
        /// </summary>
        public void Loop()
        {
            bool done = false;
            charPrompt.ValidList = GetKeystrokeList();
            while (!done)
            {
                // Seeing the menu every time is annoying
                //print.String(this.ToString());
                char? c = charPrompt.PromptAndInput();
                if (c == null)
                {
                    done = true;
                }
                else
                {
                    MenuItem m = this.FindMatchingItem(c.Value);
                    try
                    {
                        m.Do();
                    }
                    catch (OperationCanceledException)
                    {
                        // user has quit
                        done = true;
                    }
                    catch (InvalidOperationException ex)
                    {
                        // Non-fatal error
                        print.StringNL(ex.Message);
                    }
                    catch (ArgumentException ex)
                    {
                        // Non-fatal error
                        print.StringNL(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        // Presumably fatal error
                        print.StringNL(ex.Message);
                        done = true;
                    }
                }
                print.Newline();
            }
        }

        /// <summary>
        ///   String to use as the prompt.
        /// </summary>
        public string Prompt
        {
            get { return charPrompt.Prompt; }
            set { charPrompt.Prompt = value; }
        }

        /// <summary>
        ///   Title of the menu.
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        ///   Returns a string of the entire menu.
        /// </summary>
        /// <returns>String representation of the menu</returns>
        public override string ToString()
        {
            string s = Draw.UnderlinedText(title);
            foreach (MenuItem menuItem in menuItems)
            {
                for (int i = 0; i < menuItem.LineCount; i++)
                {
                    s += menuItem.ToString(i);
                }
            }
            return s;
        }

        /// <summary>
        ///   Returns the list of keystrokes that have corresponding
        ///   menu items.
        /// </summary>
        public char[] ValidKeys
        {
            get
            {
                char[] validKeys = new char[menuItems.Count];
                for (int i = 0; i < menuItems.Count; i++)
                {
                    validKeys[i] = menuItems[i].Keystroke;
                }
                return validKeys;
            }
            private set { }
        }
    }
}
