//
// MSGTest/Types/String/DrawTests.cs
//

using MSG.Types.String;
using NUnit.Framework;

namespace MSGTest.Types.String
{
    [TestFixture]
    public class DrawTests
    {
        [Test]
        public void TestRulerReturns10CharRulerWith1AtTheEnd()
        {
            Assert.AreEqual("---------1", Draw.Ruler(10));
        }
        [Test]
        public void TestRulerReturns20CharRulerWith2AtTheEnd()
        {
            Assert.AreEqual("---------1---------2", Draw.Ruler(20));
        }
        [Test]
        public void TestUnderlinedTextReturnsMainMenuTitle()
        {
            Assert.AreEqual("Main Menu\n---------\n", Draw.UnderlinedText("Main Menu"));
        }
    }
}
