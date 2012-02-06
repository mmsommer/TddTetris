using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using TddTetris;

namespace Tests
{
    [TestFixture]
    public class FieldTest
    {
        private static IBlock UNUSED_BLOCK = null;

        [Test]
        public void Test_SetBlock()
        {
            Field subject = new Field(10, 10);
            IBlock block = new BlockHelper.MockBlock(4, 4);
            Vector2 position = new Vector2(2, 3);

            subject.SetBlock(block, position);

            Assert.AreEqual(block, subject.Block);
            Assert.AreEqual(position, subject.Position);
        }

        [Test]
        public void Test_AdvanceBlock()
        {
            var overlapCheckerStub = new Mock<OverlapChecker>();
            overlapCheckerStub
                .Setup(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()))
                .Returns(() => true);

            Field subject = new Field(10, 10, overlapCheckerStub.Object);

            subject.SetBlock(UNUSED_BLOCK, new Vector2(3, 4));

            subject.AdvanceBlock();

            Assert.AreEqual(new Vector2(3, 5), subject.Position);
        }

        [Test]
        public void Test_MoveLeft()
        {
            var overlapCheckerStub = new Mock<OverlapChecker>();
            overlapCheckerStub
                .Setup(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()))
                .Returns(() => true);

            Field subject = new Field(10, 10, overlapCheckerStub.Object);
            subject.SetBlock(UNUSED_BLOCK, new Vector2(3, 4));
            subject.MoveBlockLeft();

            Assert.AreEqual(new Vector2(2, 4), subject.Position);
        }

        [Test]
        public void Test_MoveRight()
        {
            var overlapCheckerStub = new Mock<OverlapChecker>();
            overlapCheckerStub
                .Setup(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()))
                .Returns(() => true);

            Field subject = new Field(10, 10, overlapCheckerStub.Object);
            subject.SetBlock(new BlockHelper.MockBlock(), new Vector2(3, 4));
            subject.MoveBlockRight();

            Assert.AreEqual(new Vector2(4, 4), subject.Position);
        }

        [Test]
        public void Test_SetContentsForTest()
        {
            Field subject = new Field(2, 2);

            subject.SetContentsForTest(new Color?[,] {
                { Color.Red, null },
                { null, Color.Blue } 
            });

            Assert.AreEqual(Color.Red, subject.ColorAt(new Vector2(0, 0)));
            Assert.AreEqual(Color.Blue, subject.ColorAt(new Vector2(1, 1)));
            Assert.IsNull(subject.ColorAt(new Vector2(0, 1)));
            Assert.IsNull(subject.ColorAt(new Vector2(1, 0)));
        }

        #region komen eigenlijk te vervallen, want wordt nu afgehandeld door de overlapchecker class

        [Test]
        public void Test_CanMoveLeft_WhenNotAtTheLeftEdge_ReturnsTrue()
        {
            Field subject = new Field(10, 10);
            subject.SetBlock(new BlockHelper.MockBlock(2, 1), new Vector2(1, 5));

            Assert.That(subject.CanMoveLeft(), Is.True);
        }

        [Test]
        public void Test_CanMoveLeft_WhenAtTheLeftEdge_ReturnsFalse()
        {
            Field subject = new Field(10, 10);
            subject.SetBlock(new BlockHelper.MockBlock(2, 1), new Vector2(0, 5));

            Assert.That(subject.CanMoveLeft(), Is.False);
        }

        [Test]
        public void Test_CanMoveRight_WhenNotAtTheRightEdge_ReturnsTrue()
        {
            Field subject = new Field(10, 10);
            subject.SetBlock(new BlockHelper.MockBlock(2, 1), new Vector2(7, 5));

            Assert.That(subject.CanMoveRight(), Is.True);
        }

        [Test]
        public void Test_CanMoveRight_WhenAtTheRightdge_ReturnsFalse()
        {
            Field subject = new Field(10, 10);
            subject.SetBlock(new BlockHelper.MockBlock(2, 1), new Vector2(8, 5));

            Assert.That(subject.CanMoveRight(), Is.False);
        }

        [Test]
        public void Test_CanAdvance_WhenNotAtTheBottom_ReturnsTrue()
        {
            int blockHeight = 3;
            int fieldHeight = 10;

            Field subject = new Field(10, 10);
            subject.SetBlock(new BlockHelper.MockBlock(1, blockHeight), new Vector2(2, fieldHeight - blockHeight - 1));

            Assert.That(subject.CanAdvance(), Is.True);
        }

        [Test]
        public void Test_CanAdvance_WhenAtBottom_ReturnsFalse()
        {
            int blockHeight = 3;
            int fieldHeight = 10;

            Field subject = new Field(10, 10);
            subject.SetBlock(new BlockHelper.MockBlock(1, blockHeight), new Vector2(3, fieldHeight - blockHeight));

            Assert.That(subject.CanAdvance(), Is.False);
        }

        [Test]
        public void Test_CanAdvance_WhenNotAtTheBottomButBlockInWay_ReturnsFalse()
        {
            int blockHeight = 3;
            int fieldHeight = 10;

            Field subject = new Field(10, 10);
            subject.SetBlock(new BlockHelper.MockBlock(1, blockHeight), new Vector2(2, fieldHeight - blockHeight - 1));
            subject.SetContentsForTest(new Color?[,]{
                {null,null,null,null,null,null,null,null,null,null},
                {null,null,null,null,null,null,null,null,null,null},
                {null,null,null,null,null,null,null,null,null,Color.Red},
                {null,null,null,null,null,null,null,null,null,null},
                {null,null,null,null,null,null,null,null,null,null},
                {null,null,null,null,null,null,null,null,null,null},
                {null,null,null,null,null,null,null,null,null,null},
                {null,null,null,null,null,null,null,null,null,null},
                {null,null,null,null,null,null,null,null,null,null},
                {null,null,null,null,null,null,null,null,null,null}
            });

            Assert.That(subject.CanAdvance(), Is.False);
        }

        [Test]
        public void Test_CanRotateRight_RightSideFreeToRotate_ReturnsTrue()
        {
            int blockHeight = 2;

            Field subject = new Field(3, 3);
            subject.SetBlock(new BlockHelper.MockBlock(1, blockHeight), new Vector2(2, 0));
            subject.SetContentsForTest(new Color?[,]{
                {null,null,null},
                {null,null,null},
                {null,null,null}
            });

            Assert.That(subject.CanRotateRight(), Is.False);
        }

        [Test]
        public void Test_CanRotateRight_RightSideNotFreeToRotate_ReturnsFalse()
        {
            int blockHeight = 2;

            Field subject = new Field(3, 3);
            subject.SetBlock(new BlockHelper.MockBlock(1, blockHeight), new Vector2(1, 0));
            subject.SetContentsForTest(new Color?[,]{
                {null,null,null},
                {null,null,null},
                {null,null,null}
            });

            Assert.That(subject.CanRotateRight(), Is.True);
        }
        #endregion

        [Test]
        public void Test_AdvanceBlock_VerifyOverlapCheckerIsCalled()
        {
            var overlapCheckerMock = new Mock<OverlapChecker>();
            overlapCheckerMock
                .Setup(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()))
                .Returns(() => true);

            var subject = new Field(10, 10, overlapCheckerMock.Object);
            subject.SetBlock(UNUSED_BLOCK, new Vector2(3, 4));

            subject.AdvanceBlock();

            overlapCheckerMock.Verify(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()));
        }

        [Test]
        public void Test_AdvanceBlock_OverlapCheckerReturnsFalse_BlockDidNotMove()
        {
            var overlapCheckerMock = new Mock<OverlapChecker>();
            overlapCheckerMock
                .Setup(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()))
                .Returns(() => false);

            Field subject = new Field(10, 10, overlapCheckerMock.Object);

            subject.SetBlock(UNUSED_BLOCK, new Vector2(3, 4));

            subject.AdvanceBlock();

            Assert.AreEqual(new Vector2(3, 4), subject.Position);
        }

        [Test]
        public void Test_MoveLeft_VerifyOverlapCheckerIsCalled()
        {
            var overlapCheckerMock = new Mock<OverlapChecker>();
            overlapCheckerMock
                .Setup(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()))
                .Returns(() => true);

            var subject = new Field(10, 10, overlapCheckerMock.Object);
            subject.SetBlock(UNUSED_BLOCK, new Vector2(3, 4));

            subject.MoveBlockLeft();

            overlapCheckerMock.Verify(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()));
        }

        [Test]
        public void Test_MoveLeft_OverlapCheckerReturnsFalse_BlockDidNotMove()
        {
            var overlapCheckerMock = new Mock<OverlapChecker>();
            overlapCheckerMock
                .Setup(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()))
                .Returns(() => false);

            Field subject = new Field(10, 10, overlapCheckerMock.Object);
            subject.SetBlock(UNUSED_BLOCK, new Vector2(3, 4));
            subject.MoveBlockLeft();

            Assert.AreEqual(new Vector2(3, 4), subject.Position);
        }

        [Test]
        public void Test_MoveRight_VerifyOverlapCheckerIsCalled()
        {
            var overlapCheckerMock = new Mock<OverlapChecker>();
            overlapCheckerMock
                .Setup(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()))
                .Returns(() => true);

            var subject = new Field(10, 10, overlapCheckerMock.Object);
            subject.SetBlock(UNUSED_BLOCK, new Vector2(3, 4));

            subject.MoveBlockRight();

            overlapCheckerMock.Verify(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()));
        }

        [Test]
        public void Test_MoveRight_OverlapCheckerReturnsFalse_BlockDidNotMove()
        {
            var overlapCheckerStub = new Mock<OverlapChecker>();
            overlapCheckerStub
                .Setup(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()))
                .Returns(() => false);

            Field subject = new Field(10, 10, overlapCheckerStub.Object);
            subject.SetBlock(new BlockHelper.MockBlock(), new Vector2(3, 4));
            subject.MoveBlockRight();

            Assert.AreEqual(new Vector2(3, 4), subject.Position);
        }

        [Test]
        public void Test_CanMoveLeft_VerifyOverlapCheckerIsCalled()
        {
            var overlapCheckerMock = new Mock<OverlapChecker>();
            overlapCheckerMock
                .Setup(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()))
                .Returns(() => true);

            Field subject = new Field(10, 10, overlapCheckerMock.Object);
            subject.SetBlock(new BlockHelper.MockBlock(2, 1), new Vector2(1, 5));

            subject.CanMoveLeft();

            overlapCheckerMock.Verify(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()));
        }

        [Test]
        public void Test_CanMoveLeft_OverlapCheckerReturnsTrue_ReturnsTrue()
        {
            var overlapCheckerMock = new Mock<OverlapChecker>();
            overlapCheckerMock
                .Setup(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()))
                .Returns(() => true);

            Field subject = new Field(10, 10, overlapCheckerMock.Object);
            subject.SetBlock(new BlockHelper.MockBlock(2, 1), new Vector2(1, 5));

            Assert.That(subject.CanMoveLeft(), Is.True);
        }

        [Test]
        public void Test_CanMoveLeft_OverlapCheckerReturnsFalse_ReturnsFalse()
        {
            var overlapCheckerMock = new Mock<OverlapChecker>();
            overlapCheckerMock
                .Setup(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()))
                .Returns(() => false);

            Field subject = new Field(10, 10, overlapCheckerMock.Object);
            subject.SetBlock(new BlockHelper.MockBlock(2, 1), new Vector2(1, 5));

            Assert.That(subject.CanMoveLeft(), Is.False);
        }

        [Test]
        public void Test_CanMoveRight_VerifyOverlapCheckerIsCalled()
        {
            var overlapCheckerMock = new Mock<OverlapChecker>();
            overlapCheckerMock
                .Setup(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()))
                .Returns(() => true);

            Field subject = new Field(10, 10, overlapCheckerMock.Object);
            subject.SetBlock(new BlockHelper.MockBlock(2, 1), new Vector2(1, 5));

            subject.CanMoveRight();

            overlapCheckerMock.Verify(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()));
        }

        [Test]
        public void Test_CanMoveRight_OverlapCheckerReturnsTrue_ReturnsTrue()
        {
            var overlapCheckerMock = new Mock<OverlapChecker>();
            overlapCheckerMock
                .Setup(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()))
                .Returns(() => true);

            Field subject = new Field(10, 10, overlapCheckerMock.Object);
            subject.SetBlock(new BlockHelper.MockBlock(2, 1), new Vector2(1, 5));

            Assert.That(subject.CanMoveRight(), Is.True);
        }

        [Test]
        public void Test_CanMoveRight_OverlapCheckerReturnsFalse_ReturnsFalse()
        {
            var overlapCheckerMock = new Mock<OverlapChecker>();
            overlapCheckerMock
                .Setup(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()))
                .Returns(() => false);

            Field subject = new Field(10, 10, overlapCheckerMock.Object);
            subject.SetBlock(new BlockHelper.MockBlock(2, 1), new Vector2(1, 5));

            Assert.That(subject.CanMoveRight(), Is.False);
        }

        [Test]
        public void Test_CanAdvance_VerifyOverlapCheckerIsCalled()
        {
            var overlapCheckerMock = new Mock<OverlapChecker>();
            overlapCheckerMock
                .Setup(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()))
                .Returns(() => true);

            Field subject = new Field(10, 10, overlapCheckerMock.Object);
            subject.SetBlock(new BlockHelper.MockBlock(2, 1), new Vector2(1, 5));

            subject.CanAdvance();

            overlapCheckerMock.Verify(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()));
        }

        [Test]
        public void Test_CanAdvance_OverlapCheckerReturnsTrue_ReturnsTrue()
        {
            var overlapCheckerMock = new Mock<OverlapChecker>();
            overlapCheckerMock
                .Setup(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()))
                .Returns(() => true);

            Field subject = new Field(10, 10, overlapCheckerMock.Object);
            subject.SetBlock(new BlockHelper.MockBlock(2, 1), new Vector2(1, 5));

            Assert.That(subject.CanAdvance(), Is.True);
        }

        [Test]
        public void Test_CanAdvance_OverlapCheckerReturnsFalse_ReturnsFalse()
        {
            var overlapCheckerMock = new Mock<OverlapChecker>();
            overlapCheckerMock
                .Setup(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()))
                .Returns(() => false);

            Field subject = new Field(10, 10, overlapCheckerMock.Object);
            subject.SetBlock(new BlockHelper.MockBlock(2, 1), new Vector2(1, 5));

            Assert.That(subject.CanAdvance(), Is.False);
        }

        [Test]
        public void Test_CanRotateRight_VerifyOverlapCheckerIsCalled()
        {
            var overlapCheckerMock = new Mock<OverlapChecker>();
            overlapCheckerMock
                .Setup(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()))
                .Returns(() => true);

            int blockHeight = 2;

            Field subject = new Field(10, 10, overlapCheckerMock.Object);
            subject.SetBlock(new BlockHelper.MockBlock(1, blockHeight), new Vector2(2, 0));

            subject.CanRotateRight();

            overlapCheckerMock.Verify(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()));
        }

        [Test]
        public void Test_CanRotateRight_OverlapCheckerReturnsFalse_ReturnsFalse()
        {
            var overlapCheckerMock = new Mock<OverlapChecker>();
            overlapCheckerMock
                .Setup(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()))
                .Returns(() => false);

            int blockHeight = 2;

            Field subject = new Field(10, 10, overlapCheckerMock.Object);
            subject.SetBlock(new BlockHelper.MockBlock(1, blockHeight), new Vector2(2, 0));

            Assert.That(subject.CanRotateRight(), Is.False);
        }

        [Test]
        public void Test_CanRotateRight_OverlapCheckerReturnsTrue_ReturnsTrue()
        {
            var overlapCheckerMock = new Mock<OverlapChecker>();
            overlapCheckerMock
                .Setup(x => x.Check(It.IsAny<IBlock>(), It.IsAny<Vector2>(), It.IsAny<Color?[,]>()))
                .Returns(() => true);

            int blockHeight = 2;

            Field subject = new Field(10, 10, overlapCheckerMock.Object);
            subject.SetBlock(new BlockHelper.MockBlock(1, blockHeight), new Vector2(2, 0));

            Assert.That(subject.CanRotateRight(), Is.True);
        }
    }
}
