namespace Cavity.Models
{
    using Xunit;

    public sealed class DeliveryPointFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<DeliveryPoint>()
                            .DerivesFrom<AddressLine>()
                            .IsAbstractBaseClass()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Number()
        {
            Assert.NotNull(new PropertyExpectations<DeliveryPoint>(x => x.Number)
                               .TypeIs<AddressNumber>()
                               .IsNotDecorated()
                               .Result);
        }
    }
}