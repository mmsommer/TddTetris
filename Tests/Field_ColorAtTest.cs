using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TddTetris;
using Microsoft.Xna.Framework;
using Moq;

namespace Tests
{
    [TestFixture]
    public class Field_ColorAtTest
    {
        [Test]
        public void Test_ColorAt_OutOfBounds_RaisesException()
        {
            Field subject = new Field(2, 2);

            try
            {
                subject.ColorAt(new Vector2(2, 0));
                Assert.Fail();
            }
            catch (Exception) { }
        }

        [Test]
        public void Test_ColorAt_WhenBlockIsNull_ReturnsNull()
        {
            Field subject = new Field(10, 10);
            Assert.IsNull(subject.ColorAt(new Vector2(3, 3)));
        }

        [Test]
        public void Test_ColorAt_WhenNoBlockIsThere_ReturnsNull()
        {
            Field subject = new Field(10, 10);
            subject.SetBlock(new BlockHelper.MockBlock(), new Vector2(3, 4));

            Assert.IsNull(subject.ColorAt(new Vector2(3, 3)));
        }

        [Test]
        public void Test_ColorAt_WhenBlockIsThereWithColor_ReturnsBlockColorAt()
        {
            Field subject = new Field(10, 10);

            Mock<IBlock> block = new Mock<IBlock>();
            block.Setup(b => b.ColorAt(new Vector2(1, 2))).Returns(Color.White);
            block.Setup(b => b.Width).Returns(2);
            block.Setup(b => b.Height).Returns(3);
            subject.SetBlock(block.Object, new Vector2(4, 4));

            Color? result = subject.ColorAt(new Vector2(5, 6));
            Assert.AreEqual(Color.White, result.Value);
        }

        [Test]
        public void Test_ColorAt_WhenBlockIsThereAndNoColor_ItReturnsFieldColorAt()
        {
            Field subject = new Field(4, 4);
            Color white = Color.White;

            subject.SetContentsForTest(new Color?[,]
            { { null, null,  null,  null},
              { null, white, white, null },
              { null, white, white, null },
              { null, null,  null,  null} });

            Mock<IBlock> block = new Mock<IBlock>();
            Color? nullColor = null;

            block.Setup(b => b.Width).Returns(2);
            block.Setup(b => b.Height).Returns(2);
            block.Setup(b => b.ColorAt(new Vector2(0, 1))).Returns(nullColor);

            subject.SetBlock(block.Object, new Vector2(1, 1));

            Color? result = subject.ColorAt(new Vector2(1, 2));
            Assert.AreEqual(Color.White, result.Value);
        }
    }
}
