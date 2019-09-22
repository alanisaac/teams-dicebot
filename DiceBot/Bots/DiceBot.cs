using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using DiceBot.Services;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using OnePlat.DiceNotation;
using OnePlat.DiceNotation.DieRoller;

namespace DiceBot.Bots
{
    public class DiceBot : ActivityHandler
    {
        private readonly IDiceRollingService _diceRollingService;

        public DiceBot(IDiceRollingService diceRollingService)
        {
            _diceRollingService = diceRollingService;
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var rollIndex = turnContext.Activity.Text.IndexOf("roll", StringComparison.OrdinalIgnoreCase);
            var mentions = turnContext.Activity.GetMentions();
            
            if (rollIndex >= 0)
            {
                var 
                var dice = new Dice();
                var result = dice.Roll("3d6", _roller);
                var results = string.Join(' ', result.Results.Select(x => x.Value));
                await turnContext.SendActivityAsync(MessageFactory.Text($"I'm going to roll the dice! {results}"), cancellationToken);
            }
        }
    }
}
