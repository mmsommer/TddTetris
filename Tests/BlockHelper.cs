using System;
using Microsoft.Xna.Framework;
using TddTetris;

namespace Tests
{
    public class BlockHelper
    {
        public class MockBlockFactory : IBlockFactory
        {
            public IBlock MakeBlock()
            {
                return new MockBlock();
            }
        }

        public class MockBlock : IBlock
        {
            public int Width { get; private set; }
            public int Height { get; private set; }

            public MockBlock()
            {
            }

            public MockBlock(int width, int height)
            {
                this.Width = width;
                this.Height = height;
            }

            public void RotateLeft()
            {
                Rotate();
            }

            private void Rotate()
            {

                var width = this.Height;
                var height = this.Width;
                this.Width = width;
                this.Height = height;
            }

            public void RotateRight()
            {
                Rotate();
            }

            public Color? ColorAt(Vector2 position)
            {
                var x = Convert.ToInt32(Math.Round(position.X));
                var y = Convert.ToInt32(Math.Round(position.Y));
                if (this.Width >= x && this.Height >= x)
                {
                    return Color.Red;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
