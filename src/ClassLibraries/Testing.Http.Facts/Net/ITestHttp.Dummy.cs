namespace Cavity.Net
{
    using System;

    public class ITestHttpDummy : ITestHttp
    {
        string ITestHttp.Location
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        bool ITestHttp.Result
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        ITestHttp ITestHttp.HasContentLocation(AbsoluteUri location)
        {
            throw new NotSupportedException();
        }
    }
}