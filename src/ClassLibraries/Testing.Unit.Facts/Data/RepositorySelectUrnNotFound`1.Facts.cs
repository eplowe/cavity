namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositorySelectUrnNotFoundOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositorySelectUrnNotFound<int>>()
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
            Assert.NotNull(new RepositorySelectUrnNotFound<int>());
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "This is only for testing purposes.")]
        public void op_Verify_IRepository()
        {
            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Select(It.IsAny<AbsoluteUri>()))
                .Returns(null as IRecord<int>)
                .Verifiable();

            new RepositorySelectUrnNotFound<int>().Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositorySelectUrnNotFound<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenRecordIsReturned()
        {
            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Select(It.IsAny<AbsoluteUri>()))
                .Returns(new Mock<IRecord<int>>().Object)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => new RepositorySelectUrnNotFound<int>().Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}