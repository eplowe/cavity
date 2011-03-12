namespace Cavity.Data
{
    using System;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryDeleteUrnOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryDeleteUrn<int>>()
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
            Assert.NotNull(new RepositoryDeleteUrn<int>());
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var obj = new RepositoryDeleteUrn<int>();
            obj.Record.Object.Key = AlphaDecimal.Random();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.Delete(obj.Record.Object.Urn))
                .Returns(true)
                .Verifiable();
            repository
                .Setup(x => x.Exists(obj.Record.Object.Urn))
                .Returns(false)
                .Verifiable();

            obj.Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryDeleteUrn<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenExists()
        {
            var obj = new RepositoryDeleteUrn<int>();
            obj.Record.Object.Key = AlphaDecimal.Random();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.Delete(obj.Record.Object.Urn))
                .Returns(true)
                .Verifiable();
            repository
                .Setup(x => x.Exists(obj.Record.Object.Urn))
                .Returns(true)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenFalse()
        {
            var obj = new RepositoryDeleteUrn<int>();
            obj.Record.Object.Key = AlphaDecimal.Random();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.Delete(obj.Record.Object.Urn))
                .Returns(false)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}