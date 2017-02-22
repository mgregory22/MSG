//
// MSGTest/Console/MenuItemTests.cs
//

using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using MSGTest.Patterns;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MSGTest.Console
{
    [TestFixture]
    public class MenuItemTests
    {
        /*
         * Member variables
         */
        Io io;
        UndoAndRedo undoAndRedo;

        MenuItem menuItem;
        string testDesc = "Test";
        char testKey = 't';
        int testMaxWidth = 40;
        TestDlgCmd testDlgCmd;

        [SetUp]
        public void Initialize()
        {
            io = new Io(new Print(), new Read());
            undoAndRedo = new UndoAndRedo();
            testDlgCmd = new TestDlgCmd(io, undoAndRedo);
            menuItem = new MenuItem(testKey, testDesc, Cond.ALWAYS, testDlgCmd);
            menuItem.MaxWidth = testMaxWidth;
        }

        [Test]
        public void TestDescriptionSaves()
        {
            Assert.AreEqual(testDesc, menuItem.Description);
        }

        [Test]
        public void TestKeystrokeSaves()
        {
            Assert.AreEqual(testKey, menuItem.Keystroke);
        }

        [Test]
        public void TestBasicKeyToStringIsCorrect()
        {
            Assert.AreEqual("[t] Test\n", menuItem.ToString());
        }

        [Test]
        public void TestMaxWidthIsStored()
        {
            Assert.AreEqual(testMaxWidth, menuItem.MaxWidth);
        }

        [Test]
        public void TestActionIsExecutedWhenCorrectKeystrokeIsSent()
        {
            if (menuItem.DoesMatch(testKey)) {
                menuItem.Do(io);
            }
            Assert.AreEqual(1, testDlgCmd.DoCount);
        }

        [Test]
        public void TestTrueIsReturnedWhenCorrectKeystrokeIsSent()
        {
            Assert.IsTrue(menuItem.DoesMatch(testKey));
        }

        [Test]
        public void TestActionIsNotExecutedWhenWrongKeystrokesAreSent()
        {
            // Try every key but the real one
            for (char k = ' '; k < '~'; k++) {
                if (k != testKey) {
                    menuItem.DoesMatch(k);
                }
            }
            // Assert Execute() was never executed
            Assert.AreEqual(0, testDlgCmd.DoCount);
        }

        [Test]
        public void TestFalseIsReturnedWhenWrongKeystrokeIsSent()
        {
            bool result = false;
            // Try every key but the real one
            for (char k = ' '; k < '~'; k++) {
                if (k != testKey) {
                    result |= menuItem.DoesMatch(k);
                }
            }
            Assert.IsFalse(result);
        }
    }

    [TestFixture]
    public class LongMenuItemTests
    {
        Io io;
        UndoAndRedo undoAndRedo;
        MenuItem menuItem;
        string testDesc = "Test of a very long description to test wrapping";
        char testKey = 't';
        int testMaxWidth = 40;

        /// <remarks>
        /// Assumes string ends with '\n'
        /// </remarks>
        public static void AssertAllLinesNoLongerThanMaxWidth(string s, int maxWidth)
        {
            int sol = 0;
            int eol = s.IndexOf('\n', sol);
            while (eol > -1) {
                Assert.IsTrue(eol - sol <= maxWidth, "Expected: {0} <= {1}, Actual: {0} > {1}", eol - sol, maxWidth);
                sol = eol + 1;
                eol = s.IndexOf('\n', sol);
            }
        }

        [SetUp]
        public void Initialize()
        {
            io = new Io(new Print(), new Read());
            undoAndRedo = new UndoAndRedo();
            TestDlgCmd testDlgCmd = new TestDlgCmd(io, undoAndRedo);
            menuItem = new MenuItem(testKey, testDesc, Cond.ALWAYS, testDlgCmd);
            menuItem.MaxWidth = testMaxWidth;
        }

        [Test]
        public void TestToStringIsNoLongerThanMaxWidth()
        {
            AssertAllLinesNoLongerThanMaxWidth(menuItem.ToString(), testMaxWidth);
        }

        [Test]
        public void TestToStringWrapsAtWordBoundary()
        {
            string testOutput = "[T] Test of a very long description to\n";
            Assert.AreEqual(testOutput.Length, menuItem.ToStringByLine(0).Length);
        }

        [Test]
        public void TestToStringReturnsSecondLineOfWrappedText()
        {
            string testOutput = "test wrapping\n";
            Assert.IsTrue(menuItem.ToStringByLine(1).EndsWith(testOutput), "Expected: \"{0}\".EndsWith(\"{1}\")", menuItem.ToStringByLine(1), testOutput);
        }
    }

    [TestFixture]
    public class VeryLongMenuItemTests
    {
        Io io;
        UndoAndRedo undoAndRedo;
        MenuItem menuItem;
        string testDescLine1 = "Test of a very long test string that";
        string testDescLine2 = "will be wrapped into three lines that";
        string testDescLine3 = "hopefully will make sense because I plan";
        string testDescLine4 = "on doing this right!";
        char testKey = 't';
        int testMaxWidth = 40;

        private int GetKeystrokePrefixLen()
        {
            string itemString = menuItem.ToString();
            int leftBracketPos = itemString.IndexOf('[');
            // The right bracket key might be the actual keystroke, so start searching for the right bracket 2 chars after the left bracket position
            int rightBracketPos = itemString.IndexOf(']', leftBracketPos + 2);
            // There's a space after the right bracket, then the description starts
            return rightBracketPos + 1;
        }

        [SetUp]
        public void Initialize()
        {
            io = new Io(new Print(), new Read());
            undoAndRedo = new UndoAndRedo();
            TestDlgCmd testDlgCmd = new TestDlgCmd(io, undoAndRedo);
            menuItem = new MenuItem(testKey, testDescLine1 + " " + testDescLine2 + " " + testDescLine3 + " " + testDescLine4, Cond.ALWAYS, testDlgCmd);
            menuItem.MaxWidth = testMaxWidth;
        }

        [Test]
        public void TestWrapSplit()
        {
            List<string> lines = new List<string>();
            menuItem.WrapSplit(menuItem.Description, testMaxWidth, lines);
            LongMenuItemTests.AssertAllLinesNoLongerThanMaxWidth(testDescLine1, testMaxWidth);
            LongMenuItemTests.AssertAllLinesNoLongerThanMaxWidth(testDescLine2, testMaxWidth);
            LongMenuItemTests.AssertAllLinesNoLongerThanMaxWidth(testDescLine3, testMaxWidth);
            LongMenuItemTests.AssertAllLinesNoLongerThanMaxWidth(testDescLine4, testMaxWidth);
            Assert.AreEqual(testDescLine1, lines[0]);
            Assert.AreEqual(testDescLine2, lines[1]);
            Assert.AreEqual(testDescLine3, lines[2]);
            Assert.AreEqual(testDescLine4, lines[3]);
        }

        [Test]
        public void TestDescriptionLinesAreIndentedPastKeystroke()
        {
            string prefix = new String(' ', GetKeystrokePrefixLen());
            for (int i = 1; i < menuItem.LineCount; i++) {
                Assert.IsTrue(menuItem.ToStringByLine(i).StartsWith(prefix));
            }
        }

    }
}
