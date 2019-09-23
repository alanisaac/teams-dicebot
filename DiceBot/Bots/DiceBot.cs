using System;
using System.Threading;
using System.Threading.Tasks;
using DiceBot.DiceNotation;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace DiceBot.Bots
{
    public class DiceBot : ActivityHandler
    {
        private readonly IDiceRoller _diceRoller;

        public DiceBot(IDiceRoller diceRoller)
        {
            _diceRoller = diceRoller;
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var activity = turnContext.Activity;
            var rollIndex = activity.Text.IndexOf("roll", StringComparison.OrdinalIgnoreCase);
            var mentions = activity.GetMentions();
            
            if (rollIndex >= 0)
            {
                var expression = activity.Text.Substring(rollIndex, activity.Text.Length);
                var diceRollResult = await _diceRoller.Roll(expression);
                var results = string.Join(' ', diceRollResult.DiceRolls);
                await turnContext.SendActivityAsync(MessageFactory.Text($"I rolled: {results}.  Total: {diceRollResult.ExpressionTotal}"), cancellationToken);
            }
        }
    }
}
