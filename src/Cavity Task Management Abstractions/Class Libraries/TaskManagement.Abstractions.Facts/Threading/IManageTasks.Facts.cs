namespace Cavity.Threading
{
    using System.Collections.Generic;

    using Moq;

    using Xunit;

    public sealed class IManageTasksFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IManageTasks>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void op_Continue()
        {
            var mock = new Mock<IManageTasks>();
            mock
                .Setup(x => x.Continue())
                .Verifiable();

            mock.Object.Continue();

            mock.VerifyAll();
        }

        [Fact]
        public void op_Pause()
        {
            var mock = new Mock<IManageTasks>();
            mock
                .Setup(x => x.Pause())
                .Verifiable();

            mock.Object.Pause();

            mock.VerifyAll();
        }

        [Fact]
        public void op_Shutdown()
        {
            var mock = new Mock<IManageTasks>();
            mock
                .Setup(x => x.Shutdown())
                .Verifiable();

            mock.Object.Shutdown();

            mock.VerifyAll();
        }

        [Fact]
        public void op_Start_IEnumerableOfString()
        {
            var args = new List<string>();
            var mock = new Mock<IManageTasks>();
            mock
                .Setup(x => x.Start(args))
                .Verifiable();

            mock.Object.Start(args);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Stop()
        {
            var mock = new Mock<IManageTasks>();
            mock
                .Setup(x => x.Stop())
                .Verifiable();

            mock.Object.Stop();

            mock.VerifyAll();
        }
    }
}