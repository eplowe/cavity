namespace Cavity.Net
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class HttpHeadTestFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpHeadTest>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
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
            var definition = new HttpRequestDefinition("http://example.com/")
            {
                Method = "GET"
            };

            Assert.NotNull(new HttpHeadTest(definition));
        }

        [Fact]
        public void ITestHttpExpectation_op_Check_ResponseNull()
        {
            var definition = new HttpRequestDefinition("http://example.com/")
            {
                Method = "GET"
            };

            ITestHttpExpectation obj = new HttpHeadTest(definition);

            Assert.Throws<ArgumentNullException>(() => obj.Check(null));
        }

        [Fact]
        public void ITestHttpExpectation_op_Check_Response()
        {
            var definition = new HttpRequestDefinition("http://example.com/")
            {
                Method = "GET"
            };

            ITestHttpExpectation obj = new HttpHeadTest(definition);

            var response = new Response();

            Assert.Throws<ArgumentNullException>(() => obj.Check(null));
        }
    }
}