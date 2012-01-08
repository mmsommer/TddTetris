using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Moq;
using NUnit.Framework;
using TddTetris;

namespace Tests
{
    [TestFixture]
    public class GameMechanics_HandleInputTest
    {
        private Vector2 position;
        private Field field;
        private GameMechanics subject;

        [SetUp]
        public void SetUp()
        {
            IBlockFactory mockBlockFactory = new BlockHelper.MockBlockFactory();
            position = new Vector2(4, 4);
            field = new Field(10, 10);
            field.SetBlock(new BlockHelper.MockBlock(), position);
            subject = new GameMechanics(field, mockBlockFactory);
        }

        [Test]
        public void Test_HandleInput_WhenNoKeysArePressed()
        {
            List<Keys> input = new List<Keys>();
            subject.HandleInput(input);

            Assert.AreEqual(position, field.Position);
        }

        [Test]
        public void Test_HandleInput_WhenLeftIsPressed()
        {
            List<Keys> input = new List<Keys> { Keys.Left };

            subject.HandleInput(input);

            Assert.AreEqual(new Vector2(3, 4), field.Position);
        }

        [Test]
        public void Test_HandleInput_WhenRightIsPressed()
        {
            List<Keys> input = new List<Keys> { Keys.Right };

            subject.HandleInput(input);

            Assert.AreEqual(new Vector2(5, 4), field.Position);
        }

        [Test]
        public void Test_HandleInput_WhenSpaceIsPressed()
        {
            Mock<IField> currentField = new Mock<IField>();

            GameMechanics currentSubject = new GameMechanics(currentField.Object, new BlockHelper.MockBlockFactory());
            List<Keys> input = new List<Keys> { Keys.Space };

            currentSubject.HandleInput(input);
            currentField.Verify(f => f.FixBlock());
        }

        [Test]
        public void Test_HandleInput_WhenUpIsPressed()
        {
            Mock<IBlock> currentBlock = new Mock<IBlock>();

            Mock<IBlockFactory> factory = new Mock<IBlockFactory>();
            factory.Setup(f => f.MakeBlock()).Returns(currentBlock.Object);

            Mock<IField> field = new Mock<IField>();

            GameMechanics currentSubject = new GameMechanics(field.Object, factory.Object);
            currentSubject.NextBlock();

            List<Keys> input = new List<Keys> { Keys.Up };

            currentSubject.HandleInput(input);
            field.Verify(x => x.CanRotateRight());
        }
    }
}
