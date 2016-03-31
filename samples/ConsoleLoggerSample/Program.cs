using System;
using System.Diagnostics;
using Ardalis.Logging;

namespace ConsoleLoggerSample
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxNumber = 100000;
            Console.WriteLine($"Testing primes up to {maxNumber}.");
            var calculator = new PrimeCalculator();
            Console.WriteLine("Naive:");
            for (int i = 2; i < maxNumber; i++)
            {
                calculator.IsPrimeNaive(i);
            }
            Console.WriteLine("Optimized:");
            for (int i = 2; i < maxNumber; i++)
            {
                calculator.IsPrimeOptimized(i);
            }
            CountLogger.DumpResults(Console.WriteLine);
        }
    }

    public class PrimeCalculator
    {
        public bool IsPrimeNaive(int number)
        {
            var timer = Stopwatch.StartNew();
            try
            {
                if (number < 2) return false;
                for (int i = 2; i < number; i++)
                {
                    if (IsDivisibleBy(number, i)) return false;
                }
                return true;
            }
            finally
            {
                timer.Stop();
                CountLogger.AddDuration(nameof(IsPrimeNaive), timer.ElapsedMilliseconds);
            }
        }
        public bool IsPrimeOptimized(int number)
        {
            var timer = Stopwatch.StartNew();
            try
            {
                if (number < 2) return false;
                for (int i = 2; i <= Math.Sqrt(number); i++)
                {
                    if (IsDivisibleBy(number, i)) return false;
                }
                return true;
            }
            finally
            {
                timer.Stop();
                CountLogger.AddDuration(nameof(IsPrimeOptimized), timer.ElapsedMilliseconds);
            }
        }

        public static bool IsDivisibleBy(int candidate, int divisor)
        {
            using (new CountLogger.DisposableStopwatch(nameof(IsDivisibleBy), CountLogger.AddDuration))
            {
                return candidate % divisor == 0;
            }
        }
    }
}
