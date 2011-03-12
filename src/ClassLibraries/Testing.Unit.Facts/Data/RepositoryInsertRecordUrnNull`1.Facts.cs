namespace Cavity.Data
{
    using System;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryInsertRecordUrnNullOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryInsertRecordUrnNull<int>>()
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
            Assert.NotNull(new RepositoryInsertRecordUrnNull<int>());
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var obj = new RepositoryInsertRecordUrnNull<int>();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Throws(new RepositoryException())
                .Verifiable();

            obj.Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryInsertRecordUrnNull<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenRepositoryExceptionIsNotThrown()
        {
            var obj = new RepositoryInsertRecordUrnNull<int>();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenUnexpectedExceptionIsThrown()
        {
            var obj = new RepositoryInsertRecordUrnNull<int>();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Throws(new InvalidOperationException())
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}