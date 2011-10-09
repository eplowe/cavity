namespace Cavity.Net
{
    using System;
    using System.Net;
    using Xunit;

    public sealed class HttpStatusCodeTestFacts
    {
        [Fact]
        public void ITestHttpExpectation_op_Check_Response()
        {
            ITestHttpExpectation obj = new HttpStatusCodeTest(HttpStatusCode.OK);

            var response = new Response(HttpStatusCode.OK, new AbsoluteUri("http://example.com"), new WebHeaderCollection(), new CookieCollection());

            Assert.True(obj.Check(response));
        }

        [Fact]
        public void ITestHttpExpectation_op_Check_ResponseNull()
        {
            ITestHttpExpectation obj = new HttpStatusCodeTest(HttpStatusCode.OK);

            Assert.Throws<ArgumentNullException>(() => obj.Check(null));
        }

        [Fact]
        public void ITestHttpExpectation_op_Check_Response_fail()
        {
            ITestHttpExpectation obj = new HttpStatusCodeTest(HttpStatusCode.NotFound);

            var response = new Response(HttpStatusCode.OK, new AbsoluteUri("http://example.com"), new WebHeaderCollection(), new CookieCollection());

            Assert.Throws<HttpTestException>(() => obj.Check(response));
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpStatusCodeTest>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<ITestHttpExpectation>()
                            .Result);
        }

        [Fact]
        public void ctor_HttpStatusCode()
        {
            Assert.NotNull(new HttpStatusCodeTest(HttpStatusCode.OK));
        }

        [Fact]
        public void prop_Expected()
        {
            Assert.True(new PropertyExpectations<HttpStatusCodeTest>(p => p.Expected)
                            .IsNotDecorated()
                            .IsAutoProperty<HttpStatusCode>()
                            .Result);
        }
    }
}