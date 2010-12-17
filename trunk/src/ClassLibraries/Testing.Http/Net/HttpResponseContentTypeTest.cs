namespace Cavity.Net
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Mime;

    public sealed class HttpResponseContentTypeTest : ITestHttpExpectation
    {
        public HttpResponseContentTypeTest(ContentType expected)
            : this()
        {
            Expected = expected;
        }

        private HttpResponseContentTypeTest()
        {
        }

        public ContentType Expected { get; set; }

        bool ITestHttpExpectation.Check(Response response)
        {
            if (null == response)
            {
                throw new ArgumentNullException("response");
            }

            if (null == Expected)
            {
                if (response.Headers.AllKeys.Contains("Content-Type"))
                {
                    throw new HttpTestException(response, "The Content-Type response header was unexpectedly sent.");
                }

                return true;
            }

            var expected = Expected.MediaType;
            var actual = response.Headers[HttpResponseHeader.ContentType];
            if (null == actual ||
                !actual.StartsWith(expected, StringComparison.Ordinal))
            {
                throw new HttpTestException(
                    response,
                    "[HTTP Content-Type]\r\n expected: {0}\r\n actual: {1}".FormatWith(expected, actual));
            }

            return true;
        }
    }
}