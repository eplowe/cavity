namespace Cavity.Data
{
    using Cavity;
    using Xunit;

    public sealed class RepositoryExpectationsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryExpectations<FakeRepository>>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new RepositoryExpectations<FakeRepository>());
        }

        [Fact]
        public void op_VerifyAll()
        {
            new RepositoryExpectations<FakeRepository>().VerifyAll();
        }
    }
}