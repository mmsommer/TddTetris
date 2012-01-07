using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public class Field : IField
    {
        private Color?[,] _field;

        public int Width { get; private set; }

        public int Height { get; private set; }

        public IBlock Block { get; private set; }

        public Vector2 Position { get; private set; }

        public Field(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            this._field = new Color?[width, height];
        }

        public Color? ColorAt(Vector2 position)
        {
            int x = (int)position.X;
            int y = (int)position.Y;

            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                throw new IndexOutOfRangeException();
            }

            return GetColorInBlock(position);
        }

        private Color? GetColorInBlock(Vector2 position)
        {
            int x = (int)position.X;
            int y = (int)position.Y;

            Color? result = null;
            if (Block != null && (x >= Position.X && x < Position.X + Block.Width && y >= Position.Y && y < Position.Y + Block.Height))
            {
                Vector2 resultingPosition = position - Position;

                result = Block.ColorAt(resultingPosition);
            }

            if (result.HasValue)
            {
                return result;
            }
            else
            {
                return _field[x, y];
            }
        }

        public void SetBlock(IBlock block, Vector2 position)
        {
            this.Block = block;
            this.Position = position;
        }

        public void AdvanceBlock()
        {
            Position = new Vector2(Position.X, Position.Y + 1);
        }

        public bool CanMoveLeft()
        {
            return Position.X > 0;
        }

        public void MoveBlockLeft()
        {
            Position = new Vector2(Position.X - 1, Position.Y);
        }

        public bool CanMoveRight()
        {
            return (Position.X + Block.Width) < Width;
        }

        public void MoveBlockRight()
        {
            Position = new Vector2(Position.X + 1, Position.Y);
        }

        public bool CanAdvance()
        {
            var futurePosition = Position + new Vector2(0, 1);
            return IsFuturePositionPossible(futurePosition);
        }

        public void FixBlock()
        {
            for (int x = 0; x < Block.Width; x++)
            {
                for (int y = 0; y < Block.Height; y++)
                {
                    Color? color = Block.ColorAt(new Vector2(x, y));

                    if (color.HasValue)
                    {
                        _field[(int)Position.X + x, (int)Position.Y + y] = color;
                    }
                }
            }
        }

        private bool IsFuturePositionPossible(Vector2 futurePosition)
        {
            var positionsToCheckInBlock = new List<Vector2>();
            for (int x = 0; x < Block.Width; x++)
            {
                for (var y = 0; y < Block.Height; y++)
                {
                    var position = new Vector2(x, y);
                    if (Block.ColorAt(position) != null)
                    {
                        positionsToCheckInBlock.Add(position + futurePosition);
                    }
                }
            }

            foreach (var position in positionsToCheckInBlock)
            {
                int x = Convert.ToInt32(Math.Round(position.X));
                int y = Convert.ToInt32(Math.Round(position.Y));
                try
                {
                    if (_field[x, y] != null)
                    {
                        return false;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    // appearantly the block is out of the field so
                    return false;
                }
            }

            // no collitions found if we come here so
            return true;
        }

        /* This is a helper method so we don't have to fix / remove blocks every time we
         * want to pre-fill the Field. The method name implies that this should only be used
         * for tests.
         */
        public void SetContentsForTest(Color?[,] field)
        {
            this._field = field;
        }
    }
}
