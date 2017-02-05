//
// MSGTest/Console/MenuTests.cs
//

using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using MSGTest.Patterns;
using NUnit.Framework;

namespace MSGTest.Console
{
    class ToStringCountMenuItem : MenuItem
    {
        private int toStringCount = 0;

        public override string ToString(int index = 0)
        {
            toStringCount++;
            return "";
        }

        public int ToStringCount
        {
            get { return toStringCount; }
            set { toStringCount = value; }
        }

        public ToStringCountMenuItem(char k, int n, Print print, Read read, UndoManager undoManager)
            : base(k, "", new TestDialogCommand(print, read, undoManager))
        {
        }
    }

    [TestFixture]
    public class MenuCreationTests
    {
        Menu menu;
        ToStringCountMenuItem[] menuItems;

        [SetUp]
        public void Initialize()
        {
            Print print = new Print();
            Read read = new Read(print);
            UndoManager undoManager = new UndoManager();
            menuItems = new ToStringCountMenuItem[] {
                new ToStringCountMenuItem('a', 1, print, read, undoManager),
                new ToStringCountMenuItem('b', 2, print, read, undoManager),
                new ToStringCountMenuItem('c', 3, print, read, undoManager),
                new ToStringCountMenuItem('d', 4, print, read, undoManager)
            };
            CharPrompt prompt = new CharPrompt(print, read, "");
            menu = new Menu("Test Menu", menuItems, prompt);
        }

        [Test]
        public void TestAllMenuItemsAreStored()
        {
            Assert.AreEqual(menuItems.Length, menu.ItemCount);
        }

        [Test]
        public void TestValidKeyListIsCorrect()
        {
            char[] expectedList = new char[] { 'a', 'b', 'c', 'd' };
            char[] actualList = menu.ValidKeys;
            for (int i = 0; i < expectedList.Length; i++)
            {
                Assert.AreEqual(expectedList[i], actualList[i]);
            }
        }
    }

    [TestFixture]
    public class MenuDisplayTests
    {
        Menu menu;
        ToStringCountMenuItem[] menuItems;

        [SetUp]
        public void Initialize()
        {
            Print print = new Print();
            Read read = new Read(print);
            UndoManager undoManager = new UndoManager();
            menuItems = new ToStringCountMenuItem[] {
                new ToStringCountMenuItem('a', 1, print, read, undoManager),
                new ToStringCountMenuItem('b', 2, print, read, undoManager),
                new ToStringCountMenuItem('c', 3, print, read, undoManager),
                new ToStringCountMenuItem('d', 4, print, read, undoManager)
            };
            CharPrompt prompt = new CharPrompt(print, read, "");
            menu = new Menu("Test Menu", menuItems, prompt);
        }

        [Test]
        public void TestMenuTitleDisplays()
        {
            Assert.IsTrue(menu.ToString().StartsWith("Test Menu\n---------\n"));
        }

        [Test]
        public void TestToStringCallsAllMenuItemToStrings()
        {
            menu.ToString();
            foreach (ToStringCountMenuItem menuItem in menuItems)
            {
                Assert.AreEqual(1, menuItem.ToStringCount);
            }
        }
    }

    [TestFixture]
    public class MenuCommandTests
    {
        public class CommandCountMenuItem : MenuItem
        {
            protected TestDialogCommand testDialogCommand;

            public CommandCountMenuItem(char keystroke, TestDialogCommand testDialogCommand, string description)
                : base(keystroke, description, testDialogCommand)
            {
                this.testDialogCommand = testDialogCommand;
            }

            public virtual TestDialogCommand TestDialogCommand
            {
                get { return testDialogCommand; }
                set { testDialogCommand = value; }
            }

            /// <summary>
            ///   Performs the action associated with the menu item.
            /// </summary>
            public override void Do()
            {
                this.dialogCommand.Do();
            }

            /// <summary>
            ///   Returns the string representation of the menu item.  If the
            ///   description is long enough to be wrapped, an index less than
            ///   LineCount can be given to retrieve the associated line of text.
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public override string ToString(int index = 0)
            {
                return testDialogCommand.IsEnabled() ? lines[index] : "";
            }
        }

        Print print;
        Read read;
        UndoManager undoManager;
        Menu menu;
        CommandCountMenuItem[] menuItems;

        [SetUp]
        public void Initialize()
        {
            print = new Print();
            read = new Read(print);
            undoManager = new UndoManager();
            menuItems = new CommandCountMenuItem[] {
                new CommandCountMenuItem('0', new TestDialogCommand(print, read, undoManager), "Item 0"),
                new CommandCountMenuItem('1', new TestDialogCommand(print, read, undoManager), "Item 1"),
                new CommandCountMenuItem('2', new TestDialogCommand(print, read, undoManager), "Item 2"),
                new CommandCountMenuItem('3', new TestDialogCommand(print, read, undoManager), "Item 3")
            };
            CharPrompt prompt = new CharPrompt(print, read, "");
            menu = new Menu("Test Menu", menuItems, prompt);
        }

        [Test]
        public void TestCorrectMenuItemIsExecutedWhenKeystrokeIsSent()
        {
            MenuItem m = menu.FindMatchingItem('1');
            m.Do();
            Assert.AreEqual(1, menuItems[1].TestDialogCommand.doCount);
            // Might as well check that ONLY the correct command was executed
            Assert.AreEqual(0, menuItems[0].TestDialogCommand.doCount);
            Assert.AreEqual(0, menuItems[2].TestDialogCommand.doCount);
            Assert.AreEqual(0, menuItems[3].TestDialogCommand.doCount);
        }

        [Test]
        public void TestFindReturnsMatchingMenuItem()
        {
            Assert.AreEqual(menuItems[1], menu.FindMatchingItem('1'));
        }
    }
}
