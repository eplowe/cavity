namespace Cavity.Diagnostics
{
    using System.Diagnostics;

    public static class CommandCounter
    {
        private const string Category = "Cavity";

        private const string Counter = "Commands/sec";

        private static readonly bool _exists = PerformanceCounterCategory.CounterExists(Counter, Category);

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