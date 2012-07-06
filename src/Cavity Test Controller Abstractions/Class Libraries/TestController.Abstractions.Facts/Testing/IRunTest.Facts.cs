namespace Cavity.Testing
{
    using System.ComponentModel.Composition;
    using System.IO;

    using Cavity.IO;

    using Moq;

    using Xunit;

    public sealed class IRunTestFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IRunTest>()
                            .IsInterface()
                            .IsDecoratedWith<InheritedExportAttribute>()
                            .Result);
        }

        [Fact]
        public void op_Run_DirectoryInfo_TextWriter()
        {
            using (var temp = new TempDirectory())
            {
                var log = temp.Info.ToFile("log.txt");
                using (var stream = File.Open(log.FullName, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        var mock = new Mock<IRunTest>();

                        // ReSharper disable AccessToDisposedClosure
                        mock
                            .Setup(x => x.Run(temp.Info, writer))
                            .Returns(true)
                            .Verifiable();

                        // ReSharper restore AccessToDisposedClosure
                        Assert.True(mock.Object.Run(temp.Info, writer));

                        mock.VerifyAll();
                    }
                }
            }
        }

        [Fact]
        public void prop_Description_get()
        {
            const string expected = "This is an example.";

            var mock = new Mock<IRunTest>();
            mock
                .SetupGet(x => x.Description)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Description;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Title_get()
        {
            const string expected = "Example";

            var mock = new Mock<IRunTest>();
            mock
                .SetupGet(x => x.Title)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Title;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }
    }
}