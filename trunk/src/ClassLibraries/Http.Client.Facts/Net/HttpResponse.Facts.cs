namespace Cavity.Net
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class HttpResponseFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<HttpResponse>()
                .DerivesFrom<HttpMessage>()
                .IsConcreteClass()
                .IsSealed()
                .NoDefaultConstructor()
                .IsNotDecorated()
                .Implements<IHttpResponse>()
                .Result);
        }

        [Fact]
        public void ctor_StatusLineNull_IHttpHeaderCollection()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpResponse(null as StatusLine, new IHttpHeaderCollectionDummy()));
        }

        [Fact]
        public void ctor_StatusLine_IHttpHeaderCollectionNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpResponse(new StatusLine("HTTP/1.1", 200, "OK"), null as IHttpHeaderCollection));
        }

        [Fact]
        public void ctor_StatusLine_IHttpHeaderCollection()
        {
            Assert.NotNull(new HttpResponse(new StatusLine("HTTP/1.1", 200, "OK"), new IHttpHeaderCollectionDummy()));
        }

        [Fact]
        public void prop_StatusLine()
        {
            Assert.NotNull(new PropertyExpectations<HttpResponse>("StatusLine")
                .TypeIs<StatusLine>()
                .ArgumentNullException()
                .Set(new StatusLine("HTTP/1.1", 200, "OK"))
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void opImplicit_HttpResponse_stringNull()
        {
            HttpResponse obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void opImplicit_HttpResponse_stringEmpty()
        {
            HttpResponse expected;

            Assert.Throws<ArgumentOutOfRangeException>(() => expected = string.Empty);
        }

        [Fact]
        public void opImplicit_HttpResponse_string()
        {
            HttpResponse expected = "HTTP/1.1 200 OK";
            HttpResponse actual = new HttpResponse(new StatusLine("HTTP/1.1", 200, "OK"), new HttpHeaderCollection());

            Assert.Equal<HttpResponse>(expected, actual);
        }

        [Fact]
        public void op_Parse_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpResponse.Parse(null as string));
        }

        [Fact]
        public void op_Parse_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => HttpResponse.Parse(string.Empty));
        }

        [Fact]
        public void op_Parse_string200()
        {
            HttpResponse expected = new HttpResponse(new StatusLine("HTTP/1.1", 200, "OK"), new HttpHeaderCollection());
            HttpResponse actual = HttpResponse.Parse("HTTP/1.1 200 OK");

            Assert.Equal<HttpResponse>(expected, actual);
        }

        [Fact]
        public void op_Parse_string404()
        {
            HttpResponse expected = new HttpResponse(new StatusLine("HTTP/1.1", 404, "Not Found"), new HttpHeaderCollection());
            HttpResponse actual = HttpResponse.Parse("HTTP/1.1 404 Not Found");

            Assert.Equal<HttpResponse>(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            string expected = "HTTP/1.1 404 Not Found";
            string actual = new HttpResponse(new StatusLine("HTTP/1.1", 404, "Not Found"), new HttpHeaderCollection()).ToString();

            Assert.Equal<string>(expected, actual);
        }
    }
}