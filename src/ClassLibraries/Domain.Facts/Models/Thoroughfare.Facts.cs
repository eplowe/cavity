namespace Cavity.Models
{
    using Xunit;

    public sealed class ThoroughfareFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Thoroughfare>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Qualifier()
        {
            Assert.NotNull(new PropertyExpectations<Thoroughfare>(x => x.Qualifier)
                               .TypeIs<string>()
                               .IsNotDecorated()
                               .Result);
        }
    }
}