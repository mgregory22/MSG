//
// MSGTest/Console/PromptTests.cs
//

using MSG.Console;
using MSGTest.IO;
using NUnit.Framework;

namespace MSGTest.Console
{
    [TestFixture]
    public class PromptTests
    {
        InputPrompt prompt;
        string promptMsg = "> ";
        TestPrint print;
        TestRead read;
        [SetUp]
        public void Initialize()
        {
            print = new TestPrint();
            read = new TestRead(print);
            prompt = new CharPrompt(print, read, promptMsg);
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
    }
}
