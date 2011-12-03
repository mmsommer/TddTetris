using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public class BlockFactory : IBlockFactory
    {
        public IBlock MakeBlock()
        {
            bool[,] blockShape = new bool[,] {
                {true, false },
                {true, true },
                {false, true} };

            return new Block( new List<bool[,]> { blockShape } , Color.White );
        }
    }
}
