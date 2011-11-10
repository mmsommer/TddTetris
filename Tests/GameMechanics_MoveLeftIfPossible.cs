using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TddTetris;
using Moq;

namespace Tests
{
    [TestFixture]
    public class GameMechanics_MoveLeftIfPossible
    {
        private static IBlockFactory UNUSED_BLOCK_FACTORY = null;
        [Test]
        public void Test_MoveLeftIfPossible_WhenPossible_ShouldMoveLeft()
        {
            Mock<IField> field = new Mock<IField>();
            field.Setup(f => f.CanMoveLeft()).Returns(true);

            GameMechanics subject = new GameMechanics(field.Object, UNUSED_BLOCK_FACTORY);

            subject.MoveLeftIfPossible();

            field.Verify(f => f.MoveBlockLeft());
        }

        [Test]
        public void Test_MoveLeftIfPossible_WhenNotPossible_ShouldNotMoveRight()
        {
            Mock<IField> field = new Mock<IField>();
            field.Setup(f => f.CanMoveLeft()).Returns(false);

            GameMechanics subjet = new GameMechanics(field.Object, UNUSED_BLOCK_FACTORY);

            subjet.MoveLeftIfPossible();

            field.Verify(f => f.MoveBlockLeft(), Times.Never());
        }
    }
}
