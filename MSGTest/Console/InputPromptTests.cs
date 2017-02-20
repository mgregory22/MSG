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
            public InputPromptDerivative(Print print, Read read, string prompt = "> ") : base(print, read, prompt)
            {
            }
        }

        InputPrompt prompt;
        string promptMsg = "> ";
        TestPrint print;
        TestRead read;

        [SetUp]
        public void Initialize()
        {
            print = new TestPrint();
            read = new TestRead(print);
            // InputPrompt is abstract, so a derived class must be used to test
            prompt = new InputPromptDerivative(print, read, promptMsg);
        }

        [Test]
        public void TestKeyPromptStoresPrint()
        {
            Assert.AreEqual(print, prompt.Print);
        }

        [Test]
        public void TestKeyPromptStoresPrompt()
        {
            Assert.AreEqual(promptMsg, prompt.Prompt);
        }

        [Test]
        public void TestKeyPromptStoresRead()
        {
            Assert.AreEqual(read, prompt.Read);
        }

        [Test]
        public void TestPause()
        {
            // Push a key to get past the prompt
            read.PushEnter();
            prompt.Pause();
            Assert.AreEqual(print.Output, prompt.PausePrompt);
        }

        [Test]
        public void TestCustomPausePrompt()
        {
            prompt.PausePrompt = "Hit any key to continue";
            // Push a key to get past the prompt
            read.PushEnter();
            prompt.Pause();
            Assert.AreEqual(print.Output, prompt.PausePrompt);
        }
    }
}
