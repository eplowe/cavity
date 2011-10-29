namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class HttpHeadTestFacts
    {
        [Fact]
        public void ITestHttpExpectation_op_Check_Response()
        {
            var request = new HttpRequestDefinition("http://example.com/")
            {
                Method = "GET"
            };

            ITestHttpExpectation obj = new HttpHeadTest(request);

            Assert.Throws<ArgumentNullException>(() => obj.Check(null));
        }

        [Fact]
        public void ITestHttpExpectation_op_Check_ResponseNull()
        {
            var request = new HttpRequestDefinition("http://example.com/")
            {
                Method = "GET"
            };

            ITestHttpExpectation obj = new HttpHeadTest(request);

            Assert.Throws<ArgumentNullException>(() => obj.Check(null));
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpHeadTest>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<ITestHttpExpectation>()
                            .Result);
        }

        [Fact]
        public void ctor_HttpRequestDefinition()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new HttpHeadTest(new HttpRequestDefinition("http://example.com/")));
        }

        [Fact]
        public void ctor_HttpRequestDefinitionNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpHeadTest(null));
        }

        [Fact]
        public void ctor_HttpRequestDefinition_whenGetMethod()
        {
            var request = new HttpRequestDefinition("http://example.com/")
            {
                Method = "GET"
            };

            Assert.NotNull(new HttpHeadTest(request));
        }
    }
}