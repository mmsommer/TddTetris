using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TddTetris;
using NUnit.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tests
{
    [TestFixture]
    public class InputQueueTest
    {
        private InputQueue subject;

        [SetUp]
        public void SetUp()
        {
            subject = new InputQueue();
        }

        [Test]
        public void Test_NoKeysPressed_ShouldReturnNothing()
        {
            List<Keys> result = subject.keyPress(new List<Keys>());

            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public void Test_KeyPressed_ShouldReturnKey()
        {
            List<Keys> result = subject.keyPress(new List<Keys>() { Keys.Down });
            CollectionAssert.Contains(result, Keys.Down);
        }

        [Test]
        public void Test_KeyPressed_WhenAlreadyExists_ShouldNotReturnKey()
        {
            subject.keyPress(new List<Keys>() { Keys.Down });
            List<Keys> result = subject.keyPress(new List<Keys>() { Keys.Down });

            CollectionAssert.DoesNotContain(result, Keys.Down);
        }
    }
}
