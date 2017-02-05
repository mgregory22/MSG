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
        Print print;
        Read read;
        UndoManager undoManager;

        MenuItem menuItem;
        string testDesc = "Test";
        char testKey = 't';
        int testMaxWidth = 40;
        TestDialogCommand testDialogCommand;

        [SetUp]
        public void Initialize()
        {
            print = new Print();
            read = new Read(print);
            undoManager = new UndoManager();
            testDialogCommand = new TestDialogCommand(print, read, undoManager);
            menuItem = new MenuItem(testKey, testDesc, testDialogCommand);
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
            Assert.AreEqual("[t] Test", menuItem.ToString());
        }

        [Test]
        public void TestMaxWidthIsStored()
        {
            Assert.AreEqual(testMaxWidth, menuItem.MaxWidth);
        }

        [Test]
        public void TestActionIsExecutedWhenCorrectKeystrokeIsSent()
        {
            if (menuItem.DoesMatch(testKey))
                menuItem.Do();
            Assert.AreEqual(1, testDialogCommand.doCount);
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
            for (char k = ' '; k < '~'; k++)
            {
                if (k != testKey)
                {
                    menuItem.DoesMatch(k);
                }
            }
            // Assert Execute() was never executed
            Assert.AreEqual(0, testDialogCommand.doCount);
        }

        [Test]
        public void TestFalseIsReturnedWhenWrongKeystrokeIsSent()
        {
            bool result = false;
            // Try every key but the real one
            for (char k = ' '; k < '~'; k++)
            {
                if (k != testKey)
                {
                    result |= menuItem.DoesMatch(k);
                }
            }
            Assert.IsFalse(result);
        }
    }

    [TestFixture]
    public class LongMenuItemTests
    {
        Print print;
        Read read;
        UndoManager undoManager;
        MenuItem menuItem;
        string testDesc = "Test of a very long description to test wrapping";
        char testKey = 't';
        int testMaxWidth = 40;

        public static void AssertStringLengthIsLessThanOrEqualToMaxWidth(string s, int maxWidth)
        {
            Assert.IsTrue(s.Length <= maxWidth, "Expected: {0} <= {1}, Actual: {0} > {1}", s.Length, maxWidth);
        }

        [SetUp]
        public void Initialize()
        {
            print = new Print();
            read = new Read(print);
            undoManager = new UndoManager();
            TestDialogCommand testDialogCommand = new TestDialogCommand(print, read, undoManager);
            menuItem = new MenuItem(testKey, testDesc, testDialogCommand);
            menuItem.MaxWidth = testMaxWidth;
        }

        [Test]
        public void TestToStringIsNoLongerThanMaxWidth()
        {
            AssertStringLengthIsLessThanOrEqualToMaxWidth(menuItem.ToString(), testMaxWidth);
        }

        [Test]
        public void TestToStringWrapsAtWordBoundary()
        {
            string testOutput = "[T] Test of a very long description to";
            Assert.AreEqual(testOutput.Length, menuItem.ToString().Length);
        }

        [Test]
        public void TestToStringReturnsSecondLineOfWrappedText()
        {
            string testOutput = "test wrapping";
            Assert.IsTrue(menuItem.ToString(1).EndsWith(testOutput), "Expected: \"{0}\".EndsWith(\"{1}\")", menuItem.ToString(1), testOutput);
        }
    }

    [TestFixture]
    public class VeryLongMenuItemTests
    {
        Print print;
        Read read;
        UndoManager undoManager;
        MenuItem menuItem;
        string testDescLine1 = "Test of a very long test string that";
        string testDescLine2 = "will be wrapped into three lines that";
        string testDescLine3 = "hopefully will make sense because I plan";
        string testDescLine4 = "on doing this right!";
        char testKey = 't';
        int testMaxWidth = 40;

        private int GetKeystrokePrefixLen()
        {
            string line1 = menuItem.ToString(0);
            int leftBracketPos = line1.IndexOf('[');
            // The right bracket key might be the actual keystroke, so start searching for the right bracket 2 chars after the left bracket position
            int rightBracketPos = line1.IndexOf(']', leftBracketPos + 2);
            // There's a space after the right bracket, then the description starts
            return rightBracketPos + 1;
        }

        [SetUp]
        public void Initialize()
        {
            print = new Print();
            read = new Read(print);
            undoManager = new UndoManager();
            TestDialogCommand testDialogCommand = new TestDialogCommand(print, read, undoManager);
            menuItem = new MenuItem(testKey, testDescLine1 + " " + testDescLine2 + " " + testDescLine3 + " " + testDescLine4, testDialogCommand);
            menuItem.MaxWidth = testMaxWidth;
        }

        [Test]
        public void TestWrapSplit()
        {
            List<string> lines = new List<string>();
            menuItem.WrapSplit(menuItem.Description, testMaxWidth, lines);
            LongMenuItemTests.AssertStringLengthIsLessThanOrEqualToMaxWidth(testDescLine1, testMaxWidth);
            LongMenuItemTests.AssertStringLengthIsLessThanOrEqualToMaxWidth(testDescLine2, testMaxWidth);
            LongMenuItemTests.AssertStringLengthIsLessThanOrEqualToMaxWidth(testDescLine3, testMaxWidth);
            LongMenuItemTests.AssertStringLengthIsLessThanOrEqualToMaxWidth(testDescLine4, testMaxWidth);
            Assert.AreEqual(testDescLine1, lines[0]);
            Assert.AreEqual(testDescLine2, lines[1]);
            Assert.AreEqual(testDescLine3, lines[2]);
            Assert.AreEqual(testDescLine4, lines[3]);
        }

        [Test]
        public void TestDescriptionLinesAreIndentedPastKeystroke()
        {
            string prefix = new String(' ', GetKeystrokePrefixLen());
            for (int i = 1; i < menuItem.LineCount; i++)
            {
                Assert.IsTrue(menuItem.ToString(i).StartsWith(prefix));
            }
        }

    }
}
