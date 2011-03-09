namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Cavity;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryInsertRecordNullFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryInsertRecordNull>()
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
            Assert.NotNull(new RepositoryInsertRecordNull());
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "This is only for testing purposes.")]
        public void op_Verify_IRepository()
        {
            var repository = new Mock<IRepository>();
            repository
                .Setup(x => x.Insert(null))
                .Throws(new ArgumentNullException())
                .Verifiable();

            new RepositoryInsertRecordNull().Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenExceptionIsNotThrown()
        {
            var repository = new Mock<IRepository>();
            repository
                .Setup(x => x.Insert(null))
                .Returns(null as IRecord)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => new RepositoryInsertRecordNull().Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenExceptionIsUnexpectedlyThrown()
        {
            var repository = new Mock<IRepository>();
            repository
                .Setup(x => x.Insert(null))
                .Throws(new InvalidOperationException())
                .Verifiable();

            Assert.Throws<UnitTestException>(() => new RepositoryInsertRecordNull().Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryInsertRecordNull().Verify(null));
        }
    }
}