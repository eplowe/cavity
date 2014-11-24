namespace Cavity.IO
{
    using System;
    using System.IO;
    using Xunit;

    public sealed class FileWriterFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<FileWriter>().DerivesFrom<StreamWriter>()
                                                          .IsConcreteClass()
                                                          .IsUnsealed()
                                                          .NoDefaultConstructor()
                                                          .IsNotDecorated()
                                                          .Result);
        }

        [Fact]
        public void ctor_FileInfo()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt");
                using (var writer = new FileWriter(file))
                {
                    Assert.NotNull(writer);
                }
            }
        }

        [Fact]
        public void ctor_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => new FileWriter(null as FileInfo));
        }

        [Fact]
        public void ctor_string()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt").FullName;
                using (var writer = new FileWriter(file))
                {
                    Assert.NotNull(writer);
                }
            }
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new FileWriter(null as string));
        }

        [Fact]
        public void op_Write_string()
        {
            using (var temp = new TempDirectory())
            {
                const string expected = "example";
                var file = temp.Info.ToFile("example.txt");
                using (var writer = new FileWriter(file))
                {
                    writer.Write(expected);
                }

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }
    }
}