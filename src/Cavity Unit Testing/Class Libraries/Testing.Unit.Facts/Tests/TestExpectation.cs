namespace Cavity.Tests
{
    using System;

    using Cavity.Fluent;

    public sealed class TestExpectation : ITestExpectation
    {
        public bool Check()
        {
            throw new NotImplementedException();
        }
    }
}