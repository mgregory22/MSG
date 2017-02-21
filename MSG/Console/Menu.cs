//
// MSG/Console/Menu.cs
//

using MSG.IO;
using MSG.Patterns;
using MSG.Types.String;
using System;
using System.Collections.Generic;

namespace MSG.Console
{
    /// <summary>
    /// The responsibilities of this class are:
    /// 1. Organize menu items together to create a menu and generate its display.
    /// 2. Receive a user-entered shortcut and perform the action(s)
    ///    of the corresponding menu item.
    /// </summary>
    public class Menu
    {
        private List<MenuItem> menuItems;
        private CharPrompt charPrompt;
        private Io io;
        private string title;
        private MenuItem helpItem;
        private const char helpKey = '?';

        /// <summary>
        /// Initializes a new menu with the given array of menu items.  The items
        /// are displayed in the order given in the array.
        /// </summary>
        /// <param name="title">The menu title displayed in prompts and help</param>
        /// <param name="charPrompt">User prompt and input object</param>
        public Menu(Io io, string title, CharPrompt charPrompt)
        {
            this.title = title;
            this.charPrompt = charPrompt;
            this.io = io;
            this.menuItems = new List<MenuItem>();

            // Menus ALWAYS have a help item for printing item
            // keystrokes and descriptions.
            this.helpItem = new MenuItem(
                    helpKey,
                    "Help",
                    Cond.ALWAYS,
                    new HelpDlgCmd(io, this)
                );
        }

        public void AddMenuItem(MenuItem menuItem)
        {
            this.menuItems.Add(menuItem);
        }

        public void AddMenuItems(MenuItem[] menuItems)
        {
            this.menuItems.AddRange(menuItems);
        }

        /// <summary>
        /// Find the menu item that matches the keystroke.
        /// </summary>
        /// <param name="keystroke"></param>
        /// <returns>
        /// The menu item of the matching keystroke or null if there was no match.
        /// </returns>
        public MenuItem FindMatchingItem(char keystroke)
        {
            foreach (MenuItem menuItem in menuItems) {
                if (menuItem.Enabled && menuItem.DoesMatch(keystroke)) {
                    return menuItem;
                }
            }
            // The help item is ALWAYS available, unless the helpKey
            // keystroke has been redefined.
            if (keystroke == helpKey) {
                return helpItem;
            }
            return null;
        }

        /// <summary>
        /// Returns the number of items in the menu.
        /// </summary>
        public int ItemCount {
            get { return menuItems.Count; }
        }

        /// <summary>
        /// Performs the menu input/action loop.
        /// </summary>
        public Cmd.Result Loop(Io io)
        {
            Cmd.Result result;

            do {
                // Print a newline to break things up a bit
                io.print.Newline();

                // Print menu title
                io.print.StringNL(this.Title);

                // Prompt ! for keystroke
                charPrompt.ValidList = ValidKeys;
                char c = charPrompt.PromptAndInput(io);

                // Find menu item that matches keystroke
                MenuItem m = this.FindMatchingItem(c);

                // Execute command
                result = m.Do(io);

                // If result should be printed, print it
                if (result.IsPrintable) {
                    io.print.StringNL(result.ToString());
                }
            } while (!result.IsReturnable);

            // Only allow UpMenu to go up a single menu
            if (result.GetType() == typeof(Cmd.UpMenu)) {
                result = Cmd.OK;
            }

            return result;
        }

        /// <summary>
        /// String to use as the prompt.
        /// </summary>
        public string Prompt {
            get { return charPrompt.Prompt; }
            set { charPrompt.Prompt = value; }
        }

        /// <summary>
        /// Title of the menu.
        /// </summary>
        public string Title {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        /// Returns a string of the entire menu.
        /// </summary>
        /// <returns>String representation of the menu</returns>
        public override string ToString()
        {
            string s = "";
            foreach (MenuItem menuItem in menuItems) {
                if (menuItem.Enabled) {
                    s += menuItem.ToString();
                }
            }
            s += helpItem.ToString();
            return s;
        }

        /// <summary>
        /// Returns the list of keystrokes that have corresponding
        /// menu items.
        /// </summary>
        public char[] ValidKeys {
            get {
                char[] validKeys = new char[menuItems.Count + 1];
                for (int i = 0; i < menuItems.Count; i++) {
                    validKeys[i] = menuItems[i].Keystroke;
                }
                // Help is ALWAYS available
                validKeys[menuItems.Count] = helpKey;
                return validKeys;
            }
            private set { }
        }
    }
}
