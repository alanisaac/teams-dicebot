using System.Threading.Tasks;
using DiceBot.Models;

namespace DiceBot.Services
{
    public interface IDiceRollingService
    {
        /// <summary>
        /// Rolls dice based on the given dice expression.
        /// </summary>
        /// <param name="expression">The dice expression.</param>
        /// <returns>The dice roll result.</returns>
        Task<DiceRollResult> Roll(string expression);
    }
}
