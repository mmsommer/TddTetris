using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public class Block : IBlock
    {
        public int Width { get { return shapes[currentShape].GetLength(1); } }
        public int Height { get { return shapes[currentShape].GetLength(0); } }

        private readonly List<bool[,]> shapes;
        private int currentShape;

        private readonly Color color;

        public Block( List<bool[,]> shapes, Color color )
        {
            this.shapes = shapes;
            this.color = color;

            this.currentShape = 0;
        }

        public void RotateLeft()
        {
            this.currentShape--;

            if (this.currentShape < 0)
                this.currentShape = this.shapes.Count - 1;
        }

        public void RotateRight()
        {
            this.currentShape++;

            this.currentShape = this.currentShape % shapes.Count;
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
