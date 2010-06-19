namespace Example
{
    using System;

    public sealed class Tester : ITest
    {
        public Tester(string expected)
        {
            this.Expected = expected;
        }

        public string Expected
        {
            get;
            set;
        }

        public bool Test(string actual)
        {
            return string.Equals(this.Expected, actual, StringComparison.Ordinal);
        }
    }
}