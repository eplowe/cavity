namespace Cavity.Caching
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class MemoryCacheCollectionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<MemoryCacheCollection>()
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
            Assert.NotNull(new MemoryCacheCollection());
        }
    }
}