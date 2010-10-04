namespace Cavity.Net
{
    using System;
    using System.Linq;
    using System.Net;

    public sealed class HttpResponseCacheControlTest : ITestHttpExpectation
    {
        public HttpResponseCacheControlTest(string expected)
            : this()
        {
            Expected = expected;
        }

        private HttpResponseCacheControlTest()
        {
        }

        public string Expected { get; set; }

        bool ITestHttpExpectation.Check(Response response)
        {
            if (null == response)
            {
                throw new ArgumentNullException("response");
            }

            if (!response.Headers.AllKeys.Contains("Cache-Control"))
            {
                throw new HttpTestException(response, "The Cache-Control response header is missing.");
            }

            var cache = response.Headers[HttpResponseHeader.CacheControl];
            if (cache.StartsWith(Expected, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            throw new HttpTestException(
                response,
                "[HTTP Cache-Control]\r\n expected: {0}\r\n actual: {1}".FormatWith(Expected, cache));
        }
    }
}