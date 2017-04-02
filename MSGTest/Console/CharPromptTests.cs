//
// MSGTest/Console/CharPromptTests.cs
//

using MSG.Console;
using MSG.IO;
using MSGTest.IO;
using NUnit.Framework;
using System.Collections.Generic;

namespace MSGTest.Console
{

    [TestFixture]
    public class CharPromptTests
    {
        [Test]
        public void TestCharPromptInvalidatesInvalidChar()
        {
            TestPrint print = new TestPrint();
            TestRead read = new TestRead();
            Io io = new Io(print, read);
            CharPrompt prompt = new CharPrompt();
            List<char> validKeys = new List<char>(new char[] { 'a', 'b', '\x1B' });

            char invalidKey = 'X';
            char validKey = 'a';
            // A valid key needs to be sent to terminate the prompt loop
            read.PushChar(invalidKey);
            read.PushChar(validKey);
            char gotKey = prompt.PromptAndInput(io, validKeys);
            Assert.AreEqual(prompt.Prompt + "X\n" + CharPrompt.helpMsg + "\n" + prompt.Prompt + "a\n", print.Output);
            // Might as well test this again
            Assert.AreEqual(validKey, gotKey);
        }

        [Test]
        public void TestCharPromptValidatesValidChar()
        {
            TestPrint print = new TestPrint();
            TestRead read = new TestRead();
            Io io = new Io(print, read);
            CharPrompt prompt = new CharPrompt();
            List<char> validKeys = new List<char>(new char[] { 'a', 'b', '\x1B' });

            char validKey = 'a';
            read.PushChar(validKey);
            char gotKey = prompt.PromptAndInput(io, validKeys);
            Assert.AreEqual(prompt.Prompt + "a\n", print.Output);
            Assert.AreEqual(validKey, gotKey);
            print.ClearOutput();
        }

        [Test]
        public void TestCharPromptHandlesEscape()
        {
            TestPrint print = new TestPrint();
            TestRead read = new TestRead();
            Io io = new Io(print, read);
            CharPrompt prompt = new CharPrompt();
            List<char> validKeys = new List<char>(new char[] { 'a', 'b', '\x1B' });

            char escapeKey = '\x1B';
            read.PushChar(escapeKey);
            char gotKey = prompt.PromptAndInput(io, validKeys);
            Assert.AreEqual(gotKey, '\x1B');
        }
    }
}
