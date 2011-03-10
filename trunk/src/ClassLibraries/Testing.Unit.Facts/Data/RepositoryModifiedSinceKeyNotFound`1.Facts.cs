namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryModifiedSinceKeyNotFoundOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryModifiedSinceKeyNotFound<int>>()
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
            Assert.NotNull(new RepositoryModifiedSinceKeyNotFound<int>());
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "This is only for testing purposes.")]
        public void op_Verify_IRepository()
        {
            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.ModifiedSince(It.IsAny<AlphaDecimal>(), DateTime.MinValue))
                .Returns(false)
                .Verifiable();

            new RepositoryModifiedSinceKeyNotFound<int>().Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryModifiedSinceKeyNotFound<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenFalse()
        {
            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.ModifiedSince(It.IsAny<AlphaDecimal>(), DateTime.MinValue))
                .Returns(true)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => new RepositoryModifiedSinceKeyNotFound<int>().Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}