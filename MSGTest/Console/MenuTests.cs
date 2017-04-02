//
// MSGTest/Console/MenuTests.cs
//

using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using MSGTest.Patterns;
using NUnit.Framework;
using System.Collections.Generic;

namespace MSGTest.Console
{
    [TestFixture]
    public class MenuTests
    {
        [Test]
        public void TestAllMenuItemsAreStored()
        {
            Io io = new Io(new Print(), new Read());
            UndoAndRedo undoAndRedo = new UndoAndRedo();
            TestDlgCmd dummyDlgCmd = new TestDlgCmd(io, undoAndRedo);
            MenuItem[] menuItems = new MenuItem[] {
                new MenuItem('a', "a", Cond.ALWAYS, dummyDlgCmd),
                new MenuItem('b', "b", Cond.ALWAYS, dummyDlgCmd),
                new MenuItem('c', "c", Cond.ALWAYS, dummyDlgCmd),
                new MenuItem('d', "d", Cond.ALWAYS, dummyDlgCmd)
            };
            CharPrompt prompt = new CharPrompt("");
            Menu menu = new Menu(io, "Test Menu", prompt);
            menu.AddMenuItems(menuItems);

            Assert.AreEqual(menuItems.Length, menu.ItemCount);
        }

        [Test]
        public void TestValidKeyListContainsAllWhenAllEnabled()
        {
            Io io = new Io(new Print(), new Read());
            UndoAndRedo undoAndRedo = new UndoAndRedo();
            TestDlgCmd dummyDlgCmd = new TestDlgCmd(io, undoAndRedo);
            MenuItem[] menuItems = new MenuItem[] {
                new MenuItem('a', "a", Cond.ALWAYS, dummyDlgCmd),
                new MenuItem('b', "b", Cond.ALWAYS, dummyDlgCmd),
                new MenuItem('c', "c", Cond.ALWAYS, dummyDlgCmd),
                new MenuItem('d', "d", Cond.ALWAYS, dummyDlgCmd)
            };
            CharPrompt prompt = new CharPrompt("");
            Menu menu = new Menu(io, "Test Menu", prompt);
            menu.AddMenuItems(menuItems);

            List<char> expectedList = new List<char>(new char[] { 'a', 'b', 'c', 'd', '?' });
            List<char> actualList = menu.ValidKeys;
            Assert.AreEqual(expectedList.Count, actualList.Count);
            for (int i = 0; i < expectedList.Count; i++) {
                Assert.AreEqual(expectedList[i], actualList[i]);
            }
        }

        [Test]
        public void TestValidKeyListContainsOnlyEnabled()
        {
            Io io = new Io(new Print(), new Read());
            UndoAndRedo undoAndRedo = new UndoAndRedo();
            TestDlgCmd dummyDlgCmd = new TestDlgCmd(io, undoAndRedo);
            MenuItem[] menuItems = new MenuItem[] {
                new MenuItem('a', "a", Cond.ALWAYS, dummyDlgCmd),
                new MenuItem('b', "b", Cond.NEVER, dummyDlgCmd),
                new MenuItem('c', "c", Cond.ALWAYS, dummyDlgCmd),
                new MenuItem('d', "d", Cond.NEVER, dummyDlgCmd)
            };
            CharPrompt prompt = new CharPrompt("");
            Menu menu = new Menu(io, "Test Menu", prompt);
            menu.AddMenuItems(menuItems);

            List<char> expectedList = new List<char>(new char[] { 'a', 'c', '?' });
            List<char> actualList = menu.ValidKeys;
            Assert.AreEqual(expectedList.Count, actualList.Count);
            for (int i = 0; i < expectedList.Count; i++) {
                Assert.AreEqual(expectedList[i], actualList[i]);
            }
        }

        class ToStringCountMenuItem : MenuItem
        {
            private int toStringCount = 0;

            public ToStringCountMenuItem(char k, int n, Io io, UndoAndRedo undoAndRedo)
                : base(k, "", Cond.ALWAYS, new TestDlgCmd(io, undoAndRedo))
            {
            }

            public override string ToString()
            {
                toStringCount++;
                return "";
            }

            public int ToStringCount {
                get { return toStringCount; }
                set { toStringCount = value; }
            }
        }

        [Test]
        public void TestToStringCallsAllMenuItemToStrings()
        {
            Io io = new Io(new Print(), new Read());
            UndoAndRedo undoAndRedo = new UndoAndRedo();
            ToStringCountMenuItem[] menuItems = new ToStringCountMenuItem[] {
                new ToStringCountMenuItem('a', 1, io, undoAndRedo),
                new ToStringCountMenuItem('b', 2, io, undoAndRedo),
                new ToStringCountMenuItem('c', 3, io, undoAndRedo),
                new ToStringCountMenuItem('d', 4, io, undoAndRedo)
            };
            CharPrompt prompt = new CharPrompt("");
            Menu menu = new Menu(io, "Test Menu", prompt);
            menu.AddMenuItems(menuItems);

            menu.ToString();
            foreach (ToStringCountMenuItem menuItem in menuItems)
            {
                Assert.AreEqual(1, menuItem.ToStringCount);
            }
        }

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

        [Test]
        public void TestCorrectMenuItemIsExecutedWhenKeystrokeIsSent()
        {
            Io io = new Io(new Print(), new Read());
            UndoAndRedo undoAndRedo = new UndoAndRedo();
            CommandCountMenuItem[] menuItems = new CommandCountMenuItem[] {
                new CommandCountMenuItem('0', "Item 0", Cond.ALWAYS, new TestDlgCmd(io, undoAndRedo)),
                new CommandCountMenuItem('1', "Item 1", Cond.ALWAYS, new TestDlgCmd(io, undoAndRedo)),
                new CommandCountMenuItem('2', "Item 2", Cond.ALWAYS, new TestDlgCmd(io, undoAndRedo)),
                new CommandCountMenuItem('3', "Item 3", Cond.ALWAYS, new TestDlgCmd(io, undoAndRedo))
            };
            CharPrompt prompt = new CharPrompt("");
            Menu menu = new Menu(io, "Test Menu", prompt);
            menu.AddMenuItems(menuItems);

            MenuItem m = menu.FindMatchingItem('1');
            m.Do(io);
            Assert.AreEqual(1, menuItems[1].TestDlgCmd.DoCount);
            // Might as well check that ONLY the correct command was executed
            Assert.AreEqual(0, menuItems[0].TestDlgCmd.DoCount);
            Assert.AreEqual(0, menuItems[2].TestDlgCmd.DoCount);
            Assert.AreEqual(0, menuItems[3].TestDlgCmd.DoCount);
        }

        [Test]
        public void TestFindMatchingItemFindsMatchingItem()
        {
            Io io = new Io(new Print(), new Read());
            UndoAndRedo undoAndRedo = new UndoAndRedo();
            CommandCountMenuItem[] menuItems = new CommandCountMenuItem[] {
                new CommandCountMenuItem('0', "Item 0", Cond.ALWAYS, new TestDlgCmd(io, undoAndRedo)),
                new CommandCountMenuItem('1', "Item 1", Cond.ALWAYS, new TestDlgCmd(io, undoAndRedo)),
                new CommandCountMenuItem('2', "Item 2", Cond.ALWAYS, new TestDlgCmd(io, undoAndRedo)),
                new CommandCountMenuItem('3', "Item 3", Cond.ALWAYS, new TestDlgCmd(io, undoAndRedo))
            };
            CharPrompt prompt = new CharPrompt("");
            Menu menu = new Menu(io, "Test Menu", prompt);
            menu.AddMenuItems(menuItems);

            Assert.AreEqual(menuItems[0], menu.FindMatchingItem('0'));
            Assert.AreEqual(menuItems[1], menu.FindMatchingItem('1'));
            Assert.AreEqual(menuItems[2], menu.FindMatchingItem('2'));
            Assert.AreEqual(menuItems[3], menu.FindMatchingItem('3'));
        }

        [Test]
        public void TestFindMatchingItemDoesNotFindDisabledItems()
        {
            Io io = new Io(new Print(), new Read());
            UndoAndRedo undoAndRedo = new UndoAndRedo();
            CommandCountMenuItem[] menuItems = new CommandCountMenuItem[] {
                new CommandCountMenuItem('0', "Item 0", Cond.ALWAYS, new TestDlgCmd(io, undoAndRedo)),
                new CommandCountMenuItem('1', "Item 1", Cond.NEVER, new TestDlgCmd(io, undoAndRedo)),
                new CommandCountMenuItem('2', "Item 2", Cond.ALWAYS, new TestDlgCmd(io, undoAndRedo)),
                new CommandCountMenuItem('3', "Item 3", Cond.NEVER, new TestDlgCmd(io, undoAndRedo))
            };
            CharPrompt prompt = new CharPrompt("");
            Menu menu = new Menu(io, "Test Menu", prompt);
            menu.AddMenuItems(menuItems);

            Assert.IsNull(menu.FindMatchingItem('1'));
        }

        [Test]
        public void TestFindMatchingItemDoesNotFindNonexistentItems()
        {
            Io io = new Io(new Print(), new Read());
            UndoAndRedo undoAndRedo = new UndoAndRedo();
            CommandCountMenuItem[] menuItems = new CommandCountMenuItem[] {
                new CommandCountMenuItem('0', "Item 0", Cond.ALWAYS, new TestDlgCmd(io, undoAndRedo)),
                new CommandCountMenuItem('1', "Item 1", Cond.NEVER, new TestDlgCmd(io, undoAndRedo)),
                new CommandCountMenuItem('2', "Item 2", Cond.ALWAYS, new TestDlgCmd(io, undoAndRedo)),
                new CommandCountMenuItem('3', "Item 3", Cond.NEVER, new TestDlgCmd(io, undoAndRedo))
            };
            CharPrompt prompt = new CharPrompt("");
            Menu menu = new Menu(io, "Test Menu", prompt);
            menu.AddMenuItems(menuItems);

            Assert.IsNull(menu.FindMatchingItem('4'));
        }
    }
}
