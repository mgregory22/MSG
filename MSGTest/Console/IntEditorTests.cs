//
// MSGTest/Console/IntEditorTests.cs
//

using MSGTest.IO;
using NUnit.Framework;

namespace MSGTest.Console
{
    class IntEditorTests
    {
        MSG.Console.IntEditor editor;
        TestPrint print;
        TestRead read;
        int? input;

        [SetUp]
        public void SetUp()
        {
            print = new TestPrint();
            print.BufferWidth = 8;
            read = new TestRead(null);
            editor = new MSG.Console.IntEditor(print, read);
        }

        [Test]
        public void TestDigitCanBeInsertedIntoBuffer()
        {
            read.PushString("0\r");
            input = editor.IntPrompt();
            Assert.AreEqual(0, input);
        }

        [Test]
        public void TestLetterCannotBeInsertedIntoBuffer()
        {
            read.PushString("a0b\r");
            input = editor.IntPrompt();
            Assert.AreEqual(0, input);
        }

        [Test]
        public void TestMinusCanBeInsertedIntoBeginningOfBuffer()
        {
            read.PushString("-0");
            input = editor.IntPrompt();
            Assert.AreEqual(0, input);
        }

        [Test]
        public void TestEscapeAbortsInput()
        {
            // Provide some input then press Esc
            read.PushString("12");
            read.PushEscape();
            input = editor.IntPrompt();
            Assert.IsNull(input);
        }

        [Test]
        public void TestEmptyInputReturnsNull()
        {
            read.PushEnter();
            input = editor.IntPrompt();
            Assert.IsNull(input);
        }
    }
}
