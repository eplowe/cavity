﻿namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryToUrnKeyNotFoundOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryToUrnKeyNotFound<int>>()
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
            Assert.NotNull(new RepositoryToUrnKeyNotFound<int>());
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "This is only for testing purposes.")]
        public void op_Verify_IRepository()
        {
            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.ToUrn(It.IsAny<AlphaDecimal>()))
                .Returns(null as AbsoluteUri)
                .Verifiable();

            new RepositoryToUrnKeyNotFound<int>().Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryToUrnKeyNotFound<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenKeyIsReturned()
        {
            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.ToUrn(It.IsAny<AlphaDecimal>()))
                .Returns("urn://example.com/" + Guid.NewGuid())
                .Verifiable();

            Assert.Throws<UnitTestException>(() => new RepositoryToUrnKeyNotFound<int>().Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}