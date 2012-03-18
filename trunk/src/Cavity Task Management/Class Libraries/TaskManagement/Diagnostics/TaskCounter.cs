namespace Cavity.Diagnostics
{
    using System;
    using System.Diagnostics;

    public static class TaskCounter
    {
        private const string _category = "Cavity";

        private const string _counter = "Tasks/sec";

        private static readonly bool _exists = CounterExists();

        public static void Increment()
        {
            if (!_exists)
            {
                return;
            }

            using (var counter = new PerformanceCounter(_category, _counter, false)
                                     {
                                         MachineName = "."
                                     })
            {
                counter.Increment();
            }
        }

        private static bool CounterExists()
        {
            try
            {
                return PerformanceCounterCategory.CounterExists(_counter, _category);
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
    }
}