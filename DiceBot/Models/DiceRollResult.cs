using System.Collections.Generic;
using System.Linq;

namespace DiceBot.Models
{
    public class DiceRollResult
    {
        public DiceRollResult(IEnumerable<int> diceRolls)
        {
            DiceRolls = new List<int>(diceRolls).AsReadOnly();
        }

        /// <summary>
        /// The resulting dice rolls.
        /// </summary>
        public IReadOnlyList<int> DiceRolls { get; }

        /// <summary>
        /// The total value of all dice.
        /// </summary>
        public int Total => DiceRolls.Sum();
    }
}
