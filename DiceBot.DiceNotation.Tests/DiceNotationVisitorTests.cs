using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiceBot.DiceNotation.Tests
{
    [TestClass]
    public class DiceNotationVisitorTests
    {
        [TestMethod]
        public void RollingSimpleDiceExpressionIsValid()
        {
            var parser = DiceNotationParser.FromString("3d6");
            var rng = new FakeDiceRng {1, 2, 3};
            var visitor = new DiceNotationVisitor(rng);
            var context = parser.notation();
            var result = visitor.VisitNotation(context);

            result.Should().Be(6);
            visitor.DiceRolls.Should().BeEquivalentTo(rng);
        }
        
        [TestMethod]
        public void RollingZeroDiceIsValid()
        {
            var parser = DiceNotationParser.FromString("0d6");
            var rng = new FakeDiceRng { 1, 2, 3 };
            var visitor = new DiceNotationVisitor(rng);
            var context = parser.notation();
            var result = visitor.VisitNotation(context);

            result.Should().Be(0);
            visitor.DiceRolls.Should().BeEmpty();
        }

        [TestMethod]
        public void AddingTwoDiceExpressionsIsValid()
        {
            var parser = DiceNotationParser.FromString("2d6+1d5");
            var rng = new FakeDiceRng { 1, 2, 3 };
            var visitor = new DiceNotationVisitor(rng);
            var context = parser.notation();
            var result = visitor.VisitNotation(context);

            result.Should().Be(6);
            visitor.DiceRolls.Should().BeEquivalentTo(rng);
        }


        [TestMethod]
        public void SubtractingTwoDiceExpressionsIsValid()
        {
            var parser = DiceNotationParser.FromString("2d6-1d5");
            var rng = new FakeDiceRng { 1, 2, 3 };
            var visitor = new DiceNotationVisitor(rng);
            var context = parser.notation();
            var result = visitor.VisitNotation(context);

            result.Should().Be(0);
            visitor.DiceRolls.Should().BeEquivalentTo(rng);
        }

        [TestMethod]
        public void RollingDiceWithZeroSidesIsInvalid()
        {
            var parser = DiceNotationParser.FromString("1d0");
            var rng = new FakeDiceRng { 1, 2, 3 };
            var visitor = new DiceNotationVisitor(rng);
            var context = parser.notation();
            Action action = () => visitor.VisitNotation(context);

            action.Should().Throw<InvalidOperationException>();
        }
    }
}
