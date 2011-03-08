namespace Cavity.Data
{
    using System;
    using Cavity;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryInsertRecordFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryInsertRecord>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Implements<IExpectRepository>()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new RepositoryInsertRecord());
        }

        [Fact]
        public void ctor_IRecord()
        {
            Assert.NotNull(new RepositoryInsertRecord(new Mock<IRecord>().Object));
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var record = new Mock<IRecord>();
            record
                .SetupGet(x => x.Key)
                .Returns(AlphaDecimal.Random())
                .Verifiable();

            var repository = new Mock<IRepository>();
            repository
                .Setup(x => x.Insert(record.Object))
                .Returns(record.Object)
                .Verifiable();

            new RepositoryInsertRecord(record.Object).Verify(repository.Object);

            record.VerifyAll();
            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenKeyIsNotSet()
        {
            var record = new Mock<IRecord>();
            record
                .SetupGet(x => x.Key)
                .Returns((int?)null)
                .Verifiable();

            var repository = new Mock<IRepository>();
            repository
                .Setup(x => x.Insert(record.Object))
                .Returns(record.Object)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => new RepositoryInsertRecord(record.Object).Verify(repository.Object));

            record.VerifyAll();
            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryInsertRecord().Verify(null));
        }
    }
}