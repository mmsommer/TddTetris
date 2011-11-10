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

            public void RotateLeft()
            {
                throw new NotImplementedException();
            }

            public void RotateRight()
            {
                throw new NotImplementedException();
            }

            public Color? ColorAt(int x, int y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
