namespace Cavity.Models
{
    using Xunit;

    public sealed class BuildingFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Building>()
                            .DerivesFrom<DeliveryPoint>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new Building());
        }
    }
}