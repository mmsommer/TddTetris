using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using TddTetris;
using Microsoft.Xna.Framework;

namespace Tests
{
    [TestFixture]
    public class GameMechanics_NextBlock
    {
        [Test]
        public void Test_NextBlock_ItSetsANewBlock()
        {
            Mock<IField> field = new Mock<IField>();
            field.Setup(f => f.CanAdvance()).Returns(false);

            GameMechanics subject = new GameMechanics(field.Object, new BlockHelper.MockBlockFactory());

            subject.NextBlock();

            field.Verify(f => f.SetBlock(It.IsAny<IBlock>(), It.IsAny<Vector2>()));
        }
    }
}
