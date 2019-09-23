using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DiceBot.DiceNotation.Tests
{
    public class FakeDiceRng : IDiceRng, IEnumerable<int>
    {
        private readonly List<int> _rolls;
        private int _currentIndex;

        public FakeDiceRng()
        {
            _rolls = new List<int>();
        }
        
        public IEnumerable<int> PastRolls => _rolls.Take(_currentIndex);

        public IEnumerable<int> UpcomingRolls => _rolls.Skip(_currentIndex);

        public int GetNextRoll(int sides)
        {
            if (_currentIndex >= _rolls.Count)
            {
                throw new InvalidOperationException("The fake dice RNG has run out of rolls.  Do you need to set up more rolls for this test?");
            }

            var roll = _rolls[_currentIndex];
            _currentIndex++;
            return roll;
        }

        public void Add(int roll)
        {
            _rolls.Add(roll);
        }

        public IEnumerator<int> GetEnumerator()
        {
            return _rolls.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
