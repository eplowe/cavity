namespace Cavity.IO
{
    using System;
    using System.IO;
    using Moq;
    using Xunit;

    public sealed class IReceiveFileFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IReceiveFile>()
                            .IsInterface()
                            .Implements<IDisposable>()
                            .Result);
        }

        [Fact]
        public void op_Receive_FileInfo_DirectoryInfo()
        {
            var file = new FileInfo("example.txt");

            var mock = new Mock<IReceiveFile>();
            mock
                .Setup(x => x.Receive(file))
                .Verifiable();

            mock.Object.Receive(file);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Receive_string_DirectoryInfo()
        {
            var file = new FileInfo("example.txt");

            var mock = new Mock<IReceiveFile>();
            mock
                .Setup(x => x.Receive(file.FullName))
                .Verifiable();

            mock.Object.Receive(file.FullName);

            mock.VerifyAll();
        }
    }
}