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
    public class Block_RotateTest
    {
        private static bool x = true;
        private static bool _ = false;

        private List<bool[,]> oShape = new List<bool[,]> { new bool[,]
            { { x, x },
              { x, x } } };

        private List<bool[,]> lShape = new List<bool[,]> {
            new bool[,]
            { { x, _ },
              { x, _ },
              { x, x } },

            new bool[,]
            { { _, _, x },
              { x, x, x } },

            new bool[,]
            { { x, x },
              { _, x },
              { _, x } },

            new bool[,]
            { { x, x, x },
              { x, _, _ } },
        };

        [Test]
        public void Test_WhenBlockHasOneShape_ItShouldNotChangeShape()
        {
            Color color = Color.Beige;

            Block subject = new Block(oShape, color);

            CompareAllShapesRight(subject, oShape, color);
        }

        [Test]
        public void Test_WhenBlockHasFourShapes_RotateRightShouldCycleThroughThem()
        {
            Color color = Color.Azure;

            Block subject = new Block(lShape, color);

            CompareAllShapesRight(subject, lShape, color);
        }

        [Test]
        public void Test_WhenBlockHasFourShapes_RotateLeftShouldCycleThroughThem()
        {
            Color color = Color.Azure;

            Block subject = new Block(lShape, color);

            CompareAllShapesLeft(subject, lShape, color);
        }

        private void CompareAllShapesRight(Block subject, List<bool[,]> shapes, Color color)
        {
            for (int i = 1; i < shapes.Count; i++)
            {
                subject.RotateRight();

                Assert.AreEqual(shapes[i].GetLength(1), subject.Width);
                Assert.AreEqual(shapes[i].GetLength(0), subject.Height);
                CompareBlock(subject, shapes[i], color);
            }

            subject.RotateRight();
            CompareBlock(subject, shapes[0], color);
        }

        private void CompareAllShapesLeft(Block subject, List<bool[,]> shapes, Color color)
        {
            for (int i = shapes.Count -1; i >= 0; i--)
            {
                int currentShape = i % shapes.Count;
                subject.RotateLeft();

                Assert.AreEqual(shapes[currentShape].GetLength(1), subject.Width);
                Assert.AreEqual(shapes[currentShape].GetLength(0), subject.Height);
                CompareBlock(subject, shapes[currentShape], color);
            }

            subject.RotateLeft();
            CompareBlock(subject, shapes[shapes.Count - 1], color);
        }

        private void CompareBlock(IBlock subject, bool[,] shape, Color color)
        {
            for (int x = 0; x < shape.GetLength(1); x++)
            {
                for (int y = 0; y < shape.GetLength(0); y++)
                {
                    if (shape[y, x])
                    {
                        Assert.AreEqual(color, subject.ColorAt(new Vector2(x, y)));
                    }
                    else
                    {
                        Assert.IsNull( subject.ColorAt(new Vector2(x, y)));
                    }
                }
            }
        }
    }
}
