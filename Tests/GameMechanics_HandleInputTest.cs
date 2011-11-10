using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TddTetris;
using Microsoft.Xna.Framework;
using System.Collections;
using Microsoft.Xna.Framework.Input;
using Moq;

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
            subject.HandleInput( input );

            Assert.AreEqual( position, field.Position );
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
    }
}
