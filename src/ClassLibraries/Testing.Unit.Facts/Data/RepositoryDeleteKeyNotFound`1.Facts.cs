namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryDeleteKeyNotFoundOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryDeleteKeyNotFound<int>>()
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
            Assert.NotNull(new RepositoryDeleteKeyNotFound<int>());
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "This is only for testing purposes.")]
        public void op_Verify_IRepository()
        {
            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Delete(It.IsAny<AlphaDecimal>()))
                .Returns(false)
                .Verifiable();

            new RepositoryDeleteKeyNotFound<int>().Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryDeleteKeyNotFound<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenKeyIsReturned()
        {
            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Delete(It.IsAny<AlphaDecimal>()))
                .Returns(true)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => new RepositoryDeleteKeyNotFound<int>().Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}