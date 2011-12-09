using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Microsoft.Xna.Framework.Input;
using TddTetris;

namespace Tests
{
    [TestFixture]
    public class InputFilterTest
    {
        private InputFilter subject;

        [SetUp]
        public void SetUp()
        {
            this.subject = new InputFilter();
        }

        [Test]
        public void Filter_WhenNoKeysArePressed_ReturnsNoKeys()
        {
            Keys[] result = subject.Filter( new Keys[] {} );

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Filter_WhenOneKeyIsPressed_ReturnsOneKey()
        {
            Keys key = Keys.A;

            Keys[] result = subject.Filter(new Keys[] { key });

            Assert.That(result, Has.Length.EqualTo(1));
        }

        [Test]
        public void Filter_WhenOneKeyIsPressed_ReturnsThatKey()
        {
            Keys key = Keys.A;

            Keys[] result = subject.Filter(new Keys[] { key });

            Assert.That(result, Is.EquivalentTo(new Keys[] { key }));
        }

        [Test]
        public void Filter_WhenOneKeyWasPressedAndIsStillPressed_ReturnsNothing()
        {
            Keys[] keys = new Keys[] { Keys.Enter };

            subject.Filter(keys);

            Keys[] result = subject.Filter(keys);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Filter_WhenOneKeyWasPressedAndANewKeyIsPressed_ReturnsTheNewKey()
        {
            subject.Filter(new Keys[] { Keys.Enter });
            Keys[] result = subject.Filter(new Keys[] { Keys.B });

            Assert.That(result, Is.EquivalentTo( new Keys[] { Keys.B }) );
        }
    }
}
