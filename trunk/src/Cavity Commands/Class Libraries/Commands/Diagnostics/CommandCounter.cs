namespace Cavity.Diagnostics
{
    using System;
    using System.ComponentModel;
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
                catch (UnauthorizedAccessException)
                {
                    return false;
                }
                catch (Win32Exception)
                {
                    return false;
                }
            }
        }

        public static void Increment()
        {
            if (_exists)
            {
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
}