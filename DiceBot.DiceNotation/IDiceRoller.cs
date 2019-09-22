using System.Threading.Tasks;
using DiceBot.Models;

namespace DiceBot.DiceNotation
{
    public interface IDiceRoller
    {
        Task<DiceRollResult> Roll(string expression);
    }
}