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

        private readonly List<bool[,]> shapes;
        private int currentShape;

        private readonly Color color;

        public Block( List<bool[,]> shapes, Color color )
        {
            this.shapes = shapes;
            this.color = color;

            this.Width = shapes[0].GetLength(1);
            this.Height = shapes[0].GetLength(0);

            this.currentShape = 0;
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
            bool present = this.shapes[currentShape][(int)position.Y, (int)position.X];

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
