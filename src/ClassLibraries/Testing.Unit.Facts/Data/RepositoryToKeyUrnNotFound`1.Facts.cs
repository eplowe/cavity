namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryToKeyUrnNotFoundOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryToKeyUrnNotFound<int>>()
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
            Assert.NotNull(new RepositoryToKeyUrnNotFound<int>());
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "This is only for testing purposes.")]
        public void op_Verify_IRepository()
        {
            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.ToKey(It.IsAny<AbsoluteUri>()))
                .Returns(null as AlphaDecimal?)
                .Verifiable();

            new RepositoryToKeyUrnNotFound<int>().Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryToKeyUrnNotFound<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenKeyIsReturned()
        {
            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.ToKey(It.IsAny<AbsoluteUri>()))
                .Returns(AlphaDecimal.Random())
                .Verifiable();

            Assert.Throws<UnitTestException>(() => new RepositoryToKeyUrnNotFound<int>().Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}