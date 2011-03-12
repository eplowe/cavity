namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryMatchKeyNotFoundOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryMatchKeyNotFound<int>>()
                            .DerivesFrom<VerifyRepositoryBase<int>>()
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
            Assert.NotNull(new RepositoryMatchKeyNotFound<int>());
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "This is only for testing purposes.")]
        public void op_Verify_IRepository()
        {
            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Match(It.IsAny<AlphaDecimal>(), It.IsAny<string>()))
                .Returns(false)
                .Verifiable();

            new RepositoryMatchKeyNotFound<int>().Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryMatchKeyNotFound<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenFalse()
        {
            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Match(It.IsAny<AlphaDecimal>(), It.IsAny<string>()))
                .Returns(true)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => new RepositoryMatchKeyNotFound<int>().Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}