namespace Cavity.Diagnostics
{
    using System;
    using System.Diagnostics;

    public static class CommandCounter
    {
        private const string Category = "Cavity";

        private const string Counter = "Commands/sec";

        private static readonly bool _exists = CounterExists;

        private static bool CounterExists
        {
            get
            {
                try
                {
                    return PerformanceCounterCategory.CounterExists(Counter, Category);
                }
                catch (InvalidOperationException)
                {
                    return false;
                }
            }
        }

        public static void Increment()
        {
            if (!_exists)
            {
                return;
            }

            using (var counter = new PerformanceCounter(Category, Counter, false)
            {
                MachineName = "."
            })
            {
                counter.Increment();
            }
        }
    }
}