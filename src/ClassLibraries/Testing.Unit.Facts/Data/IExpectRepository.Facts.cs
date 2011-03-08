namespace Cavity.Data
{
    using System;
    using Cavity;
    using Moq;
    using Xunit;

    public sealed class IExpectRepositoryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IExpectRepository>()
                .IsInterface()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var repository = new Mock<IRepository>().Object;

            var mock = new Mock<IExpectRepository>();
            mock
                .Setup(x => x.Verify(repository))
                .Verifiable();

            mock.Object.Verify(repository);

            mock.VerifyAll();
        }
    }
}