namespace Cavity.Data
{
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

        [Fact]
        public void expectations_int()
        {
            new RepositoryExpectations<int>().VerifyAll<FakeRepository<int>>();
        }

        [Fact]
        public void expectations_string()
        {
            new RepositoryExpectations<string>().VerifyAll<FakeRepository<string>>();
        }
    }
}