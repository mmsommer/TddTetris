using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public class BlockFactory : IBlockFactory
    {
        private readonly Random random;

        private static bool x = true;
        private static bool _ = false;

        #region Shapes
        private static List<bool[,]> oShape = new List<bool[,]> {
          new bool[,] { { x, x },
                        { x, x } }
        };

        private static List<bool[,]> tShape = new List<bool[,]> {
          new bool[,] { { _, x, _ },
                        { x, x, x } },

          new bool[,] { { x, _ },
                        { x, x },
                        { x, _ } },

          new bool[,] { { x, x, x },
                        { _, x, _ } },

          new bool[,] { { _, x },
                        { x, x },
                        { _, x } }
        };

        private static List<bool[,]> sShape = new List<bool[,]> {
          new bool[,] { { _, x, x },
                        { x, x, _ } },

          new bool[,] { { x, _ },
                        { x, x },
                        { _, x } }
        };

        private static List<bool[,]> zShape = new List<bool[,]> {
          new bool[,] { { x, x, _ },
                        { _, x, x } },

          new bool[,] { { _, x },
                        { x, x },
                        { x, _ } }
        };

        private static List<bool[,]> iShape = new List<bool[,]> {
          new bool[,] { { x, x, x, x } },

          new bool[,] { { x },
                        { x },
                        { x },
                        { x } }
        };

        private static List<bool[,]> lShape = new List<bool[,]> {
          new bool[,] { { _, _, x },
                        { x, x, x } },

          new bool[,] { { x, _ },
                        { x, _ },
                        { x, x } },

          new bool[,] { { x, x, x },
                        { x, _, _ } },

          new bool[,] { { x, x },
                        { _, x },
                        { _, x } }
        };

        private static List<bool[,]> jShape = new List<bool[,]> {
          new bool[,] { { x, _, _ },
                        { x, x, x } },

          new bool[,] { { x, x },
                        { x, _ },
                        { x, _ } },

          new bool[,] { { x, x, x },
                        { _, _, x } },

          new bool[,] { { _, x },
                        { _, x },
                        { x, x } }
        };
        #endregion

        private static List<List<bool[,]>> shapes = new List<List<bool[,]>>
        { oShape, tShape, sShape, zShape, lShape, jShape, iShape };

        private static List<Color> colors = new List<Color>
        {
            Color.Blue, Color.Beige, Color.Red, Color.White, Color.Purple, Color.Green, Color.Orange
        };

        public BlockFactory()
        {
            this.random = new Random();
        }

        public IBlock MakeBlock()
        {
            int index = random.Next(shapes.Count);

            List<bool[,]> selectedShape = shapes[index];
            Color selectedColor = colors[index];

            return new Block( selectedShape, selectedColor );
        }
    }
}
