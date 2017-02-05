//
// MSGTest/Types/Array/EndlessArrayTests.cs
//

using MSG.Types.Array;
using NUnit.Framework;
using System;

namespace MSGTest.Types.Array
{
    [TestFixture]
    public class EndlessArrayTests
    {
        EndlessArray<int> array;
        [SetUp]
        public void SetUp()
        {
            array = new EndlessArray<int>(0, 1, 2);
        }

        [Test]
        public void TestIndexWithinLength()
        {
            Assert.AreEqual(0, array[0]);
            Assert.AreEqual(2, array[2]);
        }

        [Test]
        public void TestIndexGreaterThanLength()
        {
            Assert.AreEqual(2, array[10]);
        }

        [Test]
        public void TestIndexLessThanZeroThrows()
        {
            int a;
            Assert.Catch<ArgumentOutOfRangeException>(() => a = array[-1]);
        }
    }
}
