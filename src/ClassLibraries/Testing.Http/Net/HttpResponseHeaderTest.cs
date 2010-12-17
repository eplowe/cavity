namespace Cavity.Net
{
    using System;
    using System.Net;

    public sealed class HttpResponseHeaderTest : ITestHttpExpectation
    {
        public HttpResponseHeaderTest(HttpResponseHeader expected)
            : this()
        {
            Expected = expected;
        }

        private HttpResponseHeaderTest()
        {
        }

        public HttpResponseHeader Expected { get; set; }

        bool ITestHttpExpectation.Check(Response response)
        {
            if (null == response)
            {
                throw new ArgumentNullException("response");
            }

            if (string.IsNullOrEmpty(response.Headers[Expected]))
            {
                throw new HttpTestException(
                    response,
                    "The {0} response header is missing.".FormatWith(Expected.ToString("G")));
            }

            return true;
        }
    }
}