namespace Cavity.Fluent
{
    using System;

    public class ITestExpectationDummy : ITestExpectation
    {
        public bool Check()
        {
            throw new NotSupportedException();
        }
    }
}