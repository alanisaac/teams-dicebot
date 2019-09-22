using System.Threading.Tasks;
using DiceBot.Models;

namespace DiceBot.DiceNotation
{
    public class DiceRoller : IDiceRoller
    {
        private readonly IDiceRng _diceRng;

        public DiceRoller(IDiceRng diceRng)
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
