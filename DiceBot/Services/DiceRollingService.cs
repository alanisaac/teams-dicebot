using System.Threading.Tasks;
using DiceBot.DiceNotation;
using DiceBot.Models;

namespace DiceBot.Services
{
    public class DiceRollingService : IDiceRollingService
    {
        private readonly IDiceRng _diceRng;

        public DiceRollingService(IDiceRng diceRng)
        {
            _diceRng = diceRng;
        }

        public Task<DiceRollResult> Roll(string expression)
        {
            var parser = DiceNotationParser.FromString(expression);
            var visitor = new DiceNotationVisitor(_diceRng);
            var expressionResult = visitor.VisitNotation(parser.notation());
            var diceRollResult = new DiceRollResult(visitor.DiceRolls, expressionResult);
            return Task.FromResult(diceRollResult);
        }
    }
}
