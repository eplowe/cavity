namespace Cavity.Models
{
    using Xunit;

    public sealed class LocalityFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Locality>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }
    }
}