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
                .DerivesFrom<ValueObject<HttpResponse>>()
                .IsConcreteClass()
                .IsSealed()
                .NoDefaultConstructor()
                .IsNotDecorated()
                .Implements<IHttpResponse>()
                .Result);
        }

        [Fact]
        public void ctor_StatusLineNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpResponse(null as StatusLine));
        }

        [Fact]
        public void ctor_StatusLine()
        {
            Assert.NotNull(new HttpResponse(new StatusLine("HTTP/1.1", 200, "OK")));
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

            Assert.Throws<FormatException>(() => expected = string.Empty);
        }

        [Fact]
        public void opImplicit_HttpResponse_string()
        {
            HttpResponse expected = "HTTP/1.1 200 OK";
            HttpResponse actual = new HttpResponse(new StatusLine("HTTP/1.1", 200, "OK"));

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
            Assert.Throws<FormatException>(() => HttpResponse.Parse(string.Empty));
        }

        [Fact]
        public void op_Parse_string200()
        {
            HttpResponse expected = new HttpResponse(new StatusLine("HTTP/1.1", 200, "OK"));
            HttpResponse actual = HttpResponse.Parse("HTTP/1.1 200 OK");

            Assert.Equal<HttpResponse>(expected, actual);
        }

        [Fact]
        public void op_Parse_string404()
        {
            HttpResponse expected = new HttpResponse(new StatusLine("HTTP/1.1", 404, "Not Found"));
            HttpResponse actual = HttpResponse.Parse("HTTP/1.1 404 Not Found");

            Assert.Equal<HttpResponse>(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            string expected = "HTTP/1.1 404 Not Found";
            string actual = new HttpResponse(new StatusLine("HTTP/1.1", 404, "Not Found")).ToString();

            Assert.Equal<string>(expected, actual);
        }
    }
}