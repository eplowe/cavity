namespace Cavity.Net
{
    using Xunit;

    public sealed class HttpRequestHeadersFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(HttpRequestHeaders).IsStatic());
        }

        [Fact]
        public void prop_AcceptCharset_get()
        {
            const string expected = "Accept-Charset";
            var actual = HttpRequestHeaders.AcceptCharset;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_AcceptEncoding_get()
        {
            const string expected = "Accept-Encoding";
            var actual = HttpRequestHeaders.AcceptEncoding;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_AcceptLanguage_get()
        {
            const string expected = "Accept-Language";
            var actual = HttpRequestHeaders.AcceptLanguage;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Accept_get()
        {
            const string expected = "Accept";
            var actual = HttpRequestHeaders.Accept;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Authorization_get()
        {
            const string expected = "Authorization";
            var actual = HttpRequestHeaders.Authorization;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Expect_get()
        {
            const string expected = "Expect";
            var actual = HttpRequestHeaders.Expect;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_From_get()
        {
            const string expected = "From";
            var actual = HttpRequestHeaders.From;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Host_get()
        {
            const string expected = "Host";
            var actual = HttpRequestHeaders.Host;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_IfMatch_get()
        {
            const string expected = "If-Match";
            var actual = HttpRequestHeaders.IfMatch;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_IfModifiedSince_get()
        {
            const string expected = "If-Modified-Since";
            var actual = HttpRequestHeaders.IfModifiedSince;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_IfNoneMatch_get()
        {
            const string expected = "If-None-Match";
            var actual = HttpRequestHeaders.IfNoneMatch;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_IfRange_get()
        {
            const string expected = "If-Range";
            var actual = HttpRequestHeaders.IfRange;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_IfUnmodifiedSince_get()
        {
            const string expected = "If-Unmodified-Since";
            var actual = HttpRequestHeaders.IfUnmodifiedSince;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_MaxForwards_get()
        {
            const string expected = "Max-Forwards";
            var actual = HttpRequestHeaders.MaxForwards;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_ProxyAuthorization_get()
        {
            const string expected = "Proxy-Authorization";
            var actual = HttpRequestHeaders.ProxyAuthorization;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Range_get()
        {
            const string expected = "Range";
            var actual = HttpRequestHeaders.Range;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Referer_get()
        {
            const string expected = "Referer";
            var actual = HttpRequestHeaders.Referer;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_TE_get()
        {
            const string expected = "TE";
            var actual = HttpRequestHeaders.TE;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_UserAgent_get()
        {
            const string expected = "User-Agent";
            var actual = HttpRequestHeaders.UserAgent;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Vary_get()
        {
            const string expected = "Vary";
            var actual = HttpRequestHeaders.Vary;

            Assert.Equal(expected, actual);
        }
    }
}