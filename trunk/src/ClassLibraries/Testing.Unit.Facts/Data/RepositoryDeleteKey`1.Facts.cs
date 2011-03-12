namespace Cavity.Data
{
    using System;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryDeleteKeyOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryDeleteKey<int>>()
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
            Assert.NotNull(new RepositoryDeleteKey<int>());
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var key = AlphaDecimal.Random();

            var obj = new RepositoryDeleteKey<int>();
            obj.Record.Object.Key = key;

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Delete(key))
                .Returns(true)
                .Verifiable();
            repository
                .Setup(x => x.Exists(key))
                .Returns(false)
                .Verifiable();

            obj.Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryDeleteKey<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenExists()
        {
            var key = AlphaDecimal.Random();

            var obj = new RepositoryDeleteKey<int>();
            obj.Record.Object.Key = key;

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Delete(key))
                .Returns(true)
                .Verifiable();
            repository
                .Setup(x => x.Exists(key))
                .Returns(true)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenFalse()
        {
            var key = AlphaDecimal.Random();

            var obj = new RepositoryDeleteKey<int>();
            obj.Record.Object.Key = key;

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Delete(key))
                .Returns(false)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenInvalidOperationException()
        {
            var obj = new RepositoryDeleteKey<int>();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();

            Assert.Throws<InvalidOperationException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}