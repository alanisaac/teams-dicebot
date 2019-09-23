using System;
using System.Security.Cryptography;

namespace DiceBot.DiceNotation
{
    /// <summary>
    /// Dice RNG adapter for <see cref="RNGCryptoServiceProvider"/>.
    /// </summary>
    public class CryptoDiceRng : IDiceRng, IDisposable
    {
        private readonly RNGCryptoServiceProvider _cryptoServiceProvider;

        public CryptoDiceRng(RNGCryptoServiceProvider cryptoServiceProvider)
        {
            _cryptoServiceProvider = cryptoServiceProvider;
        }

        public int GetNextRoll(int sides)
        {
            DiceRngValidator.ValidateSides(sides);

            var data = new byte[4];
            _cryptoServiceProvider.GetBytes(data);
            var value = BitConverter.ToInt32(data, 0);
            return (value % sides) + 1;
        }

        public void Dispose()
        {
            _cryptoServiceProvider?.Dispose();
        }
    }
}
