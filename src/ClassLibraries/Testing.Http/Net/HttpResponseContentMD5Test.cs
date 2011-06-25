namespace Cavity.Net
{
    using System;
#if !NET20
    using System.Linq;
#endif

    public sealed class HttpResponseContentMD5Test : ITestHttpExpectation
    {
        bool ITestHttpExpectation.Check(Response response)
        {
            if (null == response)
            {
                throw new ArgumentNullException("response");
            }

            if (response.Headers.AllKeys.Contains("Content-MD5"))
            {
                return true;
            }

            throw new HttpTestException(response, "The Content-MD5 response header is missing.");
        }
    }
}