namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class RequestLineFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RequestLine>()
                            .DerivesFrom<ComparableObject>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_HttpVersion_string_stringAbsolute_string()
        {
            Assert.NotNull(new RequestLine("GET", "http://www.example.com/", "HTTP/1.1"));
        }

        [Fact]
        public void ctor_HttpVersion_string_stringRelative_string()
        {
            Assert.NotNull(new RequestLine("GET", "/", "HTTP/1.1"));
        }

        [Fact]
        public void opImplicit_RequestLine_string()
        {
            var expected = new RequestLine("GET", "/", "HTTP/1.1");
            RequestLine actual = "GET / HTTP/1.1";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_RequestLine_stringEmpty()
        {
            Assert.Throws<FormatException>(() => (RequestLine)string.Empty);
        }

        [Fact]
        public void opImplicit_RequestLine_stringNull()
        {
            Assert.Null((RequestLine)null);
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            Assert.Throws<FormatException>(() => RequestLine.FromString(string.Empty));
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => RequestLine.FromString(null));
        }

        [Fact]
        public void op_FromString_string_whenCR()
        {
            Assert.Throws<FormatException>(() => RequestLine.FromString("999 Foo \r Bar"));
        }

        [Fact]
        public void op_FromString_string_whenGetRelative()
        {
            var expected = new RequestLine("GET", "/", "HTTP/1.1");
            var actual = RequestLine.FromString("GET / HTTP/1.1");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_whenLR()
        {
            Assert.Throws<FormatException>(() => RequestLine.FromString("999 Foo \n Bar"));
        }

        [Fact]
        public void op_FromString_string_whenMissingHttpMethod()
        {
            Assert.Throws<FormatException>(() => RequestLine.FromString("/ HTTP/1.1"));
        }

        [Fact]
        public void op_FromString_string_whenMissingHttpVersion()
        {
            Assert.Throws<FormatException>(() => RequestLine.FromString("GET /"));
        }

        [Fact]
        public void op_FromString_string_whenMissingRequestUri()
        {
            Assert.Throws<FormatException>(() => RequestLine.FromString("GET HTTP/1.1"));
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "HEAD / HTTP/1.1";
            var actual = new RequestLine("HEAD", "/", "HTTP/1.1").ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Method()
        {
            Assert.True(new PropertyExpectations<RequestLine>(p => p.Method)
                            .TypeIs<string>()
                            .ArgumentNullException()
                            .ArgumentOutOfRangeException(string.Empty)
                            .Set("OPTIONS")
                            .Set("GET")
                            .Set("HEAD")
                            .Set("POST")
                            .Set("PUT")
                            .Set("DELETE")
                            .Set("TRACE")
                            .Set("CONNECT")
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_RequestUri()
        {
            Assert.True(new PropertyExpectations<RequestLine>(p => p.RequestUri)
                            .TypeIs<string>()
                            .ArgumentNullException()
                            .ArgumentOutOfRangeException(string.Empty)
                            .Set("*")
                            .Set("http://www.example.com/")
                            .Set("/")
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Version()
        {
            Assert.True(new PropertyExpectations<RequestLine>(p => p.Version)
                            .TypeIs<HttpVersion>()
                            .ArgumentNullException()
                            .Set(new HttpVersion(1, 0))
                            .Set(new HttpVersion(1, 1))
                            .IsNotDecorated()
                            .Result);
        }
    }
}