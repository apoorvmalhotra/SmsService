using System;

namespace SmsService.Utilities
{
    public static class RandomNumberGenerator
    {
        public static int GetRandom(int minDigits, int maxDigits)
        {
            var random = new Random();

            if (minDigits < 1 || minDigits > maxDigits)
                throw new ArgumentOutOfRangeException();

            return random.Next((int)Math.Pow(10, minDigits - 1), (int)Math.Pow(10, maxDigits - 1));
        }
    }
}