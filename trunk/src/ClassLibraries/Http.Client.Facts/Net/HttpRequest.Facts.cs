namespace Cavity.Net
{
    using System;
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
        public void op_ToResponse_IHttpClient()
        {
            var obj = new HttpRequest("GET / HTTP/1.1");

            Assert.Throws<NotSupportedException>(() => obj.ToResponse(new HttpClient()));
        }

        [Fact]
        public void op_ToResponse_IHttpClientNull()
        {
            var obj = new HttpRequest("GET / HTTP/1.1");

            Assert.Throws<ArgumentNullException>(() => obj.ToResponse(null as IHttpClient));
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