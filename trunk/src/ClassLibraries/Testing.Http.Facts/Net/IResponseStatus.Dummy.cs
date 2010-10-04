namespace Cavity.Net
{
    using System;
    using System.Net;

    public sealed class IResponseStatusDummy : IResponseStatus
    {
        IResponseCacheControl IResponseStatus.Is(HttpStatusCode status)
        {
            throw new NotSupportedException();
        }

        ITestHttp IResponseStatus.IsSeeOther(AbsoluteUri location)
        {
            throw new NotSupportedException();
        }
    }
}