namespace Cavity.Diagnostics
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using Cavity;
    using Moq;
    using Xunit;

    public sealed class IProcessFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IProcess>()
                            .IsInterface()
                            .Implements<IDisposable>()
                            .Result);
        }

        [Fact]
        public void op_Start()
        {
            const bool expected = true;

            var mock = new Mock<IProcess>();
            mock
                .Setup(x => x.Start())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Start();

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_ExitCode_get()
        {
            const int expected = 123;

            var mock = new Mock<IProcess>();
            mock
                .SetupGet(x => x.ExitCode)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.ExitCode;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_StandardError_get()
        {
            using (var stream = new MemoryStream())
            using (var expected = new StreamReader(stream))
            {
                var mock = new Mock<IProcess>();
                mock
                    .SetupGet(x => x.StandardError)
                    .Returns(expected)
                    .Verifiable();

                var actual = mock.Object.StandardError;

                Assert.Same(expected, actual);

                mock.VerifyAll();
            }
        }

        [Fact]
        public void prop_StandardOutput_get()
        {
            using (var stream = new MemoryStream())
            using (var expected = new StreamReader(stream))
            {
                var mock = new Mock<IProcess>();
                mock
                    .SetupGet(x => x.StandardOutput)
                    .Returns(expected)
                    .Verifiable();

                var actual = mock.Object.StandardOutput;

                Assert.Same(expected, actual);

                mock.VerifyAll();
            }
        }

        [Fact]
        public void prop_StartInfo_get()
        {
            var expected = new ProcessStartInfo();

            var mock = new Mock<IProcess>();
            mock
                .SetupGet(x => x.StartInfo)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.StartInfo;

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_StartInfo_set()
        {
            var value = new ProcessStartInfo();
            var mock = new Mock<IProcess>();
            mock
                .SetupSet(x => x.StartInfo = value)
                .Verifiable();

            mock.Object.StartInfo = value;

            mock.VerifyAll();
        }
    }
}