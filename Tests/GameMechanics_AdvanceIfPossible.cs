using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TddTetris;
using Moq;
using Microsoft.Xna.Framework;

namespace Tests
{
    [TestFixture]
    public class GameMechanics_AdvanceIfPossible
    {
        [Test]
        public void Test_AdvanceIfPossible_WhenPossible_ShouldAdvance()
        {
            Mock<IField> field = new Mock<IField>();
            field.Setup(f => f.CanAdvance()).Returns(true);

            GameMechanics subject = new GameMechanics(field.Object, new BlockHelper.MockBlockFactory());

            subject.AdvanceIfPossible();

            field.Verify(f => f.AdvanceBlock());
        }

        [Test]
        public void Test_AdvanceIfPossible_WhenNotPossible_ShouldFixBlock()
        {
            Mock<IField> field = new Mock<IField>();
            field.Setup(f => f.CanAdvance()).Returns(false);

            GameMechanics subject = new GameMechanics(field.Object, new BlockHelper.MockBlockFactory());

            subject.AdvanceIfPossible();

            field.Verify(f => f.FixBlock());
        }

        [Test]
        public void Test_AdvanceIfPossible_WhenNotPossible_ShouldSetNewBlock()
        {
            Mock<IField> field = new Mock<IField>();
            field.Setup(f => f.CanAdvance()).Returns(false);

            GameMechanics subject = new GameMechanics(field.Object, new BlockHelper.MockBlockFactory());

            subject.AdvanceIfPossible();

            field.Verify(f => f.SetBlock(It.IsAny<IBlock>(), It.IsAny<Vector2>()));
        }
    }
}
