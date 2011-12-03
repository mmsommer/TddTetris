using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TddTetris;
using Microsoft.Xna.Framework;

namespace Tests
{
    [TestFixture]
    public class BlockTest
    {
        private List<bool[,]> blockValues;
        private Color blockColor = Color.White;

        [SetUp]
        public void SetUp()
        {
            this.blockValues = new List<bool[,]> { new bool[,] {
              { false, true },
              { false, true } }};
        }

        [Test]
        public void TestColorAt_WhenEmptySpot_ReturnsNull()
        {
            Block block = new Block(this.blockValues, this.blockColor);

            Assert.False(block.ColorAt(new Vector2(0, 0)).HasValue);
        }

        [Test]
        public void TestColorAt_WhenFilledSpot_ReturnsTheBlockColor()
        {
            Block block = new Block(this.blockValues, this.blockColor);

            Assert.AreEqual(this.blockColor, block.ColorAt(new Vector2(1, 0)).Value);
        }

        /* In order to make block creation in source more readable, swap X and Y in Block
         * [ [ x, - ],  <--- This is (0,0) and (0,1)
         *   [ -, x ] ] <--- This is (1,0) and (1,1)
         * 
         * In other words, the first coordinate (which usually points to X) points to Y and vice versa.
         */
        [Test]
        public void TestWidth_ShouldReturnBlockWidth()
        {
            List<bool[,]> blockValues = new List<bool[,]> { new bool[2, 4] };

            Block block = new Block(blockValues, Color.White);

            Assert.AreEqual(4, block.Width);
        }

        [Test]
        public void TestHeight_ShouldReturnBlockHeight()
        {
            List<bool[,]> blockValues = new List<bool[,]> { new bool [2, 4] };

            Block block = new Block(blockValues, Color.White);

            Assert.AreEqual(2, block.Height);
        }
    }
}
