﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TddTetris;
using Microsoft.Xna.Framework;

namespace Tests
{
    [TestFixture]
    public class FieldTest
    {
        private static IBlock UNUSED_BLOCK = null;

        #region Temporary tests until real block mapping is implemented
        [Test]
        public void Test_ColorAt_NullIfNotBlockPosition()
        {
            Field subject = new Field(2, 2);
            subject.SetBlock(null, new Vector2(1, 1));

            Assert.IsNull( subject.ColorAt(new Vector2(0,0)) );
        }

        [Test]
        public void Test_ColorAt_WhiteIfBlockPosition()
        {
            Vector2 position = new Vector2(1, 1);
            Field subject = new Field(2, 2);
            subject.SetBlock(null, position);

            Assert.AreEqual(Color.White, subject.ColorAt(position));
        }
        #endregion

        [Test]
        public void Test_ColorAt_OutOfBounds_RaisesException()
        {
            Field subject = new Field(2, 2);

            try
            {
                subject.ColorAt(new Vector2(2, 0));
                Assert.Fail();
            }
            catch (Exception)
            {
            }
        }

        [Test]
        public void Test_SetBlock()
        {
            Field subject = new Field(10, 10);
            IBlock block = new BlockHelper.MockBlock();
            Vector2 position = new Vector2(2, 3);

            subject.SetBlock(block, position);

            Assert.AreEqual(block, subject.Block);
            Assert.AreEqual(position, subject.Position);
        }

        [Test]
        public void Test_AdvanceBlock()
        {
            Field subject = new Field(10, 10);

            subject.SetBlock(UNUSED_BLOCK, new Vector2(3, 4));

            subject.AdvanceBlock();

            Assert.AreEqual(new Vector2(3, 5), subject.Position);
        }

        [Test]
        public void Test_MoveLeft()
        {
            Field subject = new Field(10, 10);
            subject.SetBlock(UNUSED_BLOCK, new Vector2(3, 4));
            subject.MoveBlockLeft();

            Assert.AreEqual(new Vector2(2, 4), subject.Position);
        }

        [Test]
        public void Test_MoveRight()
        {
            Field subject = new Field(10, 10);
            subject.SetBlock(new BlockHelper.MockBlock(), new Vector2(3, 4));
            subject.MoveBlockRight();

            Assert.AreEqual(new Vector2(4, 4), subject.Position);
        }

        #region Temporary tests until real behaviour is implemented
        [Test]
        public void Test_CanAdvance_WhenNotAtTheBottom_ReturnsTrue()
        {
            Field subject = new Field(10, 10);
            subject.SetBlock(UNUSED_BLOCK, new Vector2(2, 3));

            Assert.IsTrue(subject.CanAdvance());
        }

        [Test]
        public void Test_CanAdvance_WhenAtBottom_ReturnsFalse()
        {
            Field subject = new Field(10, 10);
            subject.SetBlock(UNUSED_BLOCK, new Vector2(3, 9));

            Assert.IsFalse(subject.CanAdvance());
        }

        [Test]
        public void Test_FixBlock_ShouldDoNothing()
        {
            Field subject = new Field(10, 10);
            subject.FixBlock();
        }

        [Test]
        public void Test_CanMoveLeft_WhenNotAtTheLeftEdge_ReturnsTrue()
        {
            Field subject = new Field(10, 10);
            subject.SetBlock(UNUSED_BLOCK, new Vector2(1, 5));

            Assert.IsTrue(subject.CanMoveLeft());
        }

        [Test]
        public void Test_CanMoveLeft_WhenAtTheLeftEdge_ReturnsFalse()
        {
            Field subject = new Field(10, 10);
            subject.SetBlock(UNUSED_BLOCK, new Vector2(0, 5));

            Assert.IsFalse(subject.CanMoveLeft());
        }

        [Test]
        public void Test_CanMoveRight_WhenNotAtTheRightEdge_ReturnsTrue()
        {
            Field subject = new Field(10, 10);
            subject.SetBlock(UNUSED_BLOCK, new Vector2(8, 5));

            Assert.IsTrue(subject.CanMoveRight());
        }

        [Test]
        public void Test_CanMoveRight_WhenAtTheRightdge_ReturnsFalse()
        {
            Field subject = new Field(10, 10);
            subject.SetBlock(UNUSED_BLOCK, new Vector2(9, 5));

            Assert.IsFalse(subject.CanMoveRight());
        }

        #endregion

    }
}
