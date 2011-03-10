namespace Cavity.Data
{
    using System;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryModifiedSinceKeyOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryModifiedSinceKey<int>>()
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
            Assert.NotNull(new RepositoryModifiedSinceKey<int>());
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var key = AlphaDecimal.Random();

            var obj = new RepositoryModifiedSinceKey<int>();
            obj.Record.Object.Key = key;

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.ModifiedSince(key, DateTime.MaxValue))
                .Returns(false)
                .Verifiable();
            repository
                .Setup(x => x.ModifiedSince(key, DateTime.MinValue))
                .Returns(true)
                .Verifiable();

            obj.Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryModifiedSinceKey<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenModifiedSinceMaxDate()
        {
            var key = AlphaDecimal.Random();

            var obj = new RepositoryModifiedSinceKey<int>();
            obj.Record.Object.Key = key;

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.ModifiedSince(key, DateTime.MaxValue))
                .Returns(true)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenNotModifiedSinceMinDate()
        {
            var key = AlphaDecimal.Random();

            var obj = new RepositoryModifiedSinceKey<int>();
            obj.Record.Object.Key = key;

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.ModifiedSince(key, DateTime.MaxValue))
                .Returns(false)
                .Verifiable();
            repository
                .Setup(x => x.ModifiedSince(key, DateTime.MinValue))
                .Returns(false)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenInvalidOperationException()
        {
            var obj = new RepositoryModifiedSinceKey<int>();

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