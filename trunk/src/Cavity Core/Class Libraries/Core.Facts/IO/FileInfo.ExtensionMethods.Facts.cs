namespace Cavity.IO
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.XPath;
    using Xunit;
    using Xunit.Extensions;

    public sealed class FileInfoExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(FileInfoExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_AppendLine_FileInfoNull_object()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).AppendLine("example"));
        }

        [Fact]
        public void op_AppendLine_FileInfo_object()
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
        public void op_AppendLine_FileInfo_objectNull()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.AppendLine(null));

                Assert.True(file.NotFound());
            }
        }

        [Fact]
        public void op_AppendLine_FileInfo_object_whenFileExists()
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
        public void op_AppendLine_FileInfo_object_whenStringEmpty()
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
        public void op_Append_FileInfoNull_object()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).Append("example"));
        }

        [Fact]
        public void op_Append_FileInfo_object()
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
        public void op_Append_FileInfo_objectNull()
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
        public void op_Append_FileInfo_object_whenFileExists()
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
        public void op_Append_FileInfo_object_whenStringEmpty()
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
        public void op_ChangeExtension_FileInfoNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).ChangeExtension(".txt"));
        }

        [Theory]
        [InlineData("example", "example", null)]
        [InlineData("example.", "example", "")]
        [InlineData("example.txt", "example", ".txt")]
        [InlineData("example.txt", "example", "txt")]
        [InlineData("example", "example.extension", null)]
        [InlineData("example.", "example.extension", "")]
        [InlineData("example.txt", "example.extension", ".txt")]
        [InlineData("example.txt", "example.extension", "txt")]
        public void op_ChangeExtension_FileInfo_string(string expected,
                                                       string name,
                                                       string extension)
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(name);

                var actual = file.ChangeExtension(extension).Name;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_CopyIfDifferent_FileInfo_FileInfo()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt").CreateNew(string.Empty);
                var destination = temp.Info.ToFile("destination.txt");

                var copy = file.CopyIfDifferent(destination);
                Assert.Equal(destination.FullName, copy.FullName);

                Assert.True(destination.Exists);
            }
        }

        [Fact]
        public void op_CopyIfDifferent_FileInfo_FileInfo_whenDifferent()
        {
            using (var temp = new TempDirectory())
            {
                const string expected = "changed";

                var file = temp.Info.ToFile("example.txt").CreateNew(expected);
                var destination = temp.Info.ToFile("destination.txt").CreateNew(string.Empty);

                var copy = file.CopyIfDifferent(destination);
                Assert.Equal(destination.FullName, copy.FullName);

                var actual = destination.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_CopyIfDifferent_FileInfo_FileInfo_whenSame()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt").CreateNew(string.Empty);
                var destination = temp.Info.ToFile("destination.txt").CreateNew(string.Empty);

                var copy = file.CopyIfDifferent(destination);
                Assert.Equal(destination.FullName, copy.FullName);

                Assert.True(destination.Exists);
            }
        }

        [Fact]
        public void op_CopyTo_FileInfoNull_FileInfo()
        {
            using (var temp = new TempDirectory())
            {
                var destination = temp.Info.ToFile("destination.txt");

                Assert.Throws<ArgumentNullException>(() => (null as FileInfo).CopyTo(destination));
            }
        }

        [Fact]
        public void op_CopyTo_FileInfoNull_FileInfo_bool()
        {
            using (var temp = new TempDirectory())
            {
                var destination = temp.Info.ToFile("destination.txt");

                Assert.Throws<ArgumentNullException>(() => (null as FileInfo).CopyTo(destination, true));
            }
        }

        [Fact]
        public void op_CopyTo_FileInfo_FileInfo()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt").CreateNew(string.Empty);
                var destination = temp.Info.ToFile("destination.txt");

                var copy = file.CopyTo(destination);
                Assert.Equal(destination.FullName, copy.FullName);

                Assert.True(destination.Exists);
            }
        }

        [Fact]
        public void op_CopyTo_FileInfo_FileInfoNull()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt");

                // ReSharper disable AssignNullToNotNullAttribute
                Assert.Throws<ArgumentNullException>(() => file.CopyTo(null));

                // ReSharper restore AssignNullToNotNullAttribute
            }
        }

        [Fact]
        public void op_CopyTo_FileInfo_FileInfoNull_bool()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt");

                // ReSharper disable AssignNullToNotNullAttribute
                Assert.Throws<ArgumentNullException>(() => file.CopyTo(null, true));

                // ReSharper restore AssignNullToNotNullAttribute
            }
        }

        [Fact]
        public void op_CopyTo_FileInfo_FileInfo_boolTrue()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt").CreateNew(string.Empty);
                var destination = temp.Info.ToFile("destination.txt");

                var copy = file.CopyTo(destination, true);
                Assert.Equal(destination.FullName, copy.FullName);

                Assert.True(destination.Exists);
            }
        }

        [Fact]
        public void op_CopyTo_FileInfo_FileInfo_bool_whenDestinationExists()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt").CreateNew(string.Empty);
                var destination = temp.Info.ToFile("destination.txt").CreateNew(string.Empty);

                Assert.Throws<IOException>(() => file.CopyTo(destination, false));
            }
        }

        [Fact]
        public void op_CopyTo_FileInfo_FileInfo_bool_whenFileMissing()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt");
                var destination = temp.Info.ToFile("destination.txt");

                Assert.Throws<FileNotFoundException>(() => file.CopyTo(destination, false));
            }
        }

        [Fact]
        public void op_CopyTo_FileInfo_FileInfo_whenDestinationExists()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt").CreateNew(string.Empty);
                var destination = temp.Info.ToFile("destination.txt").CreateNew(string.Empty);

                Assert.Throws<IOException>(() => file.CopyTo(destination));
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
        public void op_CreateNew_FileInfo_object()
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
        public void op_CreateNew_FileInfo_objectNull()
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
        public void op_CreateNew_FileInfo_object_whenExists()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                Assert.Same(file, file.CreateNew(string.Empty));

                Assert.Throws<IOException>(() => file.CreateNew("example"));
            }
        }

        [Fact]
        public void op_CreateNew_FileInfo_object_whenStringEmpty()
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
        public void op_DeduplicateLines_FileInfo()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp
                    .Info
                    .ToFile(Guid.NewGuid())
                    .AppendLine("example")
                    .AppendLine("test")
                    .AppendLine("example")
                    .AppendLine("test")
                    .AppendLine("example");

                Assert.Same(file, file.DeduplicateLines());

                const int expected = 2;
                var actual = file.LineCount();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_DeduplicateLines_FileInfoEmpty()
        {
            using (var temp = new TempDirectory())
            {
                const int expected = 0;
                var file = temp
                    .Info
                    .ToFile(Guid.NewGuid())
                    .CreateNew()
                    .DeduplicateLines();

                var actual = file.LineCount();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_DeduplicateLines_FileInfoMissing()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());

                Assert.Throws<FileNotFoundException>(() => file.DeduplicateLines());
            }
        }

        [Fact]
        public void op_DeduplicateLines_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).DeduplicateLines());
        }

        [Theory]
        [InlineData(false, "", "")]
        [InlineData(false, "example", "example")]
        [InlineData(false, "\r\n", "\r\n")]
        [InlineData(false, "example\r\n", "example\r\n")]
        [InlineData(true, "\r\n", "\n")]
        [InlineData(true, "example\r\n", "example\n")]
        [InlineData(true, "\r\n\r\n", "\n\n")]
        [InlineData(true, "example\r\n\r\n", "example\n\n")]
        public void op_FixNewLine_FileInfo(bool result,
                                           string expected,
                                           string value)
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt").Append(value);

                Assert.Equal(result, file.FixNewLine());

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_FixNewLine_FileInfoMissing()
        {
            using (var temp = new TempDirectory())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<FileNotFoundException>(() => temp.Info.ToFile("missing.txt").FixNewLine());

                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void op_FixNewLine_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).FixNewLine());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void op_LineCount_FileInfo(int expected)
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                for (var i = 0; i < expected; i++)
                {
                    file.AppendLine(i);
                }

                var actual = file.LineCount();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_LineCount_FileInfoEmpty()
        {
            using (var temp = new TempDirectory())
            {
                const int expected = 0;
                var file = temp.Info.ToFile(Guid.NewGuid()).CreateNew();

                var actual = file.LineCount();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_LineCount_FileInfoMissing()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());

                Assert.Throws<FileNotFoundException>(() => file.LineCount());
            }
        }

        [Fact]
        public void op_LineCount_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).LineCount());
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
        public void op_MoveTo_FileInfoNull_FileInfo()
        {
            using (var temp = new TempDirectory())
            {
                var destination = temp.Info.ToFile("destination.txt");

                Assert.Throws<ArgumentNullException>(() => (null as FileInfo).MoveTo(destination));
            }
        }

        [Fact]
        public void op_MoveTo_FileInfoNull_FileInfo_bool()
        {
            using (var temp = new TempDirectory())
            {
                var destination = temp.Info.ToFile("destination.txt");

                Assert.Throws<ArgumentNullException>(() => (null as FileInfo).MoveTo(destination, true));
            }
        }

        [Fact]
        public void op_MoveTo_FileInfo_FileInfo()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt").CreateNew(string.Empty);
                var destination = temp.Info.ToFile("destination.txt");

                var move = file.MoveTo(destination);
                Assert.Equal(destination.FullName, move.FullName);

                Assert.False(temp.Info.ToFile("example.txt").Exists);
                Assert.True(destination.Exists);
            }
        }

        [Fact]
        public void op_MoveTo_FileInfo_FileInfoNull()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt");

                // ReSharper disable AssignNullToNotNullAttribute
                Assert.Throws<ArgumentNullException>(() => file.MoveTo(null));

                // ReSharper restore AssignNullToNotNullAttribute
            }
        }

        [Fact]
        public void op_MoveTo_FileInfo_FileInfoNull_bool()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt");

                // ReSharper disable AssignNullToNotNullAttribute
                Assert.Throws<ArgumentNullException>(() => file.MoveTo(null, true));

                // ReSharper restore AssignNullToNotNullAttribute
            }
        }

        [Fact]
        public void op_MoveTo_FileInfo_FileInfo_boolTrue()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt").CreateNew(string.Empty);
                var destination = temp.Info.ToFile("destination.txt");

                var move = file.MoveTo(destination, true);
                Assert.Equal(destination.FullName, move.FullName);

                Assert.False(temp.Info.ToFile("example.txt").Exists);
                Assert.True(destination.Exists);
            }
        }

        [Fact]
        public void op_MoveTo_FileInfo_FileInfo_bool_whenDestinationExists()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt").CreateNew(string.Empty);
                var destination = temp.Info.ToFile("destination.txt").CreateNew(string.Empty);

                Assert.Throws<IOException>(() => file.MoveTo(destination, false));
            }
        }

        [Fact]
        public void op_MoveTo_FileInfo_FileInfo_bool_whenFileMissing()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt");
                var destination = temp.Info.ToFile("destination.txt");

                Assert.Throws<FileNotFoundException>(() => file.MoveTo(destination, false));
            }
        }

        [Fact]
        public void op_MoveTo_FileInfo_FileInfo_whenDestinationExists()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt").CreateNew(string.Empty);
                var destination = temp.Info.ToFile("destination.txt").CreateNew(string.Empty);

                Assert.Throws<IOException>(() => file.MoveTo(destination));
            }
        }

        [Theory]
        [InlineData(true, "2013-12.txt")]
        [InlineData(false, "example.txt")]
        public void op_NameIsMonth_FileInfo(bool expected,
                                            string name)
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(name);

                var actual = file.NameIsMonth();

                Assert.Equal(expected, actual);
            }
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

        [Theory]
        [InlineData("example", "example")]
        [InlineData("example", "example.txt")]
        public void op_RemoveExtension_FileInfo(string expected,
                                                string name)
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(name);

                var actual = file.RemoveExtension().Name;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_RemoveExtension_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).RemoveExtension());
        }

        [Fact]
        public void op_SetDate_FileInfo()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example").Create("test");

                var expected = DateTime.UtcNow.AddYears(-1);

                file.SetDate(expected);

                Assert.Equal(expected, file.CreationTimeUtc);
                Assert.Equal(expected, file.LastAccessTimeUtc);
                Assert.Equal(expected, file.LastWriteTimeUtc);
            }
        }

        [Fact]
        public void op_SetDate_FileInfoNotFound()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example");

                Assert.Throws<FileNotFoundException>(() => file.SetDate(DateTime.UtcNow));
            }
        }

        [Fact]
        public void op_SetDate_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).SetDate(DateTime.UtcNow));
        }

        [Fact]
        public void op_ToParent_FileInfo()
        {
            using (var temp = new TempDirectory())
            {
                var expected = temp.Info.FullName;
                var actual = temp.Info.ToFile("example.txt").ToParent().FullName;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_ToParent_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).ToParent());
        }

        [Fact]
        public void op_ToReadStream_FileInfo()
        {
            const string expected = "expected";

            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt").CreateNew(expected);

                using (var stream = file.ToReadStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var actual = reader.ReadToEnd();

                        Assert.Equal(expected, actual);
                    }
                }
            }
        }

        [Fact]
        public void op_ToWriteStream_FileInfo_FileModeAppend()
        {
            const string expected = "append expected";

            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt").CreateNew("append");

                using (var stream = file.ToWriteStream(FileMode.Append))
                {
                    using (var reader = new StreamWriter(stream))
                    {
                        reader.Write(" expected");
                    }
                }

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_ToWriteStream_FileInfo_FileModeCreate()
        {
            const string expected = "created";

            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt").CreateNew("deleted");

                using (var stream = file.ToWriteStream(FileMode.Create))
                {
                    using (var reader = new StreamWriter(stream))
                    {
                        reader.Write(expected);
                    }
                }

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "This naming is intentional.")]
        public void op_ToWriteStream_FileInfo_FileModeCreateNew()
        {
            const string expected = "created";

            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt");

                using (var stream = file.ToWriteStream(FileMode.CreateNew))
                {
                    using (var reader = new StreamWriter(stream))
                    {
                        reader.Write(expected);
                    }
                }

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_ToWriteStream_FileInfo_FileModeOpen()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt");

                Assert.Throws<ArgumentOutOfRangeException>(() => file.ToWriteStream(FileMode.Open));
            }
        }

        [Fact]
        public void op_ToWriteStream_FileInfo_FileModeOpenOrCreate()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt");

                Assert.Throws<ArgumentOutOfRangeException>(() => file.ToWriteStream(FileMode.OpenOrCreate));
            }
        }

        [Fact]
        public void op_ToWriteStream_FileInfo_FileModeTruncate()
        {
            const string expected = "expected";

            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile("example.txt").CreateNew("truncate");

                using (var stream = file.ToWriteStream(FileMode.Truncate))
                {
                    using (var reader = new StreamWriter(stream))
                    {
                        reader.Write(expected);
                    }
                }

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Truncate_FileInfoNull_IXPathNavigable()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<example />");

            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).Truncate(xml));
        }

        [Fact]
        public void op_Truncate_FileInfoNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => (null as FileInfo).Truncate("example"));
        }

        [Fact]
        public void op_Truncate_FileInfo_IXPathNavigable()
        {
            const string expected = "<example />";
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());
                var xml = new XmlDocument();
                xml.LoadXml(expected);

                Assert.Same(file, file.CreateNew(string.Empty));
                Assert.Same(file, file.Truncate(xml));

                var actual = file.ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Truncate_FileInfo_IXPathNavigableNull()
        {
            using (var temp = new TempDirectory())
            {
                var file = temp.Info.ToFile(Guid.NewGuid());

                Assert.Throws<ArgumentNullException>(() => file.Truncate(null as IXPathNavigable));
            }
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
                Assert.Same(file, file.Truncate(null as string));

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