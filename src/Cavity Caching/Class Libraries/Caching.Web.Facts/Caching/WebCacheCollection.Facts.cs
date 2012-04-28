namespace Cavity.Caching
{
    using Cavity;
    using Xunit;

    public sealed class WebCacheCollectionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<WebCacheCollection>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .Implements<ICacheCollection35>()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new WebCacheCollection());
        }
    }
}