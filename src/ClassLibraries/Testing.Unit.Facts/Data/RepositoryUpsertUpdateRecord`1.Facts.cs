namespace Cavity.Data
{
    using System;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryUpsertUpdateRecordOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryUpsertUpdateRecord<int>>()
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
            Assert.NotNull(new RepositoryUpsertUpdateRecord<int>());
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var obj = new RepositoryUpsertUpdateRecord<int>();
            obj.Record.Object.Key = AlphaDecimal.Random();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.Upsert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();

            obj.Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryUpsertUpdateRecord<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenFalse()
        {
            var obj = new RepositoryUpsertUpdateRecord<int>();
            obj.Record.Object.Key = AlphaDecimal.Random();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.Upsert(obj.Record.Object))
                .Returns(new Mock<IRecord<int>>().Object)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}