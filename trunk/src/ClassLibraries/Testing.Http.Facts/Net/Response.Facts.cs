namespace Cavity.Net
{
    using System;
    using System.Net;
    using Xunit;

    public sealed class ResponseFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Response>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_HttpStatusCode_AbsoluteUri_WebHeaderCollection_CookieCollection()
        {
            Assert.NotNull(new Response(HttpStatusCode.OK, new AbsoluteUri("http://example.com/"), new WebHeaderCollection(), new CookieCollection()));
        }

        [Fact]
        public void ctor_HttpStatusCode_AbsoluteUriNull_WebHeaderCollection_CookieCollection()
        {
            Assert.Throws<ArgumentNullException>(() => new Response(HttpStatusCode.OK, null, new WebHeaderCollection(), new CookieCollection()));
        }

        [Fact]
        public void ctor_HttpStatusCode_AbsoluteUri_WebHeaderCollectionNull_CookieCollection()
        {
            Assert.Throws<ArgumentNullException>(() => new Response(HttpStatusCode.OK, new AbsoluteUri("http://example.com/"), null, new CookieCollection()));
        }

        [Fact]
        public void ctor_HttpStatusCode_AbsoluteUri_WebHeaderCollection_CookieCollectionNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Response(HttpStatusCode.OK, new AbsoluteUri("http://example.com/"), new WebHeaderCollection(), null));
        }

        [Fact]
        public void prop_Cookies()
        {
            Assert.True(new PropertyExpectations<Response>(p => p.Cookies)
                            .IsNotDecorated()
                            .TypeIs<CookieCollection>()
                            .Result);
        }

        [Fact]
        public void prop_Headers()
        {
            Assert.True(new PropertyExpectations<Response>(p => p.Headers)
                            .IsNotDecorated()
                            .TypeIs<WebHeaderCollection>()
                            .Result);
        }

        [Fact]
        public void prop_Location()
        {
            Assert.True(new PropertyExpectations<Response>(p => p.Location)
                            .IsNotDecorated()
                            .TypeIs<AbsoluteUri>()
                            .Result);
        }

        [Fact]
        public void prop_Status()
        {
            Assert.True(new PropertyExpectations<Response>(p => p.Status)
                            .IsNotDecorated()
                            .TypeIs<HttpStatusCode>()
                            .Result);
        }

        [Fact]
        public void op_ToString()
        {
            var obj = new Response(HttpStatusCode.OK, new AbsoluteUri("http://example.com/"), new WebHeaderCollection(), new CookieCollection());

            var expected =
                "http://example.com/" + Environment.NewLine +
                Environment.NewLine +
                "HTTP/1.1 200 OK" + Environment.NewLine;
            var actual = obj.ToString();

            Assert.Equal(expected, actual);
        }
    }
}