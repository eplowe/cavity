namespace Cavity.Data
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class FileRepositoryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<FileRepository<int>>()
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
            Assert.NotNull(new FileRepository<int>());
        }

        [Fact]
        public void expectations_class()
        {
            ////new RepositoryExpectations<RandomObject>().VerifyAll<FileRepository<RandomObject>>();
        }

        [Fact]
        public void expectations_struct()
        {
            //// new RepositoryExpectations<int>().VerifyAll<FakeRepository<int>>();
        }
    }
}