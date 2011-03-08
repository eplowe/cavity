namespace Cavity.Data
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class FakeRepositoryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<FakeRepository>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Implements<IRepository>()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new FakeRepository());
        }
    }
}