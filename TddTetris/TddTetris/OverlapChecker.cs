using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public class OverlapChecker
    {
        public bool CheckPosition(Vector2 position, Color?[,] field)
        {
            int x = Convert.ToInt32(Math.Round(position.X));
            int y = Convert.ToInt32(Math.Round(position.Y));

            return field[x, y] != null;
        }

        public bool CheckFieldBoundaries(Vector2 position, Color?[,] field)
        {
            int x = Convert.ToInt32(Math.Round(position.X));
            int y = Convert.ToInt32(Math.Round(position.Y));

            return x < 0 ||
                y < 0 ||
                x >= field.GetLength(0) ||
                y >= field.GetLength(1);
        }

        public virtual bool Check(IBlock block, Vector2 futurePosition, Color?[,] field)
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
                if (CheckFieldBoundaries(position, field))
                {
                    return false;
                }
                if (CheckPosition(position, field))
                {
                    return false;
                }
            }

            // no collitions found if we come here so
            return true;
        }
    }
}
