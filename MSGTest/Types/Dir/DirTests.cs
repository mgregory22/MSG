//
// MSGTest/Types/Dir/DirTests.cs
//

using MSG.Types.Dir;
using NUnit.Framework;
using System;

namespace MSGTest.Types.Dir
{
    [TestFixture]
    public class DirTests
    {
        Dir<char> dir;

        [SetUp]
        public void SetUp()
        {
            dir = new Dir<char>();
        }

        [Test]
        public void TestAddAndCount()
        {
            Assert.AreEqual(0, dir.Count);
            dir.Add('a');
            Assert.AreEqual(1, dir.Count);
            dir.Add('b');
            Assert.AreEqual(2, dir.Count);
        }

        [Test]
        public void TestAddingDuplicateItemThrows()
        {
            dir.Add('a');
            Assert.Throws<InvalidOperationException>(() => dir.Add('a'));
        }

        [Test]
        public void TestBracketAccess()
        {
            dir.Add('a');
            dir.Add('b');
            Assert.AreEqual('a', dir[0]);
            Assert.AreEqual('b', dir[1]);
        }

        [Test]
        public void TestOutOfBoundsBracketAccessThrows()
        {
            dir.Add('a');
            char c;
            Assert.Throws<ArgumentOutOfRangeException>(() => c = dir[1]);
        }

        [Test]
        public void TestGetSubdirOnItemWithoutSubdirReturnsNull()
        {
            dir.Add('a');
            Assert.IsNull(dir.GetSubdir(0));
        }

        [Test]
        public void TestInsertAndCount()
        {
            Assert.AreEqual(0, dir.Count);
            dir.Insert(0, 'a');
            Assert.AreEqual(1, dir.Count);
            dir.Insert(1, 'b');
            Assert.AreEqual(2, dir.Count);
            dir.Insert(0, 'c');
            Assert.AreEqual(3, dir.Count);
            Assert.AreEqual('c', dir[0]);
            Assert.AreEqual('a', dir[1]);
            Assert.AreEqual('b', dir[2]);
        }

        [Test]
        public void TestInsertingDuplicateItemThrows()
        {
            dir.Add('a');
            Assert.Throws<InvalidOperationException>(() => dir.Insert(1, 'a'));
        }

        [Test]
        public void TestInsertingPastEndInsertsAtEnd()
        {
            dir.Add('a');
            Assert.AreEqual(1, dir.Count);
            Assert.DoesNotThrow(() => dir.Insert(3, 'b'));
            Assert.AreEqual(2, dir.Count);
            Assert.AreEqual('b', dir[1]);
        }

        [Test]
        public void TestIterator()
        {
            dir.Add('a');
            dir.Add('b');
            dir.Insert(0, 'c');
            Dir<char> subdir = new Dir<char>();
            dir.SetSubdir(1, subdir);
            int i = 0;
            foreach (char d in dir) {
                if (i == 0) {
                    Assert.AreEqual(d, 'c');
                } else if (i == 1) {
                    Assert.AreEqual(d, 'a');
                } else if (i == 2) {
                    Assert.AreEqual(d, 'b');
                }
                i++;
            }
        }

        [Test]
        public void TestItemExists()
        {
            Assert.IsFalse(dir.ItemExists('a'));
            dir.Add('a');
            Assert.IsTrue(dir.ItemExists('a'));
            Assert.IsFalse(dir.ItemExists('b'));
            dir.Add('b');
            Assert.IsTrue(dir.ItemExists('b'));
            dir.Insert(0, 'c');
            Assert.IsTrue(dir.ItemExists('c'));
        }

        [Test]
        public void TestItemExistsAt()
        {
            Assert.IsFalse(dir.ItemExistsAt(0));
            dir.Add('a');
            Assert.IsTrue(dir.ItemExistsAt(0));
            Assert.IsFalse(dir.ItemExistsAt(1));
            dir.Add('b');
            Assert.IsTrue(dir.ItemExistsAt(1));
            dir.Insert(0, 'c');
            Assert.IsTrue(dir.ItemExistsAt(2));
        }

        [Test]
        public void TestMove()
        {
            dir.Insert(0, 'a');
            dir.Insert(1, 'b');
            dir.Move(1, 0);
            Assert.AreEqual(2, dir.Count);
            Assert.AreEqual('b', dir[0]);
            Assert.AreEqual('a', dir[1]);
        }

        [Test]
        public void TestRemoveFirstItem()
        {
            dir.Insert(0, 'a');
            dir.Insert(1, 'b');
            dir.Remove('a');
            Assert.AreEqual(1, dir.Count);
            Assert.AreEqual('b', dir[0]);
            char c;
            Assert.Throws<ArgumentOutOfRangeException>(() => c = dir[1]);
            dir.Remove('b');
            Assert.AreEqual(0, dir.Count);
            Assert.Throws<ArgumentOutOfRangeException>(() => c = dir[0]);
        }

        [Test]
        public void TestRemoveLastItem()
        {
            dir.Insert(0, 'a');
            dir.Insert(1, 'b');
            dir.Remove('b');
            Assert.AreEqual(1, dir.Count);
            Assert.AreEqual('a', dir[0]);
            char c;
            Assert.Throws<ArgumentOutOfRangeException>(() => c = dir[1]);
            dir.Remove('a');
            Assert.AreEqual(0, dir.Count);
            Assert.Throws<ArgumentOutOfRangeException>(() => c = dir[0]);
        }

        [Test]
        public void TestRemoveAt()
        {
            dir.Insert(0, 'a');
            dir.Insert(1, 'b');
            dir.RemoveAt(0);
            Assert.AreEqual(1, dir.Count);
            Assert.AreEqual('b', dir[0]);
            char c;
            Assert.Throws<ArgumentOutOfRangeException>(() => c = dir[1]);
        }

        [Test]
        public void TestSetSubdirAndGetSubdir()
        {
            dir.Add('a');
            Dir<char> subdir = new Dir<char>();
            dir.SetSubdir(0, subdir);
            Assert.AreEqual(subdir, dir.GetSubdir(0));
            dir.Insert(0, 'c');
            Assert.AreEqual(subdir, dir.GetSubdir(1));
        }

        [Test]
        public void TestHasSubdirAt()
        {
            dir.Add('a');
            dir.Add('b');
            Dir<char> subdir = new Dir<char>();
            dir.SetSubdir(1, subdir);
            Assert.IsFalse(dir.HasSubdirAt(0));
            Assert.IsTrue(dir.HasSubdirAt(1));
        }
    }
}
