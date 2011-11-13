namespace Cavity.Net
{
    using Xunit;

    public sealed class HttpExchangeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpExchange>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new HttpExchange());
        }

        [Fact]
        public void prop_Request()
        {
            Assert.True(new PropertyExpectations<HttpExchange>(x => x.Request)
                            .IsAutoProperty<HttpRequest>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Response()
        {
            Assert.True(new PropertyExpectations<HttpExchange>(x => x.Response)
                            .IsAutoProperty<HttpResponse>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}