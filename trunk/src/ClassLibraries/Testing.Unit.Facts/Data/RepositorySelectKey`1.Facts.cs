namespace Cavity.Data
{
    using System;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositorySelectKeyOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositorySelectKey<int>>()
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
            Assert.NotNull(new RepositorySelectKey<int>());
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var key = AlphaDecimal.Random();

            var obj = new RepositorySelectKey<int>();
            obj.Record.Object.Key = key;

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.Select(key))
                .Returns(obj.Record.Object)
                .Verifiable();

            obj.Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositorySelectKey<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenKeyIsDifferent()
        {
            var key = AlphaDecimal.Random();

            var obj = new RepositorySelectKey<int>();
            obj.Record.Object.Key = key;

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();

            var record = new Mock<IRecord<int>>();
            record
                .SetupGet(x => x.Key)
                .Returns(AlphaDecimal.Random());
            record
                .SetupGet(x => x.Urn)
                .Returns(obj.Record.Object.Urn);
            repository
                .Setup(x => x.Select(key))
                .Returns(record.Object)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenNullIsReturned()
        {
            var key = AlphaDecimal.Random();

            var obj = new RepositorySelectKey<int>();
            obj.Record.Object.Key = key;

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.Select(key))
                .Returns(null as IRecord<int>)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenUrnIsDifferent()
        {
            var key = AlphaDecimal.Random();

            var obj = new RepositorySelectKey<int>();
            obj.Record.Object.Key = key;

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();

            var record = new Mock<IRecord<int>>();
            record
                .SetupGet(x => x.Key)
                .Returns(obj.Record.Object.Key);
            record
                .SetupGet(x => x.Urn)
                .Returns("urn://example.net/" + Guid.NewGuid());
            repository
                .Setup(x => x.Select(key))
                .Returns(record.Object)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenValueIsDifferent()
        {
            var key = AlphaDecimal.Random();

            var obj = new RepositorySelectKey<int>();
            obj.Record.Object.Key = key;
            obj.Record.Object.Value = 123;

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();

            var record = new Mock<IRecord<int>>();
            record
                .SetupGet(x => x.Key)
                .Returns(obj.Record.Object.Key);
            record
                .SetupGet(x => x.Urn)
                .Returns(obj.Record.Object.Urn);
            record
                .SetupGet(x => x.Value)
                .Returns(456);
            repository
                .Setup(x => x.Select(key))
                .Returns(record.Object)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}