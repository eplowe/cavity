namespace Cavity.Net
{
    using System;
    using System.Net;
    using System.Net.Mime;
    using Xunit;

    public sealed class HttpResponseContentTypeTestFacts
    {
        [Fact]
        public void ITestHttpExpectation_op_Check_Response()
        {
            ITestHttpExpectation obj = new HttpResponseContentTypeTest(new ContentType("text/plain"));

            var headers = new WebHeaderCollection
            {
                {
                    HttpResponseHeader.ContentType, "text/plain"
                    }
            };

            var response = new Response(HttpStatusCode.OK, new AbsoluteUri("http://example.com"), headers, new CookieCollection());

            Assert.True(obj.Check(response));
        }

        [Fact]
        public void ITestHttpExpectation_op_Check_ResponseNull()
        {
            ITestHttpExpectation obj = new HttpResponseContentTypeTest(new ContentType("text/plain"));

            Assert.Throws<ArgumentNullException>(() => obj.Check(null));
        }

        [Fact]
        public void ITestHttpExpectation_op_Check_Response_fail()
        {
            ITestHttpExpectation obj = new HttpResponseContentTypeTest(new ContentType("text/plain"));

            var response = new Response(HttpStatusCode.OK, new AbsoluteUri("http://example.com"), new WebHeaderCollection(), new CookieCollection());

            Assert.Throws<HttpTestException>(() => obj.Check(response));
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpResponseContentTypeTest>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<ITestHttpExpectation>()
                            .Result);
        }

        [Fact]
        public void ctor_ContentType()
        {
            Assert.NotNull(new HttpResponseContentTypeTest(new ContentType("text/plain")));
        }

        [Fact]
        public void ctor_ContentTypeNull()
        {
            Assert.NotNull(new HttpResponseContentTypeTest(null));
        }
    }
}