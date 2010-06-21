namespace Cavity.Net
{
    using System;
    using System.IO;
    using Cavity;
    using Xunit;

    public sealed class HttpRequestFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<HttpRequest>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .NoDefaultConstructor()
                .IsNotDecorated()
                .Implements<IHttpRequest>()
                .Result);
        }

        [Fact]
        public void ctor_RequestLineNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpRequest(null as RequestLine));
        }

        [Fact]
        public void ctor_RequestLine()
        {
            Assert.NotNull(new HttpRequest(new RequestLine("GET", "/", "HTTP/1.1")));
        }

        [Fact]
        public void prop_AbsoluteUri()
        {
            Assert.NotNull(new PropertyExpectations<HttpRequest>("AbsoluteUri")
                .TypeIs<Uri>()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_Headers()
        {
            Assert.NotNull(new PropertyExpectations<HttpRequest>("Headers")
                .IsAutoProperty<IHttpHeaderCollection>()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_Body()
        {
            Assert.NotNull(new PropertyExpectations<HttpRequest>("Body")
                .IsAutoProperty<IHttpBody>()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_AbsoluteUri_get()
        {
            Uri expected = new Uri("http://www.example.com/");

            var requestLine = new RequestLine("GET", expected.AbsoluteUri, "HTTP/1.1");

            Uri actual = new HttpRequest(requestLine).AbsoluteUri;

            Assert.Equal<Uri>(expected, actual);
        }

        [Fact]
        public void prop_RequestLine()
        {
            Assert.NotNull(new PropertyExpectations<HttpRequest>("RequestLine")
                .TypeIs<RequestLine>()
                .ArgumentNullException()
                .Set(new RequestLine("GET", "/", "HTTP/1.1"))
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void opImplicit_HttpRequest_stringNull()
        {
            HttpRequest obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void opImplicit_HttpRequest_stringEmpty()
        {
            HttpRequest expected;

            Assert.Throws<FormatException>(() => expected = string.Empty);
        }

        [Fact]
        public void opImplicit_HttpRequest_string()
        {
            HttpRequest expected = "GET / HTTP/1.1";
            HttpRequest actual = new HttpRequest("GET / HTTP/1.1");

            Assert.Equal<HttpRequest>(expected, actual);
        }

        [Fact]
        public void op_Parse_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpRequest.Parse(null as string));
        }

        [Fact]
        public void op_Parse_stringEmpty()
        {
            Assert.Throws<FormatException>(() => HttpRequest.Parse(string.Empty));
        }

        [Fact]
        public void op_Parse_string()
        {
            HttpRequest expected = new HttpRequest("GET / HTTP/1.1");
            HttpRequest actual = HttpRequest.Parse("GET / HTTP/1.1");

            Assert.Equal<HttpRequest>(expected, actual);
        }

        [Fact]
        public void op_Write_StreamWriterNull()
        {
            var obj = new HttpRequest("GET / HTTP/1.1");

            Assert.Throws<ArgumentNullException>(() => obj.Write(null as StreamWriter));
        }

        [Fact]
        public void op_ToString()
        {
            string expected = "GET / HTTP/1.1";
            string actual = new HttpRequest("GET / HTTP/1.1").ToString();

            Assert.Equal<string>(expected, actual);
        }
    }
}