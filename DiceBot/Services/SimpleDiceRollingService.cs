using System.Security.Cryptography;
using System.Threading.Tasks;
using DiceBot.DiceNotation;
using DiceBot.Models;

namespace DiceBot.Services
{
    public class SimpleDiceRollingService : IDiceRollingService
    {
        private readonly RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();

        public Task<DiceRollResult> Roll(string expression)
        {
            
        }
    }
}
