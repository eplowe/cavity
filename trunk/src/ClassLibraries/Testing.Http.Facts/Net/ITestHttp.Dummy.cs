namespace Cavity.Net
{
    using System;

    public sealed class ITestHttpDummy : ITestHttp
    {
        bool ITestHttp.Result
        {
            get
            {
                throw new NotSupportedException();
            }
        }
    }
}