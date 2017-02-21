//
// MSGTest/Console/PromptTests.cs
//

using MSG.Console;
using MSG.IO;
using MSGTest.IO;
using NUnit.Framework;

namespace MSGTest.Console
{
    [TestFixture]
    public class InputPromptTests
    {
        class InputPromptDerivative : InputPrompt
        {
            public InputPromptDerivative(string prompt = "> ")
                : base(prompt)
            {
            }
        }

        InputPrompt prompt;
        string promptMsg = "> ";
        TestPrint print;
        TestRead read;
        Io io;

        [SetUp]
        public void Initialize()
        {
            print = new TestPrint();
            read = new TestRead();
            io = new Io(print, read);
            // InputPrompt is abstract, so a derived class must be used to test
            prompt = new InputPromptDerivative(promptMsg);
        }

        [Test]
        public void TestKeyPromptStoresPrompt()
        {
            Assert.AreEqual(promptMsg, prompt.Prompt);
        }

        [Test]
        public void TestPause()
        {
            // Push a key to get past the prompt
            read.PushEnter();
            prompt.Pause(io);
            Assert.AreEqual(prompt.PausePrompt, print.Output);
        }

        [Test]
        public void TestCustomPausePrompt()
        {
            prompt.PausePrompt = "Hit any key to continue";
            // Push a key to get past the prompt
            read.PushEnter();
            prompt.Pause(io);
            Assert.AreEqual(prompt.PausePrompt, print.Output);
        }
    }
}
