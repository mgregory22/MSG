//
// MSGTest/Console/EditorTests.cs
//

using MSG.Console;
using MSGTest.IO;
using NUnit.Framework;
using System;

namespace MSGTest.Console
{
    [TestFixture]
    public partial class EditorTests
    {
        Editor editor;
        TestPrint print;
        TestRead read;
        string input;

        [SetUp]
        public void SetUp()
        {
            print = new TestPrint();
            print.BufferWidth = 8;
            read = new TestRead(null);
            editor = new Editor(print, read);
        }

        [Test]
        public void TestCharWasInsertedIntoBuffer()
        {
            read.PushString("a\r");
            input = editor.StringPrompt();
            Assert.AreEqual("a", input);
        }

        [Test]
        public void TestCharWasEchoedOnScreen()
        {
            read.PushString("a");
            input = editor.StringPrompt();
            Assert.AreEqual(editor.LastPrompt + "a", print.Output);
        }

        [Test]
        public void TestCharsWereInsertedIntoBuffer()
        {
            read.PushString("Word\r");
            input = editor.StringPrompt();
            Assert.AreEqual("Word", input);
        }

        [Test]
        public void TestCharsWereEchoedOnScreen()
        {
            read.PushString("Word");
            input = editor.StringPrompt();
            Assert.AreEqual(editor.LastPrompt + "Word", print.Output);
        }

        [Test]
        public void TestBackspace()
        {
            read.PushString("Word\b");
            input = editor.StringPrompt();
            Assert.AreEqual(editor.LastPrompt + "Word<5^0 <5^0", print.Output);
        }

        [Test]
        public void TestBackspaceAtEndOfTextToPreviousLineErasesLastCharacterOnThatLineAndPutsCursorAtTheEnd()
        {
            // Setup
            read.PushString("23456");
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushString("\b");
            editor.GetAndProcessKeys();
            Assert.AreEqual("<6^0 <6^0", print.Output);
        }

        [Test]
        public void TestBackspaceThatUnwrapsErasesSecondLineAndMovesCursorToMarginOfFirstLine()
        {
            // Setup
            //             01234567
            read.PushString("Wor wr");
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushString("\b");
            editor.GetAndProcessKeys();
            Assert.AreEqual("<6^0w   <7^0", print.Output);
        }

        [Test]
        public void TestCursorDownAtEndOfSingleLineOfInputHasNoEffect()
        {
            // Setup
            read.PushString("2345");
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushDownArrow();
            editor.GetAndProcessKeys();
            Assert.AreEqual("", print.Output);
        }

        [Test]
        public void TestCursorDownInMiddleOfSingleLineOfInputHasNoEffect()
        {
            // Setup
            read.PushString("2345");
            read.PushLeftArrow(2);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushDownArrow();
            editor.GetAndProcessKeys();
            Assert.AreEqual("", print.Output);
        }

        [Test]
        public void TestCursorDownOnFirstLineOfTwoPutsCursorDirectlyUnderOriginalPosition()
        {
            // Setup
            read.PushString("2345 0123456");
            read.PushLeftArrow(9);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushDownArrow();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<5^1", print.Output);
        }

        [Test]
        public void TestCursorDownOnSecondLineOfThreePutsCursorDirectlyUnderOriginalPosition()
        {
            // Setup
            read.PushString("2345 012345601234");
            read.PushLeftArrow(9);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushDownArrow();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<3^2", print.Output);
        }

        [Test]
        public void TestCursorDownOnEmptyLineHasNoEffect()
        {
            // Setup
            print.ClearOutput();  // clear prompt output
            // Test
            read.PushDownArrow();
            editor.StringPrompt();
            Assert.AreEqual(editor.LastPrompt, print.Output);
        }

        [Test]
        public void TestCursorEndMovesCursorToEndOfSingleLine()
        {
            // Setup
            read.PushString("2345");
            read.PushLeftArrow(3);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushEnd();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<6^0", print.Output);
        }

        [Test]
        public void TestCursorEndOnSecondLineMovesCursorToEndOfSecondLine()
        {
            // Setup
            read.PushString("2345 70123");
            read.PushLeftArrow(3);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushEnd();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<5^1", print.Output);
        }

        [Test]
        public void TestCursorEndOnFirstLineOfTwoLineEntryMovesCursorToEndOfFirstLine()
        {
            // Setup
            read.PushString("2345 012345");
            read.PushLeftArrow(9);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushEnd();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<6^0", print.Output);
        }

        [Test]
        public void TestCursorEndOnFullLinePutsCursorAtEndOfSameLine()
        {
            // Setup
            read.PushString("234 012345");
            read.PushLeftArrow(3);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushEnd();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<6^1", print.Output);
        }

        [Test]
        public void TestCursorHomeMovesCursorToStartOfSingleLine()
        {
            // Setup
            read.PushString("2345");
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushHome();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<2^0", print.Output);
        }

        [Test]
        public void TestCursorHomeOnSecondLineMovesCursorToStartOfSecondLine()
        {
            // Setup
            read.PushString("2345 70123");
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushHome();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<0^1", print.Output);
        }

        [Test]
        public void TestCursorHomeOnFirstLineOfTwoLineInputMovesCursorToStartOfFirstLine()
        {
            // Setup
            read.PushString("2345 70123");
            read.PushLeftArrow(6);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushHome();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<2^0", print.Output);
        }

        [Test]
        public void TestCursorLeftAtBeginningOfNewLineBringsCursorToEndOfPreviousLine()
        {
            // Setup
            read.PushString("2345 70");
            read.PushLeftArrow(2);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushLeftArrow();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<6^0", print.Output);
        }

        [Test]
        public void TestCursorLeftCannotMoveCursorBeforeBeginning()
        {
            read.PushRightArrow();
            editor.StringPrompt();
            Assert.AreEqual(editor.LastPrompt.Length, print.CursorLeft);
        }

        [Test]
        public void TestCursorLeftDoesNotEraseCharacters()
        {
            // Setup
            read.PushString("2345");
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushLeftArrow(3);
            editor.GetAndProcessKeys();
            Assert.AreEqual("<5^0<4^0<3^0", print.Output);
        }

        [Test]
        public void TestCursorLeftIntoFullLinePutsCursorAtEndOfLine()
        {
            // Setup
            read.PushString("23456 ");
            read.PushLeftArrow();
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushLeftArrow();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<6^0", print.Output);
        }

        [Test]
        public void TestCursorLeftWorksOnSecondLine()
        {
            // Setup
            read.PushString("2345\n01234");
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushLeftArrow();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<4^1", print.Output);
        }

        [Test]
        public void TestCursorLeftWorksOnWrappedSecondLine()
        {
            // Setup
            read.PushString("2345 7012");
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushLeftArrow();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<3^1", print.Output);
        }

        [Test]
        public void TestCursorRightBeforeEndOfFirstLineAndBeforeEndOfBufferMovesCursorOneRight()
        {
            // Setup
            read.PushString("2345");
            read.PushLeftArrow(3);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushRightArrow();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<4^0", print.Output);
        }

        [Test]
        public void TestCursorRightAtEndOfFirstLineAndBeforeEndOfBufferPutsCursorAtBeginningOfNextLine()
        {
            // Setup
            read.PushString("2345601234");
            read.PushLeftArrow(6);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushRightArrow();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<0^1", print.Output);
        }

        [Test]
        public void TestCursorRightCannotMoveCursorPastEndOfEmptyBuffer()
        {
            read.PushRightArrow();
            editor.StringPrompt();
            Assert.AreEqual(editor.LastPrompt.Length, print.CursorLeft);
        }

        [Test]
        public void TestCursorRightCannotMoveCursorPastEndOfWrappingBuffer()
        {
            // Setup
            read.PushString("2345601234");
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushRightArrow();
            editor.GetAndProcessKeys();
            Assert.AreEqual(5, print.CursorLeft);
        }

        [Test]
        public void TestCursorUpInMiddleOfSecondLineOfTwoPutsCursorDirectlyAboveOriginalPosition()
        {
            // Setup
            read.PushString("2345 0123456");
            read.PushLeftArrow(4);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushUpArrow();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<3^0", print.Output);
        }

        [Test]
        public void TestCursorUpInMiddleOfSecondLineOfTwoPutsCursorDirectlyAboveOriginalPosition2()
        {
            // Setup
            read.PushString("234 0123 56");
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushUpArrow();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<5^0", print.Output);
        }

        [Test]
        public void TestCursorUpInMiddleOfThirdLineOfThreePutsCursorDirectlyAboveOriginalPosition()
        {
            // Setup
            read.PushString("2345 012345601234");
            read.PushLeftArrow(2);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushUpArrow();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<3^1", print.Output);
        }

        [Test]
        public void TestCursorUpToShorterLinesAndBackDownToOriginalLinePutsCursorInOriginalColumn()
        {
            // Setup
            read.PushString("2345 01234 012345 0123456");
            read.PushLeftArrow(1);
            read.PushUpArrow(2);
            read.PushDownArrow();
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushDownArrow();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<6^3", print.Output);
        }

        [Test]
        public void TestCursorUpToShorterLinesAndBackDownToOriginalLinePutsCursorInOriginalColumn2()
        {
            // Setup
            read.PushString("2345 01234 012345 0123456");
            read.PushUpArrow(2);
            read.PushDownArrow();
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushDownArrow();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<7^3", print.Output);
        }

        [Test]
        public void TestCursorUpToTheRightOfLastCharInFirstLinePutsCursorAfterLastCharOfFirstLine()
        {
            // Setup
            read.PushString("23 012345");
            read.PushLeftArrow(1);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushUpArrow();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<4^0", print.Output);
        }

        [Test]
        public void TestCursorUpUnderLastCharInFirstLinePutsCursorOnLastCharOfFirstLine()
        {
            // Setup
            read.PushString("2345 70123456");
            read.PushLeftArrow(2);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushUpArrow();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<6^0", print.Output);
        }

        [Test]
        public void TestCursorWordLeftAtBeginningOfInputHasNoEffect()
        {
            // Setup
            //             01234567
            read.PushString("23 45");
            read.PushLeftArrow(5);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushLeftArrow(1, ConsoleModifiers.Control);
            editor.GetAndProcessKeys();
            Assert.AreEqual("", print.Output);
        }

        [Test]
        public void TestCursorWordLeftToBeginningOfInputMovesCursorOneWordLeft()
        {
            // Setup
            //             01234567
            read.PushString("23 45");
            read.PushLeftArrow(1);
            read.PushLeftArrow(1, ConsoleModifiers.Control);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushLeftArrow(1, ConsoleModifiers.Control);
            editor.GetAndProcessKeys();
            Assert.AreEqual("<2^0", print.Output);
        }

        [Test]
        public void TestCursorWordLeftToMiddleOfLineMovesCursorOneWordLeft()
        {
            // Setup
            //             012345601234567
            read.PushString("23 45 12 23 3");
            read.PushLeftArrow(1);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushLeftArrow(1, ConsoleModifiers.Control);
            editor.GetAndProcessKeys();
            Assert.AreEqual("<4^1", print.Output);
        }

        [Test]
        public void TestCursorWordRightAtBeginningOfInputMovesCursorOneWordRight()
        {
            // Setup
            //             01234567
            read.PushString("23 45");
            read.PushLeftArrow(5);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushRightArrow(1, ConsoleModifiers.Control);
            editor.GetAndProcessKeys();
            Assert.AreEqual("<5^0", print.Output);
        }

        [Test]
        public void TestCursorWordRightOnLastWordMovesCursorToEndOfLine()
        {
            // Setup
            //             01234567
            read.PushString("23 45");
            read.PushLeftArrow(2);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushRightArrow(1, ConsoleModifiers.Control);
            editor.GetAndProcessKeys();
            Assert.AreEqual("<7^0", print.Output);
        }

        [Test]
        public void TestCursorWordRightToMiddleOfLineMovesCursorOneWordRight()
        {
            // Setup
            //             012345601234567
            read.PushString("23 45 12 23 3");
            read.PushLeftArrow(7);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushRightArrow(1, ConsoleModifiers.Control);
            editor.GetAndProcessKeys();
            Assert.AreEqual("<4^1", print.Output);
        }

        [Test]
        public void TestEscapeEditorReturnsNullBuffer()
        {
            read.PushString("234\x1B");
            string buffer = editor.StringPrompt();
            editor.GetAndProcessKeys();
            Assert.IsNull(buffer);
        }

        [Test]
        public void TestEscapeEditorOnLinesOtherThanTheLastPutsCursorAfterAllTheEnteredLines()
        {
            // Setup
            read.PushString("2345 70 23456701 34 67012 456");
            read.PushLeftArrow(19);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushEscape();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<0^5\n", print.Output);
        }

        [Test]
        public void TestExitEditorOnLinesOtherThanTheLastPutsCursorAfterAllTheEnteredLines()
        {
            // Setup
            read.PushString("2345 70 23456701 34 67012 456");
            read.PushLeftArrow(19);
            editor.StringPrompt();
            print.ClearOutput();
            // Test
            read.PushEnter();
            editor.GetAndProcessKeys();
            Assert.AreEqual("<0^5\n", print.Output);
        }

        [Test]
        public void TestInsertDoesNotThrowExceptionWhenTextWraps()
        {
            Assert.DoesNotThrow(() => read.PushString("2345 1234"), editor.StringPrompt());
        }

        [Test]
        public void TestInsertLineThatNeedsToBeWrapped()
        {
            read.PushString("War wrap");
            editor.StringPrompt();
            Assert.AreEqual(editor.LastPrompt + "War <5^0 w<6^0  wrap", print.Output);
        }

        [Test]
        public void TestInsertOnce()
        {
            read.PushString("a");
            editor.StringPrompt();
            Assert.AreEqual(editor.LastPrompt + "a", print.Output);
        }

        [Test]
        public void TestInsertTwice()
        {
            read.PushString("ab");
            editor.StringPrompt();
            Assert.AreEqual(editor.LastPrompt + "ab", print.Output);
        }
    }
}
