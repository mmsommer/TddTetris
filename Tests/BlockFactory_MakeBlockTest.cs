using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TddTetris;

namespace Tests
{
    [TestFixture]
    public class BlockFactory_MakeBlockTest
    {
        [Test]
        public void ShouldReturnBlock()
        {
            BlockFactory blockFactory = new BlockFactory();

            IBlock block = blockFactory.MakeBlock();

            Assert.IsInstanceOf<Block>(block);
        }
    }
}
