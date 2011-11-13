namespace Cavity.Net
{
    using Xunit;

    public sealed class HttpMessageFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpMessage>()
                            .DerivesFrom<object>()
                            .IsAbstractBaseClass()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Body()
        {
            Assert.True(new PropertyExpectations<HttpMessage>(p => p.Body)
                            .TypeIs<IHttpMessageBody>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Headers()
        {
            Assert.True(new PropertyExpectations<HttpMessage>(p => p.Headers)
                            .TypeIs<IHttpMessageHeaders>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}