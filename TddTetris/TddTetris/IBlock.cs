using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public interface IBlock
    {
        int Width  { get; }
        int Height { get; }

        void RotateLeft();
        void RotateRight();

        Color? ColorAt(Vector2 position);
    }
}

