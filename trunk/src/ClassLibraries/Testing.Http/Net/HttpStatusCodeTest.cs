namespace Cavity.Net
{
    using System;
    using System.Net;

    public sealed class HttpStatusCodeTest : ITestHttpExpectation
    {
        public HttpStatusCodeTest(HttpStatusCode expected)
            : this()
        {
            Expected = expected;
        }

        private HttpStatusCodeTest()
        {
        }

        public HttpStatusCode Expected { get; set; }

        bool ITestHttpExpectation.Check(Response response)
        {
            if (null == response)
            {
                throw new ArgumentNullException("response");
            }

            if (Expected != response.Status)
            {
                throw new HttpTestException(
                    response,
                    "[HTTP Status Code]\r\n expected: {0}\r\n actual: {1}".FormatWith(Expected, response.Status));
            }

            return true;
        }
    }
}