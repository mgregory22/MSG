//
// MSGTest/Console/Editor/BufferTests.cs
//

using NUnit.Framework;
using Buffer = MSG.Console.Editor.Buffer;

namespace MSGTest.Console
{
    [TestFixture]
    public class NewBufferTests
    {
        Buffer buffer; // creation buffer

        [SetUp]
        public void SetUp()
        {
            buffer = new Buffer();
        }

        [Test]
        public void TestClearResetsCursorPosition()
        {
            buffer.Clear();
            Assert.AreEqual(0, buffer.Point);
        }

        [Test]
        public void TestClearResetsText()
        {
            buffer.Clear();
            Assert.AreEqual("", buffer.Text);
        }

        [Test]
        public void TestCreatingEmptyBufferResetsCursorPosition()
        {
            Assert.AreEqual(0, buffer.Point);
        }

        [Test]
        public void TestCreatingEmptyBufferResetsText()
        {
            Assert.AreEqual("", buffer.Text);
        }

        [Test]
        public void TestCursorLeftHasNoEffectOnEmptyBuffer()
        {
            buffer.RetreatPoint();
            Assert.AreEqual(0, buffer.Point);
            Assert.AreEqual("", buffer.Text);
        }

        [Test]
        public void TestCursorRightHasNoEffectOnEmptyBuffer()
        {
            buffer.AdvancePoint();
            Assert.AreEqual(0, buffer.Point);
            Assert.AreEqual("", buffer.Text);
        }

        [Test]
        public void TestDeleteHasNoEffectOnEmptyBuffer()
        {
            buffer.DeleteChar();
            Assert.AreEqual("", buffer.Text);
            Assert.AreEqual(0, buffer.Point);
        }

        [Test]
        public void TestInsertCharIntoEmptyBuffer()
        {
            buffer.InsertChar('z');
            Assert.AreEqual("z", buffer.Text);
        }

        [Test]
        public void TestIsEmptyIsTrueOnEmptyBuffer()
        {
            Assert.IsTrue(buffer.IsEmpty());
        }

        [Test]
        public void TestToStringOnEmptyBufferReturnsEmptyString()
        {
            Assert.AreEqual("", buffer.ToString());
        }
    }

    [TestFixture]
    public class OldBufferTests
    {
        Buffer buffer; // editing buffer

        [SetUp]
        public void SetUp()
        {
            buffer = new Buffer("abcd", 4); // editing "abcd", cursor position 4
        }

        [Test]
        public void TestCreatingExistingTextBufferSetsInitialCursorPosition()
        {
            Assert.AreEqual(4, buffer.Point);
        }

        [Test]
        public void TestCreatingExistingTextBufferSetsInitialString()
        {
            Assert.AreEqual("abcd", buffer.Text);
        }
        /*
        [Test]
        public void TestBackspaceRemovesCharBeforeCursor()
        {
            buffer.Backspace();
            Assert.AreEqual("abc", buffer.Text);
        }

        [Test]
        public void TestBackspaceMovesCursorBackOne()
        {
            buffer.Backspace();
            Assert.AreEqual(3, buffer.Point);
        }

        [Test]
        public void TestBackspaceAndInsertReplacesChar()
        {
            buffer.Backspace();
            buffer.Insert('x');
            Assert.AreEqual("abcx", buffer.Text);
        }

        [Test]
        public void TestBackspaceAndInsertRestoresCursorToSamePosition()
        {
            buffer.Backspace();
            buffer.Insert('x');
            Assert.AreEqual(4, buffer.Point);
        }
        */
        [Test]
        public void TestClearResetsCursorPosition()
        {
            buffer.Clear();
            Assert.AreEqual(0, buffer.Point);
        }

        [Test]
        public void TestClearResetsText()
        {
            buffer.Clear();
            Assert.AreEqual("", buffer.Text);
        }

        [Test]
        public void TestCursorLeftAndInsert()
        {
            buffer.RetreatPoint();
            buffer.InsertChar('x');
            Assert.AreEqual("abcxd", buffer.Text);
        }

        [Test]
        public void TestCursorLeftAndInsertRestoresCursorPosition()
        {
            buffer.RetreatPoint();
            buffer.InsertChar('x');
            Assert.AreEqual(4, buffer.Point);
        }

        [Test]
        public void TestCursorLeftMovesCursorLeftOne()
        {
            buffer.RetreatPoint();
            Assert.AreEqual(3, buffer.Point);
        }

        [Test]
        public void TestCursorLeftHasNoEffectOnText()
        {
            buffer.RetreatPoint();
            Assert.AreEqual("abcd", buffer.Text);
        }

        [Test]
        public void TestCursorRightAtEndHasNoEffectOnCursorPosition()
        {
            buffer.AdvancePoint();
            Assert.AreEqual(4, buffer.Point);
        }

        [Test]
        public void TestCursorRightAtEndHasNoEffectOnText()
        {
            buffer.AdvancePoint();
            Assert.AreEqual("abcd", buffer.Text);
        }

        [Test]
        public void TestDeleteAtEndDoesntMoveCursor()
        {
            buffer.DeleteChar();
            Assert.AreEqual(4, buffer.Point);
        }

        [Test]
        public void TestDeleteAtEndDoesntRemoveChar()
        {
            buffer.DeleteChar();
            Assert.AreEqual("abcd", buffer.Text);
        }

        [Test]
        public void TestInsertCharAtEnd()
        {
            buffer.InsertChar('x');
            Assert.AreEqual("abcdx", buffer.Text);
        }

        [Test]
        public void TestIsEmptyIsFalse()
        {
            Assert.IsFalse(buffer.IsEmpty());
        }

        [Test]
        public void TestToStringReturnsText()
        {
            Assert.AreEqual("abcd", buffer.ToString());
        }
    }
}
