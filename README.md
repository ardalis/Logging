# Logging

Some utilities for using logging to measure application performance.

Example Usage:

## Via Using Statement

```
public static bool IsDivisibleBy(int candidate, int divisor)
{
    using (new CountLogger.DisposableStopwatch(CountLogger.AddDuration))
    {
        return candidate % divisor == 0;
    }
}
```

## try...finally

Wrap the body of the method you want to measure in a try...finally block and start a timer at the start of the method:

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
        CountLogger.AddDuration(timer.ElapsedMilliseconds);
    }
}
```

## Output

Use any lambda that takes a string. You can use Log.Information() here, or Console.WriteLine(), etc.

```
CountLogger.DumpResults(Console.WriteLine);
```

Run the console application in the sample folder to see sample output.