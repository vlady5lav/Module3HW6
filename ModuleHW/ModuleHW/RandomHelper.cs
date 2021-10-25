using System;

namespace ModuleHW
{
    public static class RandomHelper
    {
        private static readonly Random _random = new Random();

        public static int GetRandomNumber(int minValue = 0, int maxValue = 1000)
        {
            return _random.Next(minValue, maxValue);
        }

        public static int GetRandomNumber(int maxValue = 1000)
        {
            return _random.Next(0, maxValue);
        }

        public static int GetRandomNumber()
        {
            return _random.Next(0, 1000);
        }
    }
}
