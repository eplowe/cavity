namespace Cavity.Data
{
    using System;
    using Cavity;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryInsertRecordUrnExistsOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryInsertRecordUrnExists<int>>()
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
            Assert.NotNull(new RepositoryInsertRecordUrnExists<int>());
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var obj = new RepositoryInsertRecordUrnExists<int>();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.Insert(obj.Duplicate.Object))
                .Throws(new RepositoryException())
                .Verifiable();

            obj.Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenRepositoryExceptionIsNotThrown()
        {
            var obj = new RepositoryInsertRecordUrnExists<int>();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.Insert(obj.Duplicate.Object))
                .Returns(obj.Duplicate.Object)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenUnexpectedExceptionIsThrownOnDuplicateRecord()
        {
            var obj = new RepositoryInsertRecordUrnExists<int>();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.Insert(obj.Duplicate.Object))
                .Throws(new InvalidOperationException())
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenUnexpectedExceptionIsThrownOnInitialRecord()
        {
            var obj = new RepositoryInsertRecordUrnExists<int>();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Throws(new InvalidOperationException())
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryInsertRecordUrnExists<int>().Verify(null));
        }
    }
}