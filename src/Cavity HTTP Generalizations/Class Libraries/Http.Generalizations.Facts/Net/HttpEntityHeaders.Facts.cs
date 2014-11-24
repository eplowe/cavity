namespace Cavity.Net
{
    using Xunit;

    public sealed class HttpEntityHeadersFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(HttpEntityHeaders).IsStatic());
        }

        [Fact]
        public void prop_Allow_get()
        {
            const string expected = "Allow";
            var actual = HttpEntityHeaders.Allow;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_ContentEncoding_get()
        {
            const string expected = "Content-Encoding";
            var actual = HttpEntityHeaders.ContentEncoding;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_ContentLanguage_get()
        {
            const string expected = "Content-Language";
            var actual = HttpEntityHeaders.ContentLanguage;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_ContentLength_get()
        {
            const string expected = "Content-Length";
            var actual = HttpEntityHeaders.ContentLength;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_ContentLocation_get()
        {
            const string expected = "Content-Location";
            var actual = HttpEntityHeaders.ContentLocation;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_ContentMD5_get()
        {
            const string expected = "Content-MD5";
            var actual = HttpEntityHeaders.ContentMD5;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_ContentRange_get()
        {
            const string expected = "Content-Range";
            var actual = HttpEntityHeaders.ContentRange;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_ContentType_get()
        {
            const string expected = "Content-Type";
            var actual = HttpEntityHeaders.ContentType;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Expires_get()
        {
            const string expected = "Expires";
            var actual = HttpEntityHeaders.Expires;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_LastModified_get()
        {
            const string expected = "Last-Modified";
            var actual = HttpEntityHeaders.LastModified;

            Assert.Equal(expected, actual);
        }
    }
}