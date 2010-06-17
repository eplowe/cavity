namespace Cavity.Net
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class HttpClientFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<HttpClient>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new HttpClient());
        }

        [Fact]
        public void prop_Settings()
        {
            Assert.NotNull(new PropertyExpectations<HttpClient>("Settings")
                .TypeIs<HttpClientSettings>()
                .DefaultValueIsNotNull()
                .ArgumentNullException()
                .IsNotDecorated()
                .Result);
        }
    }
}