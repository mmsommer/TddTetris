using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public class FieldHelper
    {
        public bool IsPositionBlocked(Vector2 position, Color?[,] field)
        {
            int x = Convert.ToInt32(Math.Round(position.X));
            int y = Convert.ToInt32(Math.Round(position.Y));

            return field[x, y] != null;
        }

        public bool IsPositionOutOfField(Vector2 position, Color?[,] field)
        {
            int x = Convert.ToInt32(Math.Round(position.X));
            int y = Convert.ToInt32(Math.Round(position.Y));

            try
            {
                var result = field[x, y];
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
            return false;
        }

        public bool IsFutureBlockPositionPossible(IBlock block, Vector2 futurePosition, Color?[,] field)
        {
            var positionsToCheckInBlock = new List<Vector2>();
            for (int x = 0; x < block.Width; x++)
            {
                for (var y = 0; y < block.Height; y++)
                {
                    var position = new Vector2(x, y);
                    if (block.ColorAt(position) != null)
                    {
                        positionsToCheckInBlock.Add(position + futurePosition);
                    }
                }
            }

            foreach (var position in positionsToCheckInBlock)
            {
                if (IsPositionOutOfField(position, field))
                {
                    return false;
                }
                if (IsPositionBlocked(position, field))
                {
                    return false;
                }
            }

            // no collitions found if we come here so
            return true;
        }
    }
}
