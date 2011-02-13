namespace Cavity.Models
{
    using Cavity.Data;
    using Moq;
    using Xunit;

    public sealed class ITaskFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ITask>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void op_Execute_DataCollection()
        {
            var expected = new DataCollection();

            var configuration = new DataCollection();

            var mock = new Mock<ITask>();
            mock
                .Setup(x => x.Execute(configuration))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Execute(configuration);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }
    }
}