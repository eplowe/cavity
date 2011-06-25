namespace Cavity.Net
{
    using System;
#if !NET20
    using System.Linq;
#endif
    using System.Net;

    public sealed class HttpResponseContentLanguageTest : ITestHttpExpectation
    {
        public HttpResponseContentLanguageTest(string expected)
            : this()
        {
            Expected = expected;
        }

        private HttpResponseContentLanguageTest()
        {
        }

        public string Expected { get; set; }

        bool ITestHttpExpectation.Check(Response response)
        {
            if (null == response)
            {
                throw new ArgumentNullException("response");
            }

            if (!response.Headers.AllKeys.Contains("Content-Language"))
            {
                throw new HttpTestException(response, "The Content-Language response header is missing.");
            }

            var language = response.Headers[HttpResponseHeader.ContentLanguage];
            if (language.Equals(Expected, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            throw new HttpTestException(
                response,
                "[HTTP Content-Language]\r\n expected: {0}\r\n actual: {1}".FormatWith(Expected, language));
        }
    }
}