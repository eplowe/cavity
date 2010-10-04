namespace Cavity.Net
{
    using System;
    using System.Net;
    using Cavity;
    using Xunit;

    public sealed class HttpResponseContentMD5TestFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpResponseContentMD5Test>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Implements<ITestHttpExpectation>()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new HttpResponseContentMD5Test());
        }

        [Fact]
        public void ITestHttpExpectation_op_Check_ResponseNull()
        {
            ITestHttpExpectation obj = new HttpResponseContentMD5Test();

            Assert.Throws<ArgumentNullException>(() => obj.Check(null));
        }

        [Fact]
        public void ITestHttpExpectation_op_Check_Response()
        {
            ITestHttpExpectation obj = new HttpResponseContentMD5Test();

            var headers = new WebHeaderCollection
            {
                {
                    HttpResponseHeader.ContentMd5, "HUXZLQLMuI/KZ5KDcJPcOA=="
                    }
            };

            var response = new Response(HttpStatusCode.OK, new AbsoluteUri("http://example.com"), headers, new CookieCollection());

            Assert.True(obj.Check(response));
        }

        [Fact]
        public void ITestHttpExpectation_op_Check_Response_fail()
        {
            ITestHttpExpectation obj = new HttpResponseContentMD5Test();

            var response = new Response(HttpStatusCode.OK, new AbsoluteUri("http://example.com"), new WebHeaderCollection(), new CookieCollection());

            Assert.Throws<HttpTestException>(() => obj.Check(response));
        }
    }
}