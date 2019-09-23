using System;

namespace DiceBot.DiceNotation
{
    internal static class DiceRngValidator
    {
        /// <summary>
        /// Throws an exception if the number of sides is invalid.
        /// </summary>
        /// <param name="sides">The number of sides.</param>
        public static void ValidateSides(int sides)
        {
            if (sides < 1)
            {
                throw new InvalidOperationException("Cannot roll dice with less than 1 side.");
            }
        }
    }
}
