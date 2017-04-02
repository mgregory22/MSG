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
    public class DbDirTests
    {
        protected Dir<Contents> dir;
        protected Contents contentsA = new Contents() { S = "contentsA", I = 1 };
        protected Contents contentsB = new Contents() { S = "contentsB", I = 2 };
        protected Contents contentsC = new Contents() { S = "contentsC", I = 4 };
        protected Contents contentsD = new Contents() { S = "contentsD", I = 16 };
        protected Contents contentsE = new Contents() { S = "contentsE", I = 25 };
        protected Database db;
        protected const string FILENAME = "DbDirTests.s3db";
        protected const string PREFIX = "Test";

        [Test]
        public void TestAddAndCount()
        {
            using (db = new Database(FILENAME, Database.OpenStyle.RecreateExistent)) {
                dir = new DbDir<Contents>(db, PREFIX);
                Assert.AreEqual(0, dir.Count);
                dir.Add("A", contentsA);
                Assert.AreEqual(1, dir.Count);
                dir.Add("B", contentsB);
                Assert.AreEqual(2, dir.Count);
            }
        }

        [Test]
        public void TestAddingDuplicateItemThrows()
        {
            using (db = new Database(FILENAME, Database.OpenStyle.RecreateExistent)) {
                dir = new DbDir<Contents>(db, PREFIX);
                dir.Add("A", contentsA);
                Assert.Throws<InvalidOperationException>(() => dir.Add("A", contentsA));
            }
        }

        [Test]
        public void TestGetCurPath()
        {
            using (db = new Database(FILENAME, Database.OpenStyle.RecreateExistent)) {
                dir = new DbDir<Contents>(db, PREFIX);
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
        }

        [Test]
        public void TestGetItemAt()
        {
            using (db = new Database(FILENAME, Database.OpenStyle.RecreateExistent)) {
                dir = new DbDir<Contents>(db, PREFIX);
                dir.Add("A", contentsA);
                dir.Add("B", contentsB);
                var a = dir.GetItemAt(0);
                Assert.AreEqual(contentsA, a, string.Format(
                        "Expected: {{ S:\"{0}\", I:{1} }}\n But was: {{ S:\"{2}\", I:{3} }}"
                        , contentsA.S
                        , contentsA.I
                        , a.S
                        , a.I
                    )
                );
                var b = dir.GetItemAt(1);
                Assert.AreEqual(contentsB, b, string.Format(
                        "Expected: {{ S:\"{0}\", I:{1} }}\n But was: {{ S:\"{2}\", I:{3} }}"
                        , contentsB.S
                        , contentsB.I
                        , b.S
                        , b.I
                    )
                );
            }
        }

        [Test]
        public void TestGetItemAtOutOfBoundsThrows()
        {
            using (db = new Database(FILENAME, Database.OpenStyle.RecreateExistent)) {
                dir = new DbDir<Contents>(db, PREFIX);
                dir.Add("A", contentsA);
                Contents c;
                Assert.Throws<ArgumentOutOfRangeException>(() => c = dir.GetItemAt(1));
            }
        }

        [Test]
        public void TestGetItemIndex()
        {
            using (db = new Database(FILENAME, Database.OpenStyle.RecreateExistent)) {
                dir = new DbDir<Contents>(db, PREFIX);
                dir.Add("A", contentsA);
                dir.Add("B", contentsB);
                dir.Add("C", contentsC);
                Assert.AreEqual(1, dir.GetItemIndex(contentsB));
            }
        }

        [Test]
        public void TestHasSubdirAt()
        {
            using (db = new Database(FILENAME, Database.OpenStyle.RecreateExistent)) {
                dir = new DbDir<Contents>(db, PREFIX);
                dir.Add("A", contentsA);
                dir.Add("B", contentsB);
                dir.CreateSubdir(1);
                dir.DownDir(1);
                dir.Add("C", contentsC);
                dir.UpDir();
                Assert.IsTrue(dir.HasSubdirAt(0)); // DbDir != MemDir in this case
                Assert.IsTrue(dir.HasSubdirAt(1));
            }
        }

        [Test]
        public void TestInsertAndCount()
        {
            using (db = new Database(FILENAME, Database.OpenStyle.RecreateExistent)) {
                dir = new DbDir<Contents>(db, PREFIX);
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
        }

        [Test]
        public void TestInsertingDuplicateItemThrows()
        {
            using (db = new Database(FILENAME, Database.OpenStyle.RecreateExistent)) {
                dir = new DbDir<Contents>(db, PREFIX);
                dir.Add("A", contentsA);
                Assert.Throws<InvalidOperationException>(() => dir.Insert(1, "A", contentsA));
            }
        }

        [Test]
        public void TestInsertingPastEndInsertsAtEnd()
        {
            using (db = new Database(FILENAME, Database.OpenStyle.RecreateExistent)) {
                dir = new DbDir<Contents>(db, PREFIX);
                dir.Add("A", contentsA);
                Assert.AreEqual(1, dir.Count);
                Assert.DoesNotThrow(() => dir.Insert(3, "B", contentsB));
                Assert.AreEqual(2, dir.Count);
                Assert.AreEqual(contentsB, dir.GetItemAt(1));
            }
        }

        [Test]
        public void TestIterator()
        {
            using (db = new Database(FILENAME, Database.OpenStyle.RecreateExistent)) {
                dir = new DbDir<Contents>(db, PREFIX);
                dir.Add("A", contentsA);
                dir.Add("B", contentsB);
                dir.Insert(0, "C", contentsC);
                dir.CreateSubdir(1);
                int i = 0;
                foreach (var d in dir) {
                    if (i == 0) {
                        Assert.AreEqual(d, contentsC);
                    } else if (i == 1) {
                        Assert.AreEqual(d, contentsA);
                    } else if (i == 2) {
                        Assert.AreEqual(d, contentsB);
                    }
                    i++;
                }
            }
        }

        [Test]
        public void TestItemExists()
        {
            using (db = new Database(FILENAME, Database.OpenStyle.RecreateExistent)) {
                dir = new DbDir<Contents>(db, PREFIX);
                Assert.IsFalse(dir.ItemExists(contentsA));
                dir.Add("A", contentsA);
                Assert.IsTrue(dir.ItemExists(contentsA));
                //Assert.IsFalse(dir.ItemExists(contentsB));
                //dir.Add(contentsB);
                //Assert.IsTrue(dir.ItemExists(contentsB));
                //dir.Insert(0, contentsC);
                //Assert.IsTrue(dir.ItemExists(contentsC));
            }
        }

        [Test]
        public void TestItemExistsAt()
        {
            using (db = new Database(FILENAME, Database.OpenStyle.RecreateExistent)) {
                dir = new DbDir<Contents>(db, PREFIX);
                Assert.IsFalse(dir.ItemExistsAt(0));
                dir.Add("A", contentsA);
                Assert.IsTrue(dir.ItemExistsAt(0));
                Assert.IsFalse(dir.ItemExistsAt(1));
                dir.Add("B", contentsB);
                Assert.IsTrue(dir.ItemExistsAt(1));
                dir.Insert(0, "C", contentsC);
                Assert.IsTrue(dir.ItemExistsAt(2));
            }
        }

        [Test]
        public void TestMove()
        {
            using (db = new Database(FILENAME, Database.OpenStyle.RecreateExistent)) {
                dir = new DbDir<Contents>(db, PREFIX);
                dir.Insert(0, "A", contentsA);
                dir.Insert(1, "B", contentsB);
                dir.Move(1, 0);
                Assert.AreEqual(2, dir.Count);
                Assert.AreEqual(contentsB, dir.GetItemAt(0));
                Assert.AreEqual(contentsA, dir.GetItemAt(1));
            }
        }

        [Test]
        public void TestRemoveFirstItem()
        {
            using (db = new Database(FILENAME, Database.OpenStyle.RecreateExistent)) {
                dir = new DbDir<Contents>(db, PREFIX);
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
        }

        [Test]
        public void TestRemoveLastItem()
        {
            using (db = new Database(FILENAME, Database.OpenStyle.RecreateExistent)) {
                dir = new DbDir<Contents>(db, PREFIX);
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
        }

        [Test]
        public void TestRemoveAt()
        {
            using (db = new Database(FILENAME, Database.OpenStyle.RecreateExistent)) {
                dir = new DbDir<Contents>(db, PREFIX);
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
}
