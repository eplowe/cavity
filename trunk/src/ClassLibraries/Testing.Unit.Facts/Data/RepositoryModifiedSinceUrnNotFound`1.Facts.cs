namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryModifiedSinceUrnNotFoundOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryModifiedSinceUrnNotFound<int>>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IVerifyRepository<int>>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new RepositoryModifiedSinceUrnNotFound<int>());
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "This is only for testing purposes.")]
        public void op_Verify_IRepository()
        {
            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.ModifiedSince(It.IsAny<AbsoluteUri>(), It.IsAny<DateTime>()))
                .Returns(false)
                .Verifiable();

            new RepositoryModifiedSinceUrnNotFound<int>().Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryModifiedSinceUrnNotFound<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenTrue()
        {
            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.ModifiedSince(It.IsAny<AbsoluteUri>(), It.IsAny<DateTime>()))
                .Returns(true)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => new RepositoryModifiedSinceUrnNotFound<int>().Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}