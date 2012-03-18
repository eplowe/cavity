namespace Cavity.IO
{
    using System;
    using System.IO;

    using Xunit;
    using Xunit.Extensions;

    public sealed class DirectoryInfoExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(DirectoryInfoExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_CopyTo_DirectoryInfoMissing_DirectoryInfo_boolTrue_string()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToDirectory("source").ToDirectory("test");
                var destination = temp.Info.ToDirectory("destination").ToDirectory("test");

                Assert.Throws<DirectoryNotFoundException>(() => source.CopyTo(destination, true, "*.txt"));
            }
        }

        [Fact]
        public void op_CopyTo_DirectoryInfoNull_DirectoryInfo_bool()
        {
            using (var temp = new TempDirectory())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).CopyTo(temp.Info, true));

                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void op_CopyTo_DirectoryInfoNull_DirectoryInfo_bool_string()
        {
            using (var temp = new TempDirectory())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).CopyTo(temp.Info, true, "*.txt"));

                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void op_CopyTo_DirectoryInfo_DirectoryInfoMissing_bool()
        {
            using (var temp = new TempDirectory())
            {
                const string expected = "copied";
                var source = temp.Info.ToDirectory("source").ToDirectory("test", true);
                source.ToFile("example.txt").Append(expected);
                var destination = temp.Info.ToDirectory("destination").ToDirectory("test");

                source.CopyTo(destination, false);

                var actual = destination.ToFile("example.txt").ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_CopyTo_DirectoryInfo_DirectoryInfoMissing_bool_string()
        {
            using (var temp = new TempDirectory())
            {
                const string expected = "copied";
                var source = temp.Info.ToDirectory("source").ToDirectory("test", true);
                source.ToFile("example.txt").Append(expected);
                source.ToFile("example.ignore").Append(string.Empty);
                var destination = temp.Info.ToDirectory("destination").ToDirectory("test");

                source.CopyTo(destination, false, "*.txt");

                var actual = destination.ToFile("example.txt").ReadToEnd();

                Assert.Equal(expected, actual);
                Assert.False(destination.ToFile("example.ignore").Exists);
            }
        }

        [Fact]
        public void op_CopyTo_DirectoryInfo_DirectoryInfoNull_bool()
        {
            using (var temp = new TempDirectory())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<ArgumentNullException>(() => temp.Info.CopyTo(null, true));

                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void op_CopyTo_DirectoryInfo_DirectoryInfoNull_bool_string()
        {
            using (var temp = new TempDirectory())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<ArgumentNullException>(() => temp.Info.CopyTo(null, true, "*.txt"));

                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void op_CopyTo_DirectoryInfo_DirectoryInfo_boolFalse()
        {
            using (var temp = new TempDirectory())
            {
                const string expected = "unchanged";
                var source = temp.Info.ToDirectory("source").ToDirectory("test", true);
                source.ToFile("example.txt").Append(string.Empty);
                var destination = temp.Info.ToDirectory("destination").ToDirectory("test", true);
                destination.ToFile("example.txt").Append(expected);

                source.CopyTo(destination, false);

                var actual = destination.ToFile("example.txt").ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_CopyTo_DirectoryInfo_DirectoryInfo_boolFalse_string()
        {
            using (var temp = new TempDirectory())
            {
                const string expected = "unchanged";
                var source = temp.Info.ToDirectory("source").ToDirectory("test", true);
                source.ToFile("example.ignore").Append(string.Empty);
                source.ToFile("example.txt").Append(string.Empty);
                var destination = temp.Info.ToDirectory("destination").ToDirectory("test", true);
                destination.ToFile("example.txt").Append(expected);

                source.CopyTo(destination, false, "*.txt");

                var actual = destination.ToFile("example.txt").ReadToEnd();

                Assert.Equal(expected, actual);
                Assert.False(destination.ToFile("example.ignore").Exists);
            }
        }

        [Fact]
        public void op_CopyTo_DirectoryInfo_DirectoryInfo_boolTrue()
        {
            using (var temp = new TempDirectory())
            {
                const string expected = "replace";
                var source = temp.Info.ToDirectory("source").ToDirectory("test", true);
                source.ToFile("example.txt").Append(expected);
                var destination = temp.Info.ToDirectory("destination").ToDirectory("test", true);
                destination.ToFile("example.txt").Append(string.Empty);

                source.CopyTo(destination, true);

                var actual = destination.ToFile("example.txt").ReadToEnd();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_CopyTo_DirectoryInfo_DirectoryInfo_boolTrue_string()
        {
            using (var temp = new TempDirectory())
            {
                const string expected = "replace";
                var source = temp.Info.ToDirectory("source").ToDirectory("test", true);
                source.ToFile("example.ignore").Append(string.Empty);
                source.ToFile("example.txt").Append(expected);
                var destination = temp.Info.ToDirectory("destination").ToDirectory("test", true);
                destination.ToFile("example.txt").Append(string.Empty);

                source.CopyTo(destination, true, "*.txt");

                var actual = destination.ToFile("example.txt").ReadToEnd();

                Assert.Equal(expected, actual);
                Assert.False(destination.ToFile("example.ignore").Exists);
            }
        }

        [Fact]
        public void op_CopyTo_DirectoryInfo_DirectoryInfo_bool_stringEmpty()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToDirectory("source", true);
                var destination = temp.Info.ToDirectory("destination", true);

                Assert.Throws<ArgumentOutOfRangeException>(() => source.CopyTo(destination, true, string.Empty));
            }
        }

        [Fact]
        public void op_CopyTo_DirectoryInfo_DirectoryInfo_bool_stringNull()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToDirectory("source", true);
                var destination = temp.Info.ToDirectory("destination", true);

                Assert.Throws<ArgumentNullException>(() => source.CopyTo(destination, true, null));
            }
        }

        [Theory]
        [InlineData(0, 0, 0, SearchOption.AllDirectories)]
        [InlineData(1, 1, 1, SearchOption.AllDirectories)]
        [InlineData(2, 2, 1, SearchOption.AllDirectories)]
        [InlineData(9, 3, 3, SearchOption.AllDirectories)]
        public void op_LineCount_DirectoryInfo_string_SearchOption(int expected,
                                                                   int files,
                                                                   int lines,
                                                                   SearchOption searchOption)
        {
            using (var temp = new TempDirectory())
            {
                var child = temp.Info.ToDirectory("child", true);
                for (var i = 0; i < files; i++)
                {
                    var file = child.ToFile("{0}.txt".FormatWith(i));
                    file.CreateNew();
                    for (var j = 0; j < lines; j++)
                    {
                        file.AppendLine(string.Empty);
                    }
                }

                var actual = temp.Info.LineCount("*.txt", searchOption);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_LineCount_DirectoryInfo_string_SearchOption_throwsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).LineCount("*.*", SearchOption.AllDirectories));
        }

        [Fact]
        public void op_LineCount_DirectoryInfo_string_SearchOption_throwsFileNotFoundException()
        {
            using (var temp = new TempDirectory())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<DirectoryNotFoundException>(() => temp.Info.ToDirectory("example").LineCount("*.*", SearchOption.AllDirectories));

                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void op_Make_DirectoryInfo()
        {
            using (var temp = new TempDirectory())
            {
                var actual = temp.Info.ToDirectory("example").Make();

                Assert.True(actual.Exists);
            }
        }

        [Fact]
        public void op_Make_DirectoryInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).Make());
        }

        [Fact]
        public void op_Make_DirectoryInfo_whenDirectoryExists()
        {
            using (var temp = new TempDirectory())
            {
                temp.Info.Make();
            }
        }

        [Fact]
        public void op_MoveTo_DirectoryInfoMissing_DirectoryInfo_boolTrue_string()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToDirectory("source").ToDirectory("test");
                var destination = temp.Info.ToDirectory("destination").ToDirectory("test");

                Assert.Throws<DirectoryNotFoundException>(() => source.MoveTo(destination, true, "*.txt"));
            }
        }

        [Fact]
        public void op_MoveTo_DirectoryInfoNull_DirectoryInfo_bool()
        {
            using (var temp = new TempDirectory())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).MoveTo(temp.Info, true));

                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void op_MoveTo_DirectoryInfoNull_DirectoryInfo_bool_string()
        {
            using (var temp = new TempDirectory())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).MoveTo(temp.Info, true, "*.txt"));

                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void op_MoveTo_DirectoryInfo_DirectoryInfoMissing_bool()
        {
            using (var temp = new TempDirectory())
            {
                const string expected = "moved";
                var source = temp.Info.ToDirectory("source").ToDirectory("test", true);
                source.ToFile("example.txt").Append(expected);
                var destination = temp.Info.ToDirectory("destination").ToDirectory("test");

                source.MoveTo(destination, false);

                var actual = destination.ToFile("example.txt").ReadToEnd();

                Assert.Equal(expected, actual);
                Assert.False(source.ToFile("example.txt").Exists);
            }
        }

        [Fact]
        public void op_MoveTo_DirectoryInfo_DirectoryInfoMissing_bool_string()
        {
            using (var temp = new TempDirectory())
            {
                const string expected = "moved";
                var source = temp.Info.ToDirectory("source").ToDirectory("test", true);
                source.ToFile("example.txt").Append(expected);
                source.ToFile("example.ignore").Append(string.Empty);
                var destination = temp.Info.ToDirectory("destination").ToDirectory("test");

                source.MoveTo(destination, false, "*.txt");

                var actual = destination.ToFile("example.txt").ReadToEnd();

                Assert.Equal(expected, actual);
                Assert.False(source.ToFile("example.txt").Exists);
                Assert.False(destination.ToFile("example.ignore").Exists);
            }
        }

        [Fact]
        public void op_MoveTo_DirectoryInfo_DirectoryInfoNull_bool()
        {
            using (var temp = new TempDirectory())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<ArgumentNullException>(() => temp.Info.MoveTo(null, true));

                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void op_MoveTo_DirectoryInfo_DirectoryInfoNull_bool_string()
        {
            using (var temp = new TempDirectory())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<ArgumentNullException>(() => temp.Info.MoveTo(null, true, "*.txt"));

                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void op_MoveTo_DirectoryInfo_DirectoryInfo_boolFalse()
        {
            using (var temp = new TempDirectory())
            {
                const string expected = "unchanged";
                var source = temp.Info.ToDirectory("source").ToDirectory("test", true);
                source.ToFile("example.txt").Append(string.Empty);
                var destination = temp.Info.ToDirectory("destination").ToDirectory("test", true);
                destination.ToFile("example.txt").Append(expected);

                source.MoveTo(destination, false);

                var actual = destination.ToFile("example.txt").ReadToEnd();

                Assert.Equal(expected, actual);
                Assert.True(source.ToFile("example.txt").Exists);
            }
        }

        [Fact]
        public void op_MoveTo_DirectoryInfo_DirectoryInfo_boolFalse_string()
        {
            using (var temp = new TempDirectory())
            {
                const string expected = "unchanged";
                var source = temp.Info.ToDirectory("source").ToDirectory("test", true);
                source.ToFile("example.ignore").Append(string.Empty);
                source.ToFile("example.txt").Append(string.Empty);
                var destination = temp.Info.ToDirectory("destination").ToDirectory("test", true);
                destination.ToFile("example.txt").Append(expected);

                source.MoveTo(destination, false, "*.txt");

                var actual = destination.ToFile("example.txt").ReadToEnd();

                Assert.Equal(expected, actual);
                Assert.True(source.ToFile("example.txt").Exists);
                Assert.False(destination.ToFile("example.ignore").Exists);
            }
        }

        [Fact]
        public void op_MoveTo_DirectoryInfo_DirectoryInfo_boolTrue()
        {
            using (var temp = new TempDirectory())
            {
                const string expected = "replace";
                var source = temp.Info.ToDirectory("source").ToDirectory("test", true);
                source.ToFile("example.txt").Append(expected);
                var destination = temp.Info.ToDirectory("destination").ToDirectory("test", true);
                destination.ToFile("example.txt").Append(string.Empty);

                source.MoveTo(destination, true);

                var actual = destination.ToFile("example.txt").ReadToEnd();

                Assert.Equal(expected, actual);
                Assert.False(source.ToFile("example.txt").Exists);
            }
        }

        [Fact]
        public void op_MoveTo_DirectoryInfo_DirectoryInfo_boolTrue_string()
        {
            using (var temp = new TempDirectory())
            {
                const string expected = "replace";
                var source = temp.Info.ToDirectory("source").ToDirectory("test", true);
                source.ToFile("example.ignore").Append(string.Empty);
                source.ToFile("example.txt").Append(expected);
                var destination = temp.Info.ToDirectory("destination").ToDirectory("test", true);
                destination.ToFile("example.txt").Append(string.Empty);

                source.MoveTo(destination, true, "*.txt");

                var actual = destination.ToFile("example.txt").ReadToEnd();

                Assert.Equal(expected, actual);
                Assert.False(source.ToFile("example.txt").Exists);
                Assert.False(destination.ToFile("example.ignore").Exists);
            }
        }

        [Fact]
        public void op_MoveTo_DirectoryInfo_DirectoryInfo_bool_stringEmpty()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToDirectory("source", true);
                var destination = temp.Info.ToDirectory("destination", true);

                Assert.Throws<ArgumentOutOfRangeException>(() => source.MoveTo(destination, true, string.Empty));
            }
        }

        [Fact]
        public void op_MoveTo_DirectoryInfo_DirectoryInfo_bool_stringNull()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToDirectory("source", true);
                var destination = temp.Info.ToDirectory("destination", true);

                Assert.Throws<ArgumentNullException>(() => source.MoveTo(destination, true, null));
            }
        }

        [Fact]
        public void op_ToDirectory_DirectoryInfoNull_object()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).ToDirectory("example"));
        }

        [Fact]
        public void op_ToDirectory_DirectoryInfo_object()
        {
            using (var temp = new TempDirectory())
            {
                const string name = "example";

                var expected = Path.Combine(temp.Info.FullName, name);

                var actual = temp.Info.ToDirectory(name);

                Assert.False(actual.Exists);
                Assert.Equal(expected, actual.FullName);
            }
        }

        [Fact]
        public void op_ToDirectory_DirectoryInfo_objectNull()
        {
            using (var temp = new TempDirectory())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<ArgumentNullException>(() => temp.Info.ToDirectory(null));

                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void op_ToDirectory_DirectoryInfo_object_bool()
        {
            using (var temp = new TempDirectory())
            {
                const string name = "example";

                var expected = Path.Combine(temp.Info.FullName, name);

                var actual = temp.Info.ToDirectory(name, true);

                Assert.True(actual.Exists);
                Assert.Equal(expected, actual.FullName);
            }
        }

        [Fact]
        public void op_ToDirectory_DirectoryInfo_object_whenInvalidCharacters()
        {
            using (var temp = new TempDirectory())
            {
                foreach (var c in new[]
                                      {
                                          "\\", "/", ":", "*", "?", "\"", "<", ">", "|", "\n", "\t"
                                      })
                {
                    var name = "invalid {0}example".FormatWith(c);

                    var expected = Path.Combine(temp.Info.FullName, "invalid example");

                    var actual = temp.Info.ToDirectory(name);

                    Assert.False(actual.Exists);
                    Assert.Equal(expected, actual.FullName);
                }
            }
        }

        [Fact]
        public void op_ToFile_DirectoryInfoNull_object()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).ToFile("example.txt"));
        }

        [Fact]
        public void op_ToFile_DirectoryInfo_object()
        {
            using (var temp = new TempDirectory())
            {
                const string name = "example.txt";

                var expected = Path.Combine(temp.Info.FullName, name);
                var actual = temp.Info.ToFile(name).FullName;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_ToFile_DirectoryInfo_objectNull()
        {
            using (var temp = new TempDirectory())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<ArgumentNullException>(() => temp.Info.ToFile(null));

                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void op_ToFile_DirectoryInfo_object_whenInvalidCharacters()
        {
            using (var temp = new TempDirectory())
            {
                foreach (var c in new[]
                                      {
                                          "\\", "/", ":", "*", "?", "\"", "<", ">", "|"
                                      })
                {
                    var name = "invalid {0}example.txt".FormatWith(c);

                    var expected = Path.Combine(temp.Info.FullName, "invalid example.txt");
                    var actual = temp.Info.ToFile(name).FullName;

                    Assert.Equal(expected, actual);
                }
            }
        }
    }
}