﻿namespace Cavity.Data
{
    using System;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryUpdateRecordOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryUpdateRecord<int>>()
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
            Assert.NotNull(new RepositoryUpdateRecord<int>());
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var obj = new RepositoryUpdateRecord<int>
            {
                Record1 =
                    {
                        Key = AlphaDecimal.Random()
                    }
            };

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();
            repository
                .Setup(x => x.Update(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();

            obj.Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryUpdateRecord<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenFalse()
        {
            var obj = new RepositoryUpdateRecord<int>
            {
                Record1 =
                    {
                        Key = AlphaDecimal.Random()
                    }
            };

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();
            repository
                .Setup(x => x.Update(obj.Record1))
                .Returns(new Mock<IRecord<int>>().Object)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}