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
        public void op_Run()
        {
            var mock = new Mock<ITask>();
            mock
                .Setup(x => x.Run())
                .Verifiable();

            mock.Object.Run();

            mock.VerifyAll();
        }
    }
}