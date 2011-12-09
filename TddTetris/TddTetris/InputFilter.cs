using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace TddTetris
{
    public class InputFilter
    {
        private Keys[] previouslyPressed;

        public InputFilter()
        {
            previouslyPressed = new Keys[] {};
        }

        public Keys[] Filter(Keys[] keys)
        {
            Keys[] newKeys = keys.Where(key => !previouslyPressed.Contains(key)).ToArray();

            previouslyPressed = keys;

            return newKeys;
        }
    }
}
