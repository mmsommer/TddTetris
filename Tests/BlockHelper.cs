using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TddTetris;
using Microsoft.Xna.Framework;

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
                throw new NotImplementedException();
            }

            public void RotateRight()
            {
                throw new NotImplementedException();
            }

            public Color? ColorAt(Vector2 position)
            {
                throw new NotImplementedException();
            }
        }
    }
}
