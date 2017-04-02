//
// MSGTest/Types/Dir/DirTests.cs
//

using MSG.IO;
using MSG.Types.Dir;
using NUnit.Framework;
using System;

namespace MSGTest.Types.Dir
{
    [TestFixture]
    public class MemDirTests
    {
        protected Dir<Contents> dir;
        protected Contents contentsA = new Contents() { S = "A", I = 1 };
        protected Contents contentsB = new Contents() { S = "B", I = 4 };
        protected Contents contentsC = new Contents() { S = "C", I = 9 };
        protected Contents contentsD = new Contents() { S = "D", I = 16 };
        protected Contents contentsE = new Contents() { S = "E", I = 25 };

        [Test]
        public void TestAddAndCount()
        {
            dir = new MemDir<Contents>();
            Assert.AreEqual(0, dir.Count);
            dir.Add("A", contentsA);
            Assert.AreEqual(1, dir.Count);
            dir.Add("B", contentsB);
            Assert.AreEqual(2, dir.Count);
        }

        [Test]
        public void TestAddingDuplicateItemThrows()
        {
            dir = new MemDir<Contents>();
            dir.Add("A", contentsA);
            Assert.Throws<InvalidOperationException>(() => dir.Add("A", contentsA));
        }

        [Test]
        public void TestGetCurPath()
        {
            dir = new MemDir<Contents>();
            dir.Add("A", contentsA);
            dir.Add("B", contentsB);
            dir.CreateSubdir(1);
            dir.DownDir(1);
            dir.Add("C", contentsC);
            dir.CreateSubdir(0);
            dir.DownDir(0);
            dir.Add("D", contentsD);
            dir.Add("E", contentsE);
            dir.DownDir(0);
            Assert.AreEqual("/B/C/D/", dir.GetCurPath());
            dir.UpDir();
            Assert.AreEqual("/B/C/", dir.GetCurPath());
            dir.UpDir();
            Assert.AreEqual("/B/", dir.GetCurPath());
        }

        [Test]
        public void TestGetItemAt()
        {
            dir = new MemDir<Contents>();
            dir.Add("A", contentsA);
            dir.Add("B", contentsB);
            Assert.AreEqual(contentsA, dir.GetItemAt(0));
            Assert.AreEqual(contentsB, dir.GetItemAt(1));
        }

        [Test]
        public void TestGetItemAtOutOfBoundsThrows()
        {
            dir = new MemDir<Contents>();
            dir.Add("A", contentsA);
            Contents c;
            Assert.Throws<ArgumentOutOfRangeException>(() => c = dir.GetItemAt(1));
        }

        [Test]
        public void TestGetItemIndex()
        {
            dir = new MemDir<Contents>();
            dir.Add("A", contentsA);
            dir.Add("B", contentsB);
            dir.Add("C", contentsC);
            Assert.AreEqual(1, dir.GetItemIndex(contentsB));
        }

        [Test]
        public void TestHasSubdirAt()
        {
            dir = new MemDir<Contents>();
            dir.Add("A", contentsA);
            dir.Add("B", contentsB);
            dir.CreateSubdir(1);
            Assert.IsFalse(dir.HasSubdirAt(0));
            Assert.IsTrue(dir.HasSubdirAt(1));
        }

        [Test]
        public void TestInsertAndCount()
        {
            dir = new MemDir<Contents>();
            Assert.AreEqual(0, dir.Count);
            dir.Insert(0, "A", contentsA);
            Assert.AreEqual(1, dir.Count);
            dir.Insert(1, "B", contentsB);
            Assert.AreEqual(2, dir.Count);
            dir.Insert(0, "C", contentsC);
            Assert.AreEqual(3, dir.Count);
            Assert.AreEqual(contentsC, dir.GetItemAt(0));
            Assert.AreEqual(contentsA, dir.GetItemAt(1));
            Assert.AreEqual(contentsB, dir.GetItemAt(2));
        }

        [Test]
        public void TestInsertingDuplicateItemThrows()
        {
            dir = new MemDir<Contents>();
            dir.Add("A", contentsA);
            Assert.Throws<InvalidOperationException>(() => dir.Insert(1, "A", contentsA));
        }

        [Test]
        public void TestInsertingPastEndInsertsAtEnd()
        {
            dir = new MemDir<Contents>();
            dir.Add("A", contentsA);
            Assert.AreEqual(1, dir.Count);
            Assert.DoesNotThrow(() => dir.Insert(3, "B", contentsB));
            Assert.AreEqual(2, dir.Count);
            Assert.AreEqual(contentsB, dir.GetItemAt(1));
        }

        [Test]
        public void TestIterator()
        {
            Enumerated<Contents> enumeratedA = new Enumerated<Contents>() { Name = "A", Item = contentsA };
            Enumerated<Contents> enumeratedB = new Enumerated<Contents>() { Name = "B", Item = contentsB };
            Enumerated<Contents> enumeratedC = new Enumerated<Contents>() { Name = "C", Item = contentsC };
            dir = new MemDir<Contents>();
            dir.Add(enumeratedA.Name, enumeratedA.Item);
            dir.Add(enumeratedB.Name, enumeratedB.Item);
            dir.Insert(0, enumeratedC.Name, enumeratedC.Item);
            dir.CreateSubdir(1);
            int i = 0;
            foreach (Enumerated<Contents> d in dir) {
                if (i == 0) {
                    Assert.AreEqual(d.Name, enumeratedC.Name);
                    Assert.AreEqual(d.Item, enumeratedC.Item);
                } else if (i == 1) {
                    Assert.AreEqual(d.Name, enumeratedA.Name);
                    Assert.AreEqual(d.Item, enumeratedA.Item);
                } else if (i == 2) {
                    Assert.AreEqual(d.Name, enumeratedB.Name);
                    Assert.AreEqual(d.Item, enumeratedB.Item);
                }
                i++;
            }
        }

        [Test]
        public void TestItemExists()
        {
            dir = new MemDir<Contents>();
            Assert.IsFalse(dir.ItemExists(contentsA));
            dir.Add("A", contentsA);
            Assert.IsTrue(dir.ItemExists(contentsA));
            Assert.IsFalse(dir.ItemExists(contentsB));
            dir.Add("B", contentsB);
            Assert.IsTrue(dir.ItemExists(contentsB));
            dir.Insert(0, "C", contentsC);
            Assert.IsTrue(dir.ItemExists(contentsC));
        }

        [Test]
        public void TestItemExistsAt()
        {
            dir = new MemDir<Contents>();
            Assert.IsFalse(dir.ItemExistsAt(0));
            dir.Add("A", contentsA);
            Assert.IsTrue(dir.ItemExistsAt(0));
            Assert.IsFalse(dir.ItemExistsAt(1));
            dir.Add("B", contentsB);
            Assert.IsTrue(dir.ItemExistsAt(1));
            dir.Insert(0, "C", contentsC);
            Assert.IsTrue(dir.ItemExistsAt(2));
        }

        [Test]
        public void TestMove()
        {
            dir = new MemDir<Contents>();
            dir.Insert(0, "A", contentsA);
            dir.Insert(1, "B", contentsB);
            dir.Move(1, 0);
            Assert.AreEqual(2, dir.Count);
            Assert.AreEqual(contentsB, dir.GetItemAt(0));
            Assert.AreEqual(contentsA, dir.GetItemAt(1));
        }

        [Test]
        public void TestRemoveFirstItem()
        {
            dir = new MemDir<Contents>();
            dir.Insert(0, "A", contentsA);
            dir.Insert(1, "B", contentsB);
            dir.Remove(contentsA);
            Assert.AreEqual(1, dir.Count);
            Assert.AreEqual(contentsB, dir.GetItemAt(0));
            Contents c;
            Assert.Throws<ArgumentOutOfRangeException>(() => c = dir.GetItemAt(1));
            dir.Remove(contentsB);
            Assert.AreEqual(0, dir.Count);
            Assert.Throws<ArgumentOutOfRangeException>(() => c = dir.GetItemAt(0));
        }

        [Test]
        public void TestRemoveLastItem()
        {
            dir = new MemDir<Contents>();
            dir.Insert(0, "A", contentsA);
            dir.Insert(1, "B", contentsB);
            dir.Remove(contentsB);
            Assert.AreEqual(1, dir.Count);
            Assert.AreEqual(contentsA, dir.GetItemAt(0));
            Contents c;
            Assert.Throws<ArgumentOutOfRangeException>(() => c = dir.GetItemAt(1));
            dir.Remove(contentsA);
            Assert.AreEqual(0, dir.Count);
            Assert.Throws<ArgumentOutOfRangeException>(() => c = dir.GetItemAt(0));
        }

        [Test]
        public void TestRemoveAt()
        {
            dir = new MemDir<Contents>();
            dir.Insert(0, "A", contentsA);
            dir.Insert(1, "B", contentsB);
            dir.RemoveAt(0);
            Assert.AreEqual(1, dir.Count);
            Assert.AreEqual(contentsB, dir.GetItemAt(0));
            Contents c;
            Assert.Throws<ArgumentOutOfRangeException>(() => c = dir.GetItemAt(1));
        }
    }
}
