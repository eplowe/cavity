namespace Cavity.Data
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class FakeRepositoryOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<FakeRepository<int>>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Implements<IRepository<int>>()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new FakeRepository<int>());
        }
    }
}