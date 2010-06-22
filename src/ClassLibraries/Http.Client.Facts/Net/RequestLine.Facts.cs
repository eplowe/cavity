namespace Cavity.Net
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Cavity;
    using Xunit;

    public sealed class RequestLineFacts
    {
        [Fact]
        public void type_definition()
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
        public void prop_Method()
        {
            Assert.True(new PropertyExpectations<RequestLine>("Method")
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
            Assert.True(new PropertyExpectations<RequestLine>("RequestUri")
                .TypeIs<string>()
                .ArgumentNullException()
                .ArgumentOutOfRangeException(string.Empty)
                .Set("*")
                .Set("http://www.example.com/")
                .Set("/")
                .IsDecoratedWith<SuppressMessageAttribute>()
                .Result);
        }

        [Fact]
        public void prop_Version()
        {
            Assert.True(new PropertyExpectations<RequestLine>("Version")
                .TypeIs<HttpVersion>()
                .ArgumentNullException()
                .Set(new HttpVersion(1, 0))
                .Set(new HttpVersion(1, 1))
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void opImplicit_RequestLine_stringNull()
        {
            RequestLine obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void opImplicit_RequestLine_stringEmpty()
        {
            RequestLine expected;

            Assert.Throws<FormatException>(() => expected = string.Empty);
        }

        [Fact]
        public void opImplicit_RequestLine_string()
        {
            RequestLine expected = "GET / HTTP/1.1";
            RequestLine actual = new RequestLine("GET", "/", "HTTP/1.1");

            Assert.Equal<RequestLine>(expected, actual);
        }

        [Fact]
        public void op_Parse_string_whenGetRelative()
        {
            RequestLine expected = new RequestLine("GET", "/", "HTTP/1.1");
            RequestLine actual = RequestLine.Parse("GET / HTTP/1.1");

            Assert.Equal<RequestLine>(expected, actual);
        }

        [Fact]
        public void op_Parse_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => RequestLine.Parse(null as string));
        }

        [Fact]
        public void op_Parse_stringEmpty()
        {
            Assert.Throws<FormatException>(() => RequestLine.Parse(string.Empty));
        }

        [Fact]
        public void op_Parse_string_whenMissingHttpVersion()
        {
            Assert.Throws<FormatException>(() => RequestLine.Parse("GET /"));
        }

        [Fact]
        public void op_Parse_string_whenMissingHttpMethod()
        {
            Assert.Throws<FormatException>(() => RequestLine.Parse("/ HTTP/1.1"));
        }

        [Fact]
        public void op_Parse_string_whenMissingRequestUri()
        {
            Assert.Throws<FormatException>(() => RequestLine.Parse("GET HTTP/1.1"));
        }

        [Fact]
        public void op_Parse_string_whenCR()
        {
            Assert.Throws<FormatException>(() => RequestLine.Parse("999 Foo \r Bar"));
        }

        [Fact]
        public void op_Parse_string_whenLR()
        {
            Assert.Throws<FormatException>(() => RequestLine.Parse("999 Foo \n Bar"));
        }

        [Fact]
        public void op_ToString()
        {
            string expected = "HEAD / HTTP/1.1";
            string actual = new RequestLine("HEAD", "/", "HTTP/1.1").ToString();

            Assert.Equal<string>(expected, actual);
        }
    }
}