namespace Cavity.Data
{
    using System;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryUpdateRecordUrnExistsOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryUpdateRecordUrnExists<int>>()
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
            Assert.NotNull(new RepositoryUpdateRecordUrnExists<int>());
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var obj = new RepositoryUpdateRecordUrnExists<int>();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.Update(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.Insert(obj.Record2.Object))
                .Returns(obj.Record2.Object)
                .Verifiable();
            repository
                .Setup(x => x.Update(obj.Record2.Object))
                .Throws(new RepositoryException())
                .Verifiable();

            obj.Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryUpdateRecordUrnExists<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenRepositoryExceptionIsNotThrown()
        {
            var obj = new RepositoryUpdateRecordUrnExists<int>();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.Update(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.Insert(obj.Record2.Object))
                .Returns(obj.Record2.Object)
                .Verifiable();
            repository
                .Setup(x => x.Update(obj.Record2.Object))
                .Returns(obj.Record2.Object)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenUnexpectedExceptionIsThrownOnDuplicateRecord()
        {
            var obj = new RepositoryUpdateRecordUrnExists<int>();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.Update(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.Insert(obj.Record2.Object))
                .Returns(obj.Record2.Object)
                .Verifiable();
            repository
                .Setup(x => x.Update(obj.Record2.Object))
                .Throws(new InvalidOperationException())
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenUnexpectedExceptionIsThrownOnInitialRecord()
        {
            var obj = new RepositoryUpdateRecordUrnExists<int>();

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