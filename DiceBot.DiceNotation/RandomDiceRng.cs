using System;

namespace DiceBot.DiceNotation
{
    /// <summary>
    /// Dice RNG adapter for <see cref="Random"/>.
    /// </summary>
    public class RandomDiceRng : IDiceRng
    {
        private readonly Random _random;

        public RandomDiceRng(Random random)
        {
            _random = random;
        }

        public int GetNextRoll(int sides)
        {
            DiceRngValidator.ValidateSides(sides);

            var roll = _random.Next(1, sides);
            return roll;
        }
    }
}
