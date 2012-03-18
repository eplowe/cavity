namespace Cavity.Diagnostics
{
    using System;
    using System.Diagnostics;

    public static class CommandCounter
    {
        private const string _category = "Cavity";

        private const string _counter = "Commands/sec";

        private static readonly bool _exists = CounterExists;

        private static bool CounterExists
        {
            get
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
    }
}