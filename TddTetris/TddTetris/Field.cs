using System;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public class Field : IField
    {
        private Color?[,] _field;

        private Vector2 _stepLeft = new Vector2(-1, 0);

        private Vector2 _stepRight = new Vector2(1, 0);

        private Vector2 _stepDown = new Vector2(0, 1);

        public int Width { get; private set; }

        public int Height { get; private set; }

        public IBlock Block { get; private set; }

        public Vector2 Position { get; private set; }

        public OverlapChecker OverlapChecker { get; private set; }

        public Field(int width, int height, OverlapChecker overlapChecker = null)
        {
            this.Width = width;
            this.Height = height;

            this._field = new Color?[width, height];
            this.OverlapChecker = overlapChecker ?? new OverlapChecker();
        }

        public Color? ColorAt(Vector2 position)
        {
            int x = (int)position.X;
            int y = (int)position.Y;

            if (OverlapChecker.CheckFieldBoundaries(position, _field))
            {
                throw new IndexOutOfRangeException();
            }

            return GetColorInBlock(position);
        }

        public void SetBlock(IBlock block, Vector2 position)
        {
            this.Block = block;
            this.Position = position;
        }

        public void AdvanceBlock()
        {
            if (CanAdvance())
            {
                Position = Position + _stepDown;
            }
        }

        public bool CanMoveLeft()
        {
            return CheckNewPosition(Position + _stepLeft);
        }

        public void MoveBlockLeft()
        {
            if (CanMoveLeft())
            {
                Position = Position + _stepLeft;
            }
        }

        public bool CanMoveRight()
        {
            return CheckNewPosition(Position + _stepRight);
        }

        public void MoveBlockRight()
        {
            if (CanMoveRight())
            {
                Position = Position + _stepRight;
            }
        }

        public bool CanAdvance()
        {
            return CheckNewPosition(Position + _stepDown);
        }

        public bool CanRotateRight()
        {
            Block.RotateRight();
            var canRotate = OverlapChecker.Check(Block, Position, _field);
            Block.RotateLeft();

            return canRotate;
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

        /* This is a helper method so we don't have to fix / remove blocks every time we
         * want to pre-fill the Field. The method name implies that this should only be used
         * for tests.
         */
        public void SetContentsForTest(Color?[,] field)
        {
            this._field = field;
        }

        private bool CheckNewPosition(Vector2 newPosition)
        {
            return OverlapChecker.Check(Block, newPosition, _field);
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
    }
}
