using Microsoft.Xna.Framework;
using NUnit.Framework;
using TddTetris;

namespace Tests
{
    public class OverlapCheckerTest
    {
        [Test]
        public void Test_CheckPosition_PositionIsBlocked_ReturnsTrue()
        {
            var field = new Color?[,]{
                {Color.Red}
            };
            var subject = new OverlapChecker();
            var vector = new Vector2(0, 0);

            Assert.That(subject.CheckPosition(vector, field), Is.True);
        }

        [Test]
        public void Test_CheckPosition_PositionIsFree_ReturnsFalse()
        {
            var field = new Color?[,]{
                {null}
            };
            var subject = new OverlapChecker();
            var vector = new Vector2(0, 0);

            Assert.That(subject.CheckPosition(vector, field), Is.False);
        }

        [Test]
        public void Test_CheckFieldBoundaries_PositionIsOutsideOfField_ReturnsTrue()
        {
            var field = new Color?[,]{
                {null}
            };
            var subject = new OverlapChecker();
            var position = new Vector2(-1, -1);

            Assert.That(subject.CheckFieldBoundaries(position, field), Is.True);
        }

        [Test]
        public void Test_CheckFieldBoundaries_PositionIsNotOutsideOfField_ReturnsFalse()
        {
            var field = new Color?[,]{
                {null}
            };
            var subject = new OverlapChecker();
            var position = new Vector2(0, 0);

            Assert.That(subject.CheckFieldBoundaries(position, field), Is.False);
        }

        #region afkomstig uit fieldtest

        [Test]
        public void Test_CanMoveLeft_WhenNotAtTheLeftEdge_ReturnsTrue()
        {
            var field = new Color?[,]{
                {null,null,null},
                {null,null,null},
                {null,null,null}
            };
            var Block = new BlockHelper.MockBlock(2, 1);
            var vector = new Vector2(1, 1);
            var subject = new OverlapChecker();

            Assert.That(subject.Check(Block, vector + new Vector2(-1, 0), field), Is.True);
        }

        [Test]
        public void Test_CanMoveLeft_WhenAtTheLeftEdge_ReturnsFalse()
        {
            var field = new Color?[,]{
                {null,null,null},
                {null,null,null},
                {null,null,null}
            };
            var Block = new BlockHelper.MockBlock(2, 1);
            var vector = new Vector2(0, 1);
            var subject = new OverlapChecker();

            Assert.That(subject.Check(Block, vector + new Vector2(-1, 0), field), Is.False);
        }

        [Test]
        public void Test_CanMoveRight_WhenNotAtTheRightEdge_ReturnsTrue()
        {
            var field = new Color?[,]{
                {null,null,null},
                {null,null,null},
                {null,null,null}
            };
            var Block = new BlockHelper.MockBlock(2, 1);
            var vector = new Vector2(0, 1);
            var subject = new OverlapChecker();

            Assert.That(subject.Check(Block, vector + new Vector2(1, 0), field), Is.True);
        }

        [Test]
        public void Test_CanMoveRight_WhenAtTheRightEdge_ReturnsFalse()
        {
            var field = new Color?[,]{
                {null,null,null},
                {null,null,null},
                {null,null,null}
            };
            var Block = new BlockHelper.MockBlock(2, 1);
            var vector = new Vector2(1, 1);
            var subject = new OverlapChecker();

            Assert.That(subject.Check(Block, vector + new Vector2(1, 0), field), Is.False);
        }

        [Test]
        public void Test_CanAdvance_WhenNotAtTheBottom_ReturnsTrue()
        {
            var field = new Color?[,]{
                {null,null,null},
                {null,null,null},
                {null,null,null}
            };
            var Block = new BlockHelper.MockBlock(2, 1);
            var vector = new Vector2(1, 1);
            var subject = new OverlapChecker();

            Assert.That(subject.Check(Block, vector + new Vector2(0, 1), field), Is.True);
        }

        [Test]
        public void Test_CanAdvance_WhenAtBottom_ReturnsFalse()
        {
            var field = new Color?[,]{
                {null,null,null},
                {null,null,null},
                {null,null,null}
            };
            var Block = new BlockHelper.MockBlock(2, 1);
            var vector = new Vector2(1, 2);
            var subject = new OverlapChecker();

            Assert.That(subject.Check(Block, vector + new Vector2(0, 1), field), Is.False);
        }

        [Test]
        public void Test_CanAdvance_WhenNotAtTheBottomButBlockInWay_ReturnsFalse()
        {
            var field = new Color?[,]{
                {null,null,null},
                {null,null,Color.Red},
                {null,null,null}
            };
            var Block = new BlockHelper.MockBlock(2, 1);
            var vector = new Vector2(1, 1);
            var subject = new OverlapChecker();

            Assert.That(subject.Check(Block, vector + new Vector2(0, 1), field), Is.False);
        }

        #endregion
    }
}
