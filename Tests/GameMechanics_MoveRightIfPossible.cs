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
    public class GameMechanics_MoveRightIfPossible
    {
        [Test]
        public void Test_MoveRightIfPossible_WhenPossible_ShouldMoveRight()
        {
            Mock<IField> field = new Mock<IField>();
            field.Setup(f => f.CanMoveRight()).Returns(true);

            GameMechanics subject = new GameMechanics(field.Object, new BlockHelper.MockBlockFactory());

            subject.MoveRightIfPossible();

            field.Verify(f => f.MoveBlockRight());
        }

        [Test]
        public void Test_MoveRightIfPossible_WhenNotPossible_ShouldNotMoveRight()
        {
            Mock<IField> field = new Mock<IField>();
            field.Setup(f => f.CanMoveRight()).Returns(false);

            GameMechanics subjet = new GameMechanics(field.Object, new BlockHelper.MockBlockFactory());

            subjet.MoveRightIfPossible();

            field.Verify(f => f.MoveBlockRight(), Times.Never());
        }
    }
}
