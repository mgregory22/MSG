//
// MSGTest/Console/CharPromptTests.cs
//

using MSG.Console;
using MSGTest.IO;
using NUnit.Framework;

namespace MSGTest.Console
{

    [TestFixture]
    public class CharPromptTests
    {
        CharPrompt prompt;
        TestPrint print;
        TestRead read;

        [SetUp]
        public void SetUp()
        {
            print = new TestPrint();
            read = new TestRead(print);
            prompt = new CharPrompt(print, read);
            prompt.ValidList = new char[] { 'a', 'b', '\x1B' };
        }

        [Test]
        public void TestCharPromptInvalidatesInvalidChar()
        {
            char invalidKey = 'X';
            char validKey = 'a';
            // A valid key needs to be sent to terminate the prompt loop
            read.PushChar(invalidKey);
            read.PushChar(validKey);
            char? gotKey = prompt.PromptAndInput();
            Assert.IsNotNull(gotKey);
            Assert.AreEqual(prompt.LastPrompt + "X\n" + CharPrompt.helpMsg + "\n" + prompt.LastPrompt + "a\n", print.Output);
            // Might as well test this again
            Assert.AreEqual(validKey, gotKey);
        }

        [Test]
        public void TestCharPromptValidatesValidChar()
        {
            char validKey = 'a';
            read.PushChar(validKey);
            char? gotKey = prompt.PromptAndInput();
            Assert.IsNotNull(gotKey);
            Assert.AreEqual(prompt.LastPrompt + "a\n", print.Output);
            Assert.AreEqual(validKey, gotKey);
            print.ClearOutput();
        }

        [Test]
        public void TestCharPromptHandlesEscape()
        {
            char escapeKey = '\x1B';
            read.PushChar(escapeKey);
            char? gotKey = prompt.PromptAndInput();
            Assert.IsNull(gotKey);
        }
    }
}
