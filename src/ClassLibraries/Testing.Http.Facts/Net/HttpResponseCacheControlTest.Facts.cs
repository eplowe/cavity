namespace Cavity.Net
{
    using System;
    using System.Net;
    using Xunit;

    public sealed class HttpResponseCacheControlTestFacts
    {
        [Fact]
        public void ITestHttpExpectation_op_Check_Response()
        {
            ITestHttpExpectation obj = new HttpResponseCacheControlTest("public");

            var headers = new WebHeaderCollection
            {
                {
                    HttpResponseHeader.CacheControl, "public"
                    }
            };
            var response = new Response(HttpStatusCode.OK, new AbsoluteUri("http://example.com"), headers, new CookieCollection());

            Assert.True(obj.Check(response));
        }

        [Fact]
        public void ITestHttpExpectation_op_Check_ResponseNull()
        {
            ITestHttpExpectation obj = new HttpResponseCacheControlTest("public");

            Assert.Throws<ArgumentNullException>(() => obj.Check(null));
        }

        [Fact]
        public void ITestHttpExpectation_op_Check_Response_fail()
        {
            ITestHttpExpectation obj = new HttpResponseCacheControlTest("private");

            var headers = new WebHeaderCollection
            {
                {
                    HttpResponseHeader.CacheControl, "public"
                    }
            };
            var response = new Response(HttpStatusCode.OK, new AbsoluteUri("http://example.com"), headers, new CookieCollection());

            Assert.Throws<HttpTestException>(() => obj.Check(response));
        }

        [Fact]
        public void ITestHttpExpectation_op_Check_Response_missing()
        {
            ITestHttpExpectation obj = new HttpResponseCacheControlTest("private");

            var response = new Response(HttpStatusCode.OK, new AbsoluteUri("http://example.com"), new WebHeaderCollection(), new CookieCollection());

            Assert.Throws<HttpTestException>(() => obj.Check(response));
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpResponseCacheControlTest>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<ITestHttpExpectation>()
                            .Result);
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new HttpResponseCacheControlTest("public"));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.NotNull(new HttpResponseCacheControlTest(string.Empty));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.NotNull(new HttpResponseCacheControlTest(null));
        }

        [Fact]
        public void prop_Expected()
        {
            Assert.True(new PropertyExpectations<HttpResponseCacheControlTest>(p => p.Expected)
                            .IsNotDecorated()
                            .IsAutoProperty<string>()
                            .Result);
        }
    }
}