namespace Cavity.Data
{
    using System;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryToKeyUrnOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryToKeyUrn<int>>()
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
            Assert.NotNull(new RepositoryToKeyUrn<int>());
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var obj = new RepositoryToKeyUrn<int>();
            obj.Record.Object.Key = AlphaDecimal.Random();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.ToKey(obj.Record.Object.Urn))
                .Returns(obj.Record.Object.Key)
                .Verifiable();

            obj.Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryToKeyUrn<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenKeyIsDifferent()
        {
            var obj = new RepositoryToKeyUrn<int>();
            obj.Record.Object.Key = AlphaDecimal.Random();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.ToKey(obj.Record.Object.Urn))
                .Returns(AlphaDecimal.Random())
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenNullIsReturned()
        {
            var obj = new RepositoryToKeyUrn<int>();
            obj.Record.Object.Key = AlphaDecimal.Random();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.ToKey(obj.Record.Object.Urn))
                .Returns(null as AlphaDecimal?)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}