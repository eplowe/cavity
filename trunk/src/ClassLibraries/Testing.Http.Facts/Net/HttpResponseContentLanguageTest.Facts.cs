namespace Cavity.Net
{
    using System;
    using System.Net;
    using Cavity;
    using Xunit;

    public sealed class HttpResponseContentLanguageTestFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpResponseContentLanguageTest>()
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
            Assert.NotNull(new HttpResponseContentLanguageTest("en"));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.NotNull(new HttpResponseContentLanguageTest(string.Empty));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.NotNull(new HttpResponseContentLanguageTest(null));
        }

        [Fact]
        public void ITestHttpExpectation_op_Check_ResponseNull()
        {
            ITestHttpExpectation obj = new HttpResponseContentLanguageTest("en");

            Assert.Throws<ArgumentNullException>(() => obj.Check(null));
        }

        [Fact]
        public void ITestHttpExpectation_op_Check_Response()
        {
            ITestHttpExpectation obj = new HttpResponseContentLanguageTest("en");

            var headers = new WebHeaderCollection
            {
                {
                    HttpResponseHeader.ContentLanguage, "en"
                    }
            };

            var response = new Response(HttpStatusCode.OK, new AbsoluteUri("http://example.com"), headers, new CookieCollection());

            Assert.True(obj.Check(response));
        }

        [Fact]
        public void ITestHttpExpectation_op_Check_Response_fail()
        {
            ITestHttpExpectation obj = new HttpResponseContentLanguageTest("en");

            var response = new Response(HttpStatusCode.OK, new AbsoluteUri("http://example.com"), new WebHeaderCollection(), new CookieCollection());

            Assert.Throws<HttpTestException>(() => obj.Check(response));
        }
    }
}