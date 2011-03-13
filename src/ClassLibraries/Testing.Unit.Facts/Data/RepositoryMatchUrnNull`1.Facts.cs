namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryMatchUrnNullOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryMatchUrnNull<int>>()
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
            Assert.NotNull(new RepositoryMatchUrnNull<int>());
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "This is only for testing purposes.")]
        public void op_Verify_IRepository()
        {
            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Match(null, It.IsAny<string>()))
                .Throws(new ArgumentNullException())
                .Verifiable();

            new RepositoryMatchUrnNull<RandomObject>().Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryMatchUrnNull<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenExceptionIsNotThrown()
        {
            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Match(null, It.IsAny<string>()))
                .Returns(false)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => new RepositoryMatchUrnNull<RandomObject>().Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenExceptionIsUnexpectedlyThrown()
        {
            var repository = new Mock<IRepository<RandomObject>>();
            repository
                .Setup(x => x.Match(null, It.IsAny<string>()))
                .Throws(new InvalidOperationException())
                .Verifiable();

            Assert.Throws<UnitTestException>(() => new RepositoryMatchUrnNull<RandomObject>().Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}