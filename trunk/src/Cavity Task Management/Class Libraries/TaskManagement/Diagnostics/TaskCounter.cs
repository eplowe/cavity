namespace Cavity.Diagnostics
{
    using System;
    using System.Diagnostics;

    public static class TaskCounter
    {
        private const string Category = "Cavity";

        private const string Counter = "Tasks/sec";

        private static readonly bool _exists = CounterExists();

        public static void Increment()
        {
            if (_exists)
            {
                using (var counter = new PerformanceCounter(Category, Counter, false)
                {
                    MachineName = "."
                })
                {
                    counter.Increment();
                }
            }
        }

        private static bool CounterExists()
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
}