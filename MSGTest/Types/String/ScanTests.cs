//
// MSGTest/Types/String/ScanTests.cs
//

using MSG.Types.String;
using NUnit.Framework;

namespace MSGTest.Types.String
{
    [TestFixture]
    public class ScanTests
    {
        [Test]
        public void TestSkipHardReturnSkipsNL()
        {
            string text = "...\n...";
            int i = 3;
            i = Scan.SkipHardReturn(text, i);
            Assert.AreEqual(4, i);
        }

        [Test]
        public void TestSkipHardReturnSkipsCR()
        {
            string text = "...\r...";
            int i = 3;
            i = Scan.SkipHardReturn(text, i);
            Assert.AreEqual(4, i);
        }

        [Test]
        public void TestSkipHardReturnSkipsCRNL()
        {
            string text = "...\r\n...";
            int i = 3;
            i = Scan.SkipHardReturn(text, i);
            Assert.AreEqual(5, i);
        }

        [Test]
        public void TestSkipHardReturnSkipsNLCR()
        {
            string text = "...\n\r...";
            int i = 3;
            i = Scan.SkipHardReturn(text, i);
            Assert.AreEqual(5, i);
        }

        [Test]
        public void TestSkipHardReturnSkipsOnlyOneNL()
        {
            string text = "...\n\n...";
            int i = 3;
            i = Scan.SkipHardReturn(text, i);
            Assert.AreEqual(4, i);
        }

        [Test]
        public void TestSkipHardReturnSkipsOnlyOneCR()
        {
            string text = "...\r\r...";
            int i = 3;
            i = Scan.SkipHardReturn(text, i);
            Assert.AreEqual(4, i);
        }

        [Test]
        public void TestSkipHardReturnSkipsOnlyOneCRNL()
        {
            string text = "...\r\n\r\n...";
            int i = 3;
            i = Scan.SkipHardReturn(text, i);
            Assert.AreEqual(5, i);
            text = "...\r\n\n\r...";
            i = 3;
            i = Scan.SkipHardReturn(text, i);
            Assert.AreEqual(5, i);
        }

        [Test]
        public void TestSkipHardReturnSkipsOnlyOneNLCR()
        {
            string text = "...\n\r\n\r...";
            int i = 3;
            i = Scan.SkipHardReturn(text, i);
            Assert.AreEqual(5, i);
            text = "...\n\r\r\n...";
            i = 3;
            i = Scan.SkipHardReturn(text, i);
            Assert.AreEqual(5, i);
        }

        [Test]
        public void TestSkipHardReturnDoesntSkipNonHardReturn()
        {
            string text = "......";
            int i = 3;
            i = Scan.SkipHardReturn(text, i);
            Assert.AreEqual(3, i);
        }

        [Test]
        public void TestSkipWhiteSpaceSkipsOneSpace()
        {
            string text = "... ...";
            int i = 3;
            i = Scan.SkipWhiteSpace(text, i);
            Assert.AreEqual(4, i);
        }

        [Test]
        public void TestSkipWhiteSpaceSkipsTwoSpaces()
        {
            string text = "...  ...";
            int i = 3;
            i = Scan.SkipWhiteSpace(text, i);
            Assert.AreEqual(5, i);
        }

        [Test]
        public void TestSkipWhiteSpaceSkipsCRSpace()
        {
            string text = "...\r ...";
            int i = 3;
            i = Scan.SkipWhiteSpace(text, i);
            Assert.AreEqual(5, i);
        }

        [Test]
        public void TestSkipWhiteSpaceSkipsNLSpace()
        {
            string text = "...\n ...";
            int i = 3;
            i = Scan.SkipWhiteSpace(text, i);
            Assert.AreEqual(5, i);
        }

        [Test]
        public void TestSkipWhiteSpaceSkipsTabSpace()
        {
            string text = "...\t ...";
            int i = 3;
            i = Scan.SkipWhiteSpace(text, i);
            Assert.AreEqual(5, i);
        }

        [Test]
        public void TestSkipWhiteSpaceDoesntSkipNonSpace()
        {
            string text = "...x...";
            int i = 3;
            i = Scan.SkipWhiteSpace(text, i);
            Assert.AreEqual(3, i);
        }
    }
}
