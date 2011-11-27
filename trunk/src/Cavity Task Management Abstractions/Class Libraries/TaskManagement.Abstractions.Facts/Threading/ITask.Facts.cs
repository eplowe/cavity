namespace Cavity.Threading
{
    using System;
    using System.ComponentModel.Composition;
    using System.Threading;
    using System.Threading.Tasks;
    using Moq;
    using Xunit;

    public sealed class ITaskFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ITask>()
                            .IsInterface()
                            .IsDecoratedWith<InheritedExportAttribute>()
                            .Implements<IDisposable>()
                            .Result);
        }

        [Fact]
        public void op_Run_CancellationToken()
        {
            var cancellation = new CancellationToken();

            var mock = new Mock<ITask>();
            mock
                .Setup(x => x.Run(cancellation))
                .Verifiable();

            mock.Object.Run(cancellation);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_CancellationToken_get()
        {
            var expected = new CancellationToken();

            var mock = new Mock<ITask>();
            mock
                .SetupGet(x => x.CancellationToken)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.CancellationToken;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_ContinuationOptions_get()
        {
            const TaskContinuationOptions expected = TaskContinuationOptions.None;

            var mock = new Mock<ITask>();
            mock
                .SetupGet(x => x.ContinuationOptions)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.ContinuationOptions;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_CreationOptions_get()
        {
            const TaskCreationOptions expected = TaskCreationOptions.None;

            var mock = new Mock<ITask>();
            mock
                .SetupGet(x => x.CreationOptions)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.CreationOptions;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }
    }
}