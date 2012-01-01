namespace Cavity.Examples
{
    using System;

    public sealed class Tester : ITest
    {
        public Tester()
        {
        }

        public Tester(string expected)
        {
            Expected = expected;
        }

        public string Expected { get; set; }

        public bool Test(string actual)
        {
            return string.Equals(Expected, actual, StringComparison.Ordinal);
        }
    }
}