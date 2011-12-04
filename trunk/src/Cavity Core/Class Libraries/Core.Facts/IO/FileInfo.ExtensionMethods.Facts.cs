namespace Cavity.IO
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.XPath;
    using Xunit;

    public sealed class FileInfoExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(FileInfoExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_AppendLine_FileInfoNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).AppendLine("example"));
        }

        [Fact]
        public void op_AppendLine_FileInfo_string()
        {
            const string expected = "example";
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.AppendLine(expected));

                var actual = file.ReadToEnd();

                Assert.Equal(expected + Environment.NewLine, actual);
            }
        }

        [Fact]
        public void op_AppendLine_FileInfo_stringEmpty()
        {
            var expected = string.Empty;
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.AppendLine(expected));

                var actual = file.ReadToEnd();

                Assert.Equal(expected + Environment.NewLine, actual);
            }
        }

        [Fact]
        public void op_AppendLine_FileInfo_stringNull()
        {
            var expected = string.Empty;
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.AppendLine(null));

                var actual = file.ReadToEnd();

                Assert.Equal(expected + Environment.NewLine, actual);
            }
        }

        [Fact]
        public void op_AppendLine_FileInfo_string_whenFileExists()
        {
            const string expected = "example";
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.CreateNew(string.Empty));
                Assert.Same(file, file.AppendLine(expected));

                var actual = file.ReadToEnd();

                Assert.Equal(expected + Environment.NewLine, actual);
            }
        }

        [Fact]
        public void op_Append_FileInfoNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).Append("example"));
        }

        [Fact]
        public void op_Append_FileInfo_string()
        {
            const string expected = "example";
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.Append(expected));

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Append_FileInfo_stringEmpty()
        {
            var expected = string.Empty;
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.Append(expected));

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Append_FileInfo_stringNull()
        {
            var expected = string.Empty;
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.Append(null));

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Append_FileInfo_string_whenFileExists()
        {
            const string expected = "example";
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.CreateNew(string.Empty));
                Assert.Same(file, file.Append(expected));

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_CreateNew_FileInfo()
        {
            var expected = string.Empty;
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.CreateNew());

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_CreateNew_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).CreateNew());
        }

        [Fact]
        public void op_CreateNew_FileInfoNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).CreateNew("example"));
        }

        [Fact]
        public void op_CreateNew_FileInfo_string()
        {
            const string expected = "example";
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.CreateNew(expected));

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_CreateNew_FileInfo_stringEmpty()
        {
            var expected = string.Empty;
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.CreateNew(expected));

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_CreateNew_FileInfo_stringNull()
        {
            var expected = string.Empty;
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.CreateNew(null));

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_CreateNew_FileInfo_string_whenExists()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.CreateNew(string.Empty));

                Assert.Throws<IOException>(() => file.CreateNew("example"));
            }
        }

        [Fact]
        public void op_CreateNew_FileInfo_whenExists()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.CreateNew());

                Assert.Throws<IOException>(() => file.CreateNew());
            }
        }

        [Fact]
        public void op_Create_FileInfoNull_IXPathNavigable()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<example />");

            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).Create(xml));
        }

        [Fact]
        public void op_Create_FileInfoNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).Create("example"));
        }

        [Fact]
        public void op_Create_FileInfo_IXPathNavigable()
        {
            const string expected = "<example />";
            var xml = new XmlDocument();
            xml.LoadXml(expected);

            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.Create(xml));

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Create_FileInfo_IXPathNavigableNull()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());

                Assert.Throws<ArgumentNullException>(() => file.Create(null as IXPathNavigable));
            }
        }

        [Fact]
        public void op_Create_FileInfo_IXPathNavigable_whenFileExists()
        {
            const string expected = "<example />";
            var xml = new XmlDocument();
            xml.LoadXml(expected);

            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.CreateNew(string.Empty));
                Assert.Same(file, file.Create(xml));

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Create_FileInfo_string()
        {
            const string expected = "example";
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.Create(expected));

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Create_FileInfo_stringEmpty()
        {
            var expected = string.Empty;
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.Create(expected));

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Create_FileInfo_stringNull()
        {
            var expected = string.Empty;
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.Create(null as string));

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Create_FileInfo_string_whenFileExists()
        {
            const string expected = "example";
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.CreateNew(string.Empty));
                Assert.Same(file, file.Create(expected));

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Lines_FileInfo()
        {
            const string expected = "example";
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.CreateNew(expected));

                foreach (var actual in file.Lines())
                {
                    Assert.Equal(expected, actual);
                }
            }
        }

        [Fact]
        public void op_Lines_FileInfoEmpty()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.CreateNew());

                Assert.Empty(file.Lines());
            }
        }

        [Fact]
        public void op_Lines_FileInfoMissing()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());

                Assert.Throws<FileNotFoundException>(() => file.Lines().ToArray());
            }
        }

        [Fact]
        public void op_Lines_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).Lines().ToArray());
        }

        [Fact]
        public void op_ReadToEnd_FileInfo()
        {
            const string expected = "example";
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.CreateNew(expected));

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_ReadToEnd_FileInfoMissing()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Throws<FileNotFoundException>(() => file.ReadToEnd());
            }
        }

        [Fact]
        public void op_ReadToEnd_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).ReadToEnd());
        }

        [Fact]
        public void op_Truncate_FileInfoNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).Truncate("example"));
        }

        [Fact]
        public void op_Truncate_FileInfo_string()
        {
            const string expected = "example";
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.CreateNew(string.Empty));
                Assert.Same(file, file.Truncate(expected));

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Truncate_FileInfo_stringEmpty()
        {
            var expected = string.Empty;
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.CreateNew(string.Empty));
                Assert.Same(file, file.Truncate(expected));

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Truncate_FileInfo_stringNull()
        {
            var expected = string.Empty;
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.CreateNew(string.Empty));
                Assert.Same(file, file.Truncate(null));

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Truncate_FileInfo_string_whenFileMissing()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());

                Assert.Throws<FileNotFoundException>(() => file.Truncate("example"));
            }
        }
    }
}