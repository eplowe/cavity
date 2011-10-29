namespace Cavity.Net
{
    using System;

    public sealed class HttpHeadTest : ITestHttpExpectation
    {
        public HttpHeadTest(IWebRequest request)
        {
            if (null == request)
            {
                throw new ArgumentNullException("request");
            }

            if (!"GET".Equals(request.Method, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentOutOfRangeException("request");
            }

            Request = request;
        }

        private IWebRequest Request { get; set; }

        bool ITestHttpExpectation.Check(Response response)
        {
            if (null == response)
            {
                throw new ArgumentNullException("response");
            }

            return false;
        }
    }
}