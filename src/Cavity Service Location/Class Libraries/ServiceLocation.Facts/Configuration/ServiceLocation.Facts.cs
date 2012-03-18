namespace Cavity.Configuration
{
    using System.Configuration;

    using Xunit;

    public sealed class ServiceLocationFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ServiceLocation>()
                            .DerivesFrom<ConfigurationSection>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new ServiceLocation());
        }
    }
}