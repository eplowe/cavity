namespace Cavity.Win32
{
    using Cavity.Collections.Generic;
    using Xunit;

    public sealed class FakeRegistryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<FakeRegistry>()
                            .DerivesFrom<MultitonCollection<string, MultitonCollection<string, object>>>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new FakeRegistry());
        }
    }
}