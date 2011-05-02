namespace Cavity
{
    using System;
    using Cavity;
    using Moq;
    using Xunit;

    public sealed class ICommandFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ICommand>()
                .IsInterface()
                .Result);
        }

        [Fact]
        public void op_Act()
        {
            var mock = new Mock<ICommand>();
            mock
                .Setup(x => x.Act())
                .Returns(true)
                .Verifiable();

            Assert.True(mock.Object.Act());

            mock.VerifyAll();
        }

        [Fact]
        public void op_Revert()
        {
            var mock = new Mock<ICommand>();
            mock
                .Setup(x => x.Revert())
                .Returns(true)
                .Verifiable();

            Assert.True(mock.Object.Revert());

            mock.VerifyAll();
        }
    }
}