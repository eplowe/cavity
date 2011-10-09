namespace Cavity.Models
{
    using Xunit;

    public sealed class DoorFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Door>()
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
            Assert.NotNull(new Door());
        }
    }
}