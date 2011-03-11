namespace Cavity.Data
{
    using System;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryInsertRecordCacheabilityNullOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryInsertRecordCacheabilityNull<int>>()
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
            Assert.NotNull(new RepositoryInsertRecordCacheabilityNull<int>());
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var obj = new RepositoryInsertRecordCacheabilityNull<int>();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Throws(new RepositoryException())
                .Verifiable();

            obj.Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryInsertRecordCacheabilityNull<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenRepositoryExceptionIsNotThrown()
        {
            var obj = new RepositoryInsertRecordCacheabilityNull<int>();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenUnexpectedExceptionIsThrown()
        {
            var obj = new RepositoryInsertRecordCacheabilityNull<int>();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Throws(new InvalidOperationException())
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}