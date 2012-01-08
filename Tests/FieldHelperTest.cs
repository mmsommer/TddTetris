using Microsoft.Xna.Framework;
using NUnit.Framework;
using TddTetris;

namespace Tests
{
    public class FieldHelperTest
    {
        [Test]
        public void Test_IsPositionBlocked_PositionIsBlocked_ReturnsTrue()
        {
            var field = new Color?[,]{
                {Color.Red}
            };
            var subject = new FieldHelper();
            var vector = new Vector2(0, 0);

            Assert.That(subject.IsPositionBlocked(vector, field), Is.True);
        }

        [Test]
        public void Test_IsPositionBlocked_PositionIsFree_ReturnsFalse()
        {
            var field = new Color?[,]{
                {null}
            };
            var subject = new FieldHelper();
            var vector = new Vector2(0, 0);

            Assert.That(subject.IsPositionBlocked(vector, field), Is.False);
        }

        [Test]
        public void Test_IsPositionOutOfField_PositionIsOutOfField_ReturnsTrue()
        {
            var field = new Color?[,]{
                {null}
            };
            var subject = new FieldHelper();
            var position = new Vector2(-1, -1);

            Assert.That(subject.IsPositionOutOfField(position, field), Is.True);
        }

        [Test]
        public void Test_IsPositionOutOfField_PositionIsNotOutOfField_ReturnsFalse()
        {
            var field = new Color?[,]{
                {null}
            };
            var subject = new FieldHelper();
            var position = new Vector2(0, 0);

            Assert.That(subject.IsPositionOutOfField(position, field), Is.False);
        }
    }
}
