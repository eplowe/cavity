namespace Cavity.Threading
{
    using System;
    using System.Threading;
    using Cavity;
    using Moq;
    using Xunit;

    public sealed class IThreadedObjectFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IThreadedObject>()
                            .IsInterface()
                            .Implements<IDisposable>()
                            .Result);
        }

        [Fact]
        public void op_Cancellation_get()
        {
            var expected = new CancellationToken();

            var mock = new Mock<IThreadedObject>();
            mock
                .SetupGet(x => x.CancellationToken)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.CancellationToken;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Cancellation_set()
        {
            var value = new CancellationToken();

            var mock = new Mock<IThreadedObject>();
            mock
                .SetupSet(x => x.CancellationToken = value)
                .Verifiable();

            mock.Object.CancellationToken = value;

            mock.VerifyAll();
        }
    }
}