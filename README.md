# Logging

Some utilities for using logging to measure application performance.

Example Usage:

## Via Using Statement

```
public static bool IsDivisibleBy(int candidate, int divisor)
{
    using (new CountLogger.DisposableStopwatch(nameof(IsDivisibleBy), CountLogger.AddDuration))
    {
        return candidate % divisor == 0;
    }
}
```

## try...finally

```
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
```

## Output
Use any lambda that takes a string. You can use Log.Information() here, or Console.WriteLine(), etc.
```
CountLogger.DumpResults(Console.WriteLine);
```
Run the console application in the sample folder to see sample output.