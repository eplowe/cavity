namespace Cavity.Data
{
    using System;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryInsertRecordOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryInsertRecord<int>>()
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
            Assert.NotNull(new RepositoryInsertRecord<int>());
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var obj = new RepositoryInsertRecord<int>();
            obj.Record.Object.Created = DateTime.UtcNow;
            obj.Record.Object.Key = AlphaDecimal.Random();
            obj.Record.Object.Modified = DateTime.UtcNow;

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();

            obj.Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryInsertRecord<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenCreatedIsNotSet()
        {
            var obj = new RepositoryInsertRecord<int>();
            obj.Record.Object.Created = null;
            obj.Record.Object.Key = AlphaDecimal.Random();
            obj.Record.Object.Modified = DateTime.UtcNow;

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenKeyIsNotSet()
        {
            var obj = new RepositoryInsertRecord<int>();
            obj.Record.Object.Created = DateTime.UtcNow;
            obj.Record.Object.Key = null;
            obj.Record.Object.Modified = DateTime.UtcNow;

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenModifiedIsNotSet()
        {
            var obj = new RepositoryInsertRecord<int>();
            obj.Record.Object.Created = DateTime.UtcNow;
            obj.Record.Object.Key = AlphaDecimal.Random();
            obj.Record.Object.Modified = null;

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}