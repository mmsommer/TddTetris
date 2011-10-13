﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public class Field : IField
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Field(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public Color? ColorAt(Vector2 position)
        {
            float x = position.X;
            float y = position.Y;

            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                throw new IndexOutOfRangeException();
            }

            if (position == Position)
            {
                return Color.White;
            }

            return null;
        }

        public Block Block { get; private set; }

        public Vector2 Position { get; private set; }

        public void SetBlock(Block block, Vector2 position)
        {
            this.Block = block;
            this.Position = position;
        }

        public void AdvanceBlock()
        {
            Position = new Vector2(Position.X, Position.Y + 1);
        }

        public void MoveBlockLeft()
        {
            Position = new Vector2(Position.X - 1, Position.Y);
        }

        public void MoveBlockRight()
        {
            Position = new Vector2(Position.X + 1, Position.Y);
        }

        public bool CanAdvance()
        {
            return Position.Y < Height - 1;
        }

        public void FixBlock()
        {
        }
    }
}
