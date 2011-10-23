namespace Cavity.Models
{
    using Xunit;

    public sealed class ResidentialUserCategoryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ResidentialUserCategory>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IUserCategory>()
                            .Result);
        }

        [Fact]
        public void prop_Code()
        {
            Assert.Equal('R', new ResidentialUserCategory().Code);
        }

        [Fact]
        public void prop_Name()
        {
            Assert.Equal("Residential", new ResidentialUserCategory().Name);
        }
    }
}