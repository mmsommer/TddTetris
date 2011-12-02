using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public class Block : IBlock
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private readonly bool[,] values;

        private readonly Color color;

        public Block( bool[,] values, Color color )
        {
            this.values = values;
            this.color = color;

            this.Width = values.GetLength(1);
            this.Height = values.GetLength(0);
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
            bool present = this.values[(int)position.Y, (int)position.X];

            if (present)
            {
                return this.color;
            }
            else
            {
                return null;
            }
        }
    }
}
