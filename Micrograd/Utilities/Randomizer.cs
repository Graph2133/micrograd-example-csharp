using System;

namespace Micrograd.Utilities
{
    internal class Randomizer
    {
        private static readonly Random random = new Random();

        private static readonly object syncLock = new object();

        public static double GetRandomValue()
        {
            lock (syncLock)
            {
                return random.NextDouble() * 2 - 1;
            }
        }
    }
}
