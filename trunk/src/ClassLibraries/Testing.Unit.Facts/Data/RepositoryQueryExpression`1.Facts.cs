namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Xml.XPath;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryQueryExpressionOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryQueryExpression<int>>()
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
            Assert.NotNull(new RepositoryQueryExpression<int>());
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var obj = new RepositoryQueryExpression<int>();

            var records = new[]
            {
                obj.Record1,
                obj.Record2
            };

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();
            repository
                .Setup(x => x.Insert(obj.Record2))
                .Returns(obj.Record2)
                .Verifiable();
            repository
                .Setup(x => x.Query(It.IsAny<XPathExpression>()))
                .Returns(records)
                .Verifiable();

            obj.Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryQueryExpression<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenNullResults()
        {
            var obj = new RepositoryQueryExpression<int>();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();
            repository
                .Setup(x => x.Insert(obj.Record2))
                .Returns(obj.Record2)
                .Verifiable();
            repository
                .Setup(x => x.Query(It.IsAny<XPathExpression>()))
                .Returns(null as IEnumerable<IRecord<int>>)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenEmptyResults()
        {
            var obj = new RepositoryQueryExpression<int>();

            var records = new List<IRecord<int>>();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record1))
                .Returns(obj.Record1)
                .Verifiable();
            repository
                .Setup(x => x.Insert(obj.Record2))
                .Returns(obj.Record2)
                .Verifiable();
            repository
                .Setup(x => x.Query(It.IsAny<XPathExpression>()))
                .Returns(records)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}