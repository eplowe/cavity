namespace Cavity.Net
{
    using System;
    using System.Net;
    using Cavity;
    using Xunit;

    public sealed class HttpResponseHeaderTestFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpResponseHeaderTest>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .NoDefaultConstructor()
                .IsNotDecorated()
                .Implements<ITestHttpExpectation>()
                .Result);
        }

        [Fact]
        public void ctor_HttpResponseHeader()
        {
            Assert.NotNull(new HttpResponseHeaderTest(HttpResponseHeader.Location));
        }

        [Fact]
        public void prop_Expected()
        {
            Assert.True(new PropertyExpectations<HttpResponseHeaderTest>(p => p.Expected)
                            .IsNotDecorated()
                            .IsAutoProperty<HttpResponseHeader>()
                            .Result);
        }

        [Fact]
        public void ITestHttpExpectation_op_Check_ResponseNull()
        {
            ITestHttpExpectation obj = new HttpResponseHeaderTest(HttpResponseHeader.Location);

            Assert.Throws<ArgumentNullException>(() => obj.Check(null));
        }

        [Fact]
        public void ITestHttpExpectation_op_Check_Response()
        {
            ITestHttpExpectation obj = new HttpResponseHeaderTest(HttpResponseHeader.Location);

            var headers = new WebHeaderCollection
            {
                {
                    HttpResponseHeader.Location, "http://example.com"
                    }
            };

            var response = new Response(HttpStatusCode.OK, new AbsoluteUri("http://example.com"), headers, new CookieCollection());

            Assert.True(obj.Check(response));
        }

        [Fact]
        public void ITestHttpExpectation_op_Check_Response_fail()
        {
            ITestHttpExpectation obj = new HttpResponseHeaderTest(HttpResponseHeader.Location);

            var response = new Response(HttpStatusCode.OK, new AbsoluteUri("http://example.com"), new WebHeaderCollection(), new CookieCollection());

            Assert.Throws<HttpTestException>(() => obj.Check(response));
        }
    }
}