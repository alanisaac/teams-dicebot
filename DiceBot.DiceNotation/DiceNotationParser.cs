using Antlr4.Runtime;

namespace DiceBot.DiceNotation
{
    public partial class DiceNotationParser
    {
        public static DiceNotationParser FromString(string input)
        {
            var inputStream = new AntlrInputStream(input);
            var diceNotationLexer = new DiceNotationLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(diceNotationLexer);
            var diceNotationParser = new DiceNotationParser(commonTokenStream);
            return diceNotationParser;
        }
    }
}
