namespace Cavity.Models
{
    using Xunit;

    public sealed class RegionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Region>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }
    }
}