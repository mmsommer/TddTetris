using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TddTetris;
using Microsoft.Xna.Framework;

namespace Tests
{
    [TestFixture]
    public class Field_FixBlockTest
    {
        private Field subject;
        private Vector2 position;
        private IBlock block;

        [SetUp]
        public void SetUp()
        {
            subject = new Field(4, 4);
            position = new Vector2(1, 1);
            block = new MockBlockImage(new bool[,]
                { { true, false },
                  { true, false }
                });
        }

        [Test]
        public void Test_FixBlock_SetsPresentColors()
        {
            subject.SetBlock(block, position);
            subject.FixBlock();
            subject.SetBlock(null, position); // This is necessary so the field doesn't use the block values -- Might be implemented better?

            Assert.AreEqual(Color.White, subject.ColorAt(new Vector2(1, 1)).Value);
            Assert.AreEqual(Color.White, subject.ColorAt(new Vector2(1, 2)).Value);
        }

        [Test]
        public void Test_FixBlock_DoesNotSetMissingColors()
        {
            Color white = Color.White;

            subject.SetContentsForTest( new Color?[,]
            { { null, null,  null,  null},
              { null, white, white, null },
              { null, white, white, null },
              { null, null,  null,  null} });

            subject.SetBlock(block, position);
            subject.FixBlock();
            subject.SetBlock(null, position); // This is necessary so the field doesn't use the block values -- Might be implemented better?

            // The values that are empty in the test block should not overwrite present values in Field
            Assert.AreEqual(Color.White, subject.ColorAt(new Vector2(2, 1)).Value);
            Assert.AreEqual(Color.White, subject.ColorAt(new Vector2(2, 2)).Value);
        }

        private class MockBlockImage : IBlock
        {
            private readonly bool[,] image;
            public MockBlockImage(bool[,] image)
            {
                this.image = image;
            }

            public int Width
            {
                get { return image.GetLength(1); }
            }

            public int Height
            {
                get { return image.GetLength(0); }
            }

            public void RotateLeft()
            {
                throw new NotImplementedException();
            }

            public void RotateRight()
            {
                throw new NotImplementedException();
            }

            public Color? ColorAt(Vector2 position)
            {
                if (image[(int)position.Y, (int)position.X])
                    return Color.White;
                else
                    return null;
            }
        }

    }
}
