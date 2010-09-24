namespace Cavity.Net
{
    using System;

    public sealed class ITestHttpExpectationDummy : ITestHttpExpectation
    {
        bool ITestHttpExpectation.Check(Response response)
        {
            throw new NotSupportedException();
        }
    }
}