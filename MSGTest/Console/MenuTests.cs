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

        public override string ToString()
        {
            toStringCount++;
            return "";
        }

        public int ToStringCount
        {
            get { return toStringCount; }
            set { toStringCount = value; }
        }

        public ToStringCountMenuItem(char k, int n, Io io, UndoManager undoManager)
            : base(k, "", Cond.ALWAYS, new TestDlgCmd(io, undoManager))
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
            Io io = new Io(new Print(), new Read());
            UndoManager undoManager = new UndoManager();
            menuItems = new ToStringCountMenuItem[] {
                new ToStringCountMenuItem('a', 1, io, undoManager),
                new ToStringCountMenuItem('b', 2, io, undoManager),
                new ToStringCountMenuItem('c', 3, io, undoManager),
                new ToStringCountMenuItem('d', 4, io, undoManager)
            };
            CharPrompt prompt = new CharPrompt("");
            menu = new Menu(io, "Test Menu", prompt);
            menu.AddMenuItems(menuItems);
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
            Io io = new Io(new Print(), new Read());
            UndoManager undoManager = new UndoManager();
            menuItems = new ToStringCountMenuItem[] {
                new ToStringCountMenuItem('a', 1, io, undoManager),
                new ToStringCountMenuItem('b', 2, io, undoManager),
                new ToStringCountMenuItem('c', 3, io, undoManager),
                new ToStringCountMenuItem('d', 4, io, undoManager)
            };
            CharPrompt prompt = new CharPrompt("");
            menu = new Menu(io, "Test Menu", prompt);
            menu.AddMenuItems(menuItems);
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
            protected TestDlgCmd testDlgCmd;

            public CommandCountMenuItem(char keystroke, string description, Cond enableCond, TestDlgCmd testDlgCmd)
                : base(keystroke, description, enableCond, testDlgCmd)
            {
                this.testDlgCmd = testDlgCmd;
            }

            public virtual TestDlgCmd TestDlgCmd
            {
                get { return testDlgCmd; }
                set { testDlgCmd = value; }
            }

            /// <summary>
            /// Performs the action associated with the menu item.
            /// </summary>
            public override Cmd.Result Do(Io io)
            {
                return this.dlgCmd.Do(io);
            }

            /// <summary>
            /// Returns the string representation of the menu item.  If the
            /// description is long enough to be wrapped, an index less than
            /// LineCount can be given to retrieve the associated line of text.
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public override string ToStringByLine(int index = 0)
            {
                return lines[index];
            }
        }

        Io io;
        UndoManager undoManager;
        Menu menu;
        CommandCountMenuItem[] menuItems;

        [SetUp]
        public void Initialize()
        {
            io = new Io(new Print(), new Read());
            undoManager = new UndoManager();
            menuItems = new CommandCountMenuItem[] {
                new CommandCountMenuItem('0', "Item 0", Cond.ALWAYS, new TestDlgCmd(io, undoManager)),
                new CommandCountMenuItem('1', "Item 1", Cond.ALWAYS, new TestDlgCmd(io, undoManager)),
                new CommandCountMenuItem('2', "Item 2", Cond.ALWAYS, new TestDlgCmd(io, undoManager)),
                new CommandCountMenuItem('3', "Item 3", Cond.ALWAYS, new TestDlgCmd(io, undoManager))
            };
            CharPrompt prompt = new CharPrompt("");
            menu = new Menu(io, "Test Menu", prompt);
            menu.AddMenuItems(menuItems);
        }

        [Test]
        public void TestCorrectMenuItemIsExecutedWhenKeystrokeIsSent()
        {
            MenuItem m = menu.FindMatchingItem('1');
            m.Do(io);
            Assert.AreEqual(1, menuItems[1].TestDlgCmd.doCount);
            // Might as well check that ONLY the correct command was executed
            Assert.AreEqual(0, menuItems[0].TestDlgCmd.doCount);
            Assert.AreEqual(0, menuItems[2].TestDlgCmd.doCount);
            Assert.AreEqual(0, menuItems[3].TestDlgCmd.doCount);
        }

        [Test]
        public void TestFindReturnsMatchingMenuItem()
        {
            Assert.AreEqual(menuItems[1], menu.FindMatchingItem('1'));
        }
    }
}
