//
// MSGTest/Types/String/FormatTests.cs
//

using MSG.Types.String;
using NUnit.Framework;

namespace MSGTest.Types.String
{
    [TestFixture]
    public class FormatTests
    {
        [Test]
        public void TestToLiteral()
        {
            Assert.AreEqual("\\t\\n", Format.ToLiteral("\t\n"));
        }

        [Test]
        public void TestPadding()
        {
            Assert.AreEqual("   ", Format.Padding(3));
        }
    }
}
