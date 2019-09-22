using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiceBot.DiceNotation.Tests
{
    [TestClass]
    public class DiceRollerTests
    {
        [TestMethod]
        public async Task RollingSimpleDiceExpressionIsValid()
        {
            var rng = new FakeDiceRng { 1, 2, 3 };
            var diceRoller = new DiceRoller(rng);
            var result = await diceRoller.Roll("3d6");

            result.ExpressionTotal.Should().Be(6);
            result.DiceRolls.Should().BeEquivalentTo(rng);
        }
        
        [TestMethod]
        public async Task RollingZeroDiceIsValid()
        {
            var rng = new FakeDiceRng { 1, 2, 3 };
            var diceRoller = new DiceRoller(rng);
            var result = await diceRoller.Roll("0d6");

            result.ExpressionTotal.Should().Be(0);
            result.DiceRolls.Should().BeEmpty();
        }

        [TestMethod]
        public async Task WhenNoCountIsSpecifiedOneDiceIsRolled()
        {
            var rng = new FakeDiceRng { 1, 2, 3 };
            var diceRoller = new DiceRoller(rng);
            var result = await diceRoller.Roll("d6");

            result.ExpressionTotal.Should().Be(1);
            result.DiceRolls.Should().BeEquivalentTo(rng.Take(1));
        }

        [TestMethod]
        public async Task AddingTwoDiceIsValid()
        {
            var rng = new FakeDiceRng { 1, 2, 3 };
            var diceRoller = new DiceRoller(rng);
            var result = await diceRoller.Roll("2d6+1d5");

            result.ExpressionTotal.Should().Be(6);
            result.DiceRolls.Should().BeEquivalentTo(rng);
        }
        
        [TestMethod]
        public async Task SubtractingTwoDiceIsValid()
        {
            var rng = new FakeDiceRng { 1, 2, 3 };
            var diceRoller = new DiceRoller(rng);
            var result = await diceRoller.Roll("2d6-1d5");

            result.ExpressionTotal.Should().Be(0);
            result.DiceRolls.Should().BeEquivalentTo(rng);
        }

        [TestMethod]
        public async Task AddingAConstantToDiceIsValid()
        {
            var rng = new FakeDiceRng { 1, 2, 3 };
            var diceRoller = new DiceRoller(rng);
            var result = await diceRoller.Roll("2d6+10");

            result.ExpressionTotal.Should().Be(13);
            result.DiceRolls.Should().BeEquivalentTo(rng.Take(2));
        }

        [TestMethod]
        public async Task SubtractingAConstantFromDiceIsValid()
        {
            var rng = new FakeDiceRng { 1, 2, 3 };
            var diceRoller = new DiceRoller(rng);
            var result = await diceRoller.Roll("2d6-10");

            result.ExpressionTotal.Should().Be(-7);
            result.DiceRolls.Should().BeEquivalentTo(rng.Take(2));
        }
        
        [TestMethod]
        public async Task MultiplyingDiceByAConstantIsValid()
        {
            var rng = new FakeDiceRng { 1, 2, 3 };
            var diceRoller = new DiceRoller(rng);
            var result = await diceRoller.Roll("2d6*2");

            result.ExpressionTotal.Should().Be(6);
            result.DiceRolls.Should().BeEquivalentTo(rng.Take(2));
        }

        [TestMethod]
        public async Task DividingDiceByAConstantIsValid()
        {
            var rng = new FakeDiceRng { 1, 2, 3 };
            var diceRoller = new DiceRoller(rng);
            var result = await diceRoller.Roll("3d6/2");

            result.ExpressionTotal.Should().Be(3);
            result.DiceRolls.Should().BeEquivalentTo(rng);
        }

        [TestMethod]
        public async Task MultiplyingTakesPrecedenceOverAdding()
        {
            var rng = new FakeDiceRng { 1, 2, 3 };
            var diceRoller = new DiceRoller(rng);
            var result = await diceRoller.Roll("3d6+2*2");

            result.ExpressionTotal.Should().Be(10);
            result.DiceRolls.Should().BeEquivalentTo(rng);
        }
        
        [TestMethod]
        public async Task ParenthesesTakePrecedence()
        {
            var rng = new FakeDiceRng { 1, 2, 3 };
            var diceRoller = new DiceRoller(rng);
            var result = await diceRoller.Roll("(3d6+2)*2");

            result.ExpressionTotal.Should().Be(16);
            result.DiceRolls.Should().BeEquivalentTo(rng);
        }

        [TestMethod]
        public async Task RollingDiceWithZeroSidesIsInvalid()
        {
            var rng = new FakeDiceRng { 1, 2, 3 };
            var diceRoller = new DiceRoller(rng);
            Func<Task> task = async () => await diceRoller.Roll("1d0");

            await task.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}
