namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class HttpRequestLineFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpRequestLine>()
                            .DerivesFrom<ComparableObject>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_TokenNull_AbsoluteUri()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpRequestLine(null, "http://example.com/"));
        }

        [Fact]
        public void ctor_TokenNull_AbsoluteUri_HttpVersion()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpRequestLine(null, "http://example.com/", "HTTP/1.1"));
        }

        [Fact]
        public void ctor_Token_AbsoluteUri()
        {
            Assert.NotNull(new HttpRequestLine("GET", "http://example.com/"));
        }

        [Fact]
        public void ctor_Token_AbsoluteUriNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpRequestLine("GET", null));
        }

        [Fact]
        public void ctor_Token_AbsoluteUriNull_HttpVersion()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpRequestLine("GET", null, "HTTP/1.1"));
        }

        [Fact]
        public void ctor_Token_AbsoluteUri_HttpVersion()
        {
            Assert.NotNull(new HttpRequestLine("GET", "http://example.com/", "HTTP/1.1"));
        }

        [Fact]
        public void ctor_Token_AbsoluteUri_HttpVersionNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpRequestLine("GET", "http://example.com/", null));
        }

        [Fact]
        public void opImplicit_string_RequestLine()
        {
            const string expected = "GET http://example.com/ HTTP/1.1";
            string actual = new HttpRequestLine("GET", "http://example.com/");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string()
        {
            var expected = new HttpRequestLine("GET", "http://example.com/", "HTTP/1.1");
            var actual = HttpRequestLine.FromString("GET http://example.com/ HTTP/1.1");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => HttpRequestLine.FromString(string.Empty));
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpRequestLine.FromString(null));
        }

        [Fact]
        public void op_FromString_string_whenMethodMissing()
        {
            Assert.Throws<FormatException>(() => HttpRequestLine.FromString("http://example.com/ HTTP/1.1"));
        }

        [Fact]
        public void op_FromString_string_whenUriMissing()
        {
            Assert.Throws<UriFormatException>(() => HttpRequestLine.FromString("GET HTTP/1.1"));
        }

        [Fact]
        public void op_FromString_string_whenVersionMissing()
        {
            var expected = new HttpRequestLine("GET", "http://example.com/", "HTTP/1.1");
            var actual = HttpRequestLine.FromString("GET http://example.com/");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "GET http://example.com/ HTTP/1.0";
            var actual = new HttpRequestLine("GET", "http://example.com/", "HTTP/1.0").ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Method()
        {
            Assert.True(new PropertyExpectations<HttpRequestLine>(x => x.Method)
                            .TypeIs<Token>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_RequestUri()
        {
            Assert.True(new PropertyExpectations<HttpRequestLine>(x => x.RequestUri)
                            .TypeIs<AbsoluteUri>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Version()
        {
            Assert.True(new PropertyExpectations<HttpRequestLine>(x => x.Version)
                            .TypeIs<HttpVersion>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}