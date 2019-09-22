using System.Collections.Generic;

namespace DiceBot.Models
{
    public class DiceRollResult
    {
        public DiceRollResult(IEnumerable<int> diceRolls, int expressionTotal)
        {
            DiceRolls = new List<int>(diceRolls).AsReadOnly();
            ExpressionTotal = expressionTotal;
        }

        /// <summary>
        /// The resulting dice rolls.
        /// </summary>
        public IReadOnlyList<int> DiceRolls { get; }

        /// <summary>
        /// The total value of the dice expression.
        /// If additional operators besides dice are used, this value will not be equal to the total of all dice rolls.
        /// </summary>
        public int ExpressionTotal { get; }
    }
}
