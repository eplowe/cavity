namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
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
        public void op_CopyTo_DirectoryInfo_DirectoryInfoNestedMissing_bool()
        {
            using (var temp = new TempDirectory())
            {
                const string expected = "copied";
                var source = temp.Info.ToDirectory("source").ToDirectory("parent").ToDirectory("child", true);
                source.ToFile("example.txt").Append(expected);
                var destination = temp.Info.ToDirectory("destination").ToDirectory("parent").ToDirectory("child");

                source.CopyTo(destination, false);

                var actual = destination.ToFile("example.txt").ReadToEnd();

                Assert.Equal(expected, actual);
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

        [Fact]
        public void op_CsvFiles_DirectoryInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).CsvFiles().First());
        }

        [Fact]
        public void op_CsvFiles_DirectoryInfoNull_SearchOption()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).CsvFiles(SearchOption.TopDirectoryOnly).First());
        }

        [Fact]
        public void op_CsvFiles_DirectoryInfo_SearchOption()
        {
            using (var temp = new TempDirectory())
            {
                var expected = temp.Info
                                   .ToDirectory("a", true)
                                   .ToFile("example.csv")
                                   .Create(string.Empty)
                                   .FullName;

                var actual = temp.Info.CsvFiles(SearchOption.AllDirectories).First().FullName;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_CsvFiles_DirectoryInfo()
        {
            using (var temp = new TempDirectory())
            {
                var expected = temp.Info
                                   .ToFile("example.csv")
                                   .Create(string.Empty)
                                   .FullName;

                var actual = temp.Info.CsvFiles().First().FullName;

                Assert.Equal(expected, actual);
            }
        }

#if NET20 || NET35
#else
        [Fact]
        public void op_CsvFiles_DirectoryInfoNull_FuncSelector()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).CsvFiles(file => file.Name).First());
        }

        [Fact]
        public void op_CsvFiles_DirectoryInfo_FuncSelector()
        {
            using (var temp = new TempDirectory())
            {
                temp.Info.ToFile("a.csv").Create(string.Empty);
                var expected = temp.Info
                                   .ToFile("z.csv")
                                   .Create(string.Empty)
                                   .FullName;

                var actual = temp.Info.CsvFiles(file => file.Name).Last().FullName;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_CsvFiles_DirectoryInfoNull_FuncSelector_SearchOption()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).CsvFiles(file => file.Name, SearchOption.TopDirectoryOnly).First());
        }

        [Fact]
        public void op_CsvFiles_DirectoryInfo_FuncSelector_SearchOption()
        {
            using (var temp = new TempDirectory())
            {
                temp.Info.ToFile("x.csv").Create(string.Empty);
                var expected = temp.Info
                                   .ToDirectory("example", true)
                                   .ToFile("z.csv")
                                   .Create(string.Empty)
                                   .FullName;

                var actual = temp.Info.CsvFiles(file => file.Name, SearchOption.AllDirectories).Last().FullName;

                Assert.Equal(expected, actual);
            }
        }
#endif

#if NET20 || NET35
#else
        [Fact]
        public void op_EnumerateDirectories_DirectoryInfoNull_FuncDirectoryInfoBool()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).EnumerateDirectories(x => x.Name == "b").First());
        }

        [Fact]
        public void op_EnumerateDirectories_DirectoryInfoNull_SearchOption_FuncDirectoryInfoBool()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).EnumerateDirectories(SearchOption.TopDirectoryOnly, x => x.Name == "b").First());
        }

        [Fact]
        public void op_EnumerateDirectories_DirectoryInfoNull_string_SearchOption_FuncDirectoryInfoBool()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).EnumerateDirectories("*", SearchOption.TopDirectoryOnly, x => x.Name == "b").First());
        }

        [Fact]
        public void op_EnumerateDirectories_DirectoryInfo_FuncDirectoryInfoBool()
        {
            using (var temp = new TempDirectory())
            {
                temp.Info.ToDirectory("a", true);
                var expected = temp.Info.ToDirectory("b", true).FullName;
                temp.Info.ToDirectory("c", true);

                var actual = temp.Info.EnumerateDirectories(x => x.Name == "b").First().FullName;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_EnumerateDirectories_DirectoryInfo_SearchOption_FuncDirectoryInfoBool()
        {
            using (var temp = new TempDirectory())
            {
                var expected = temp.Info.ToDirectory("a").ToDirectory("b", true).FullName;
                temp.Info.ToDirectory("c", true);

                var actual = temp.Info.EnumerateDirectories(SearchOption.AllDirectories, x => x.Name == "b").First().FullName;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_EnumerateDirectories_DirectoryInfo_string_SearchOption_FuncDirectoryInfoBool()
        {
            using (var temp = new TempDirectory())
            {
                var expected = temp.Info.ToDirectory("a").ToDirectory("b", true).FullName;
                temp.Info.ToDirectory("c", true);

                var actual = temp.Info.EnumerateDirectories("*", SearchOption.AllDirectories, x => x.Name == "b").First().FullName;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_EnumerateFiles_DirectoryInfoNull_FuncFileInfoBool()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).EnumerateFiles(x => x.Name == "b").First());
        }

        [Fact]
        public void op_EnumerateFiles_DirectoryInfoNull_SearchOption_FuncFileInfoBool()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).EnumerateFiles(SearchOption.TopDirectoryOnly, x => x.Name == "b").First());
        }

        [Fact]
        public void op_EnumerateFiles_DirectoryInfoNull_string_SearchOption_FuncFileInfoBool()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).EnumerateFiles("*", SearchOption.TopDirectoryOnly, x => x.Name == "b").First());
        }

        [Fact]
        public void op_EnumerateFiles_DirectoryInfo_FuncFileInfoBool()
        {
            using (var temp = new TempDirectory())
            {
                temp.Info.ToDirectory("a", true);
                var expected = temp.Info.ToFile("b").CreateNew(string.Empty).FullName;
                temp.Info.ToDirectory("c", true);

                var actual = temp.Info.EnumerateFiles(x => x.Name == "b").First().FullName;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_EnumerateFiles_DirectoryInfo_SearchOption_FuncFileInfoBool()
        {
            using (var temp = new TempDirectory())
            {
                var expected = temp.Info.ToDirectory("a", true).ToFile("abc.txt").CreateNew(string.Empty).FullName;
                temp.Info.ToDirectory("a").ToFile("abcdef.txt").CreateNew(string.Empty);
                temp.Info.ToDirectory("c", true);

                var actual = temp.Info.EnumerateFiles(SearchOption.AllDirectories, x => x.Name.Length == 7).First().FullName;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_EnumerateFiles_DirectoryInfo_string_SearchOption_FuncFileInfoBool()
        {
            using (var temp = new TempDirectory())
            {
                var expected = temp.Info.ToDirectory("a", true).ToFile("abc.txt").CreateNew(string.Empty).FullName;
                temp.Info.ToDirectory("a").ToFile("abcdef.txt").CreateNew(string.Empty);
                temp.Info.ToDirectory("c", true);

                var actual = temp.Info.EnumerateFiles("*.txt", SearchOption.AllDirectories, x => x.Name.Length == 7).First().FullName;

                Assert.Equal(expected, actual);
            }
        }
#endif

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
        public void op_RobocopyTo_DirectoryInfoNull_DirectoryInfo()
        {
            using (var temp = new TempDirectory())
            {
                var destination = temp.Info.ToDirectory("destination").Make();

                Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).RobocopyTo(destination));
            }
        }

        [Fact]
        public void op_RobocopyTo_DirectoryInfoNotFound_DirectoryInfo()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToDirectory("source");
                var destination = temp.Info.ToDirectory("destination").Make();

                Assert.Throws<DirectoryNotFoundException>(() => source.RobocopyTo(destination));
            }
        }

        [Fact]
        public void op_RobocopyTo_DirectoryInfo_DirectoryInfoNull()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToDirectory("source").Make();

                Assert.Throws<ArgumentNullException>(() => source.RobocopyTo(null));
            }
        }

        [Fact]
        public void op_RobocopyTo_DirectoryInfo_DirectoryInfoNotFound()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToDirectory("source").Make();
                source.ToFile("example").Create("test");

                var destination = temp.Info.ToDirectory("destination");

                source.RobocopyTo(destination);

                Assert.True(source.ToFile("example").Exists);
                Assert.True(destination.ToFile("example").Exists);
            }
        }

        [Fact]
        public void op_RobocopyTo_DirectoryInfo_DirectoryInfo()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToDirectory("source").Make();
                source.ToFile("example").Create("test");

                var destination = temp.Info.ToDirectory("destination").Make();

                source.RobocopyTo(destination);

                Assert.True(source.ToFile("example").Exists);
                Assert.True(destination.ToFile("example").Exists);
            }
        }

        [Fact]
        public void op_RobocopyTo_DirectoryInfoNull_DirectoryInfo_boolFalse()
        {
            using (var temp = new TempDirectory())
            {
                var destination = temp.Info.ToDirectory("destination").Make();

                Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).RobocopyTo(destination, false));
            }
        }

        [Fact]
        public void op_RobocopyTo_DirectoryInfoNotFound_DirectoryInfo_boolFalse()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToDirectory("source");
                var destination = temp.Info.ToDirectory("destination").Make();

                Assert.Throws<DirectoryNotFoundException>(() => source.RobocopyTo(destination, false));
            }
        }

        [Fact]
        public void op_RobocopyTo_DirectoryInfo_DirectoryInfoNull_boolFalse()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToDirectory("source").Make();

                Assert.Throws<ArgumentNullException>(() => source.RobocopyTo(null, false));
            }
        }

        [Fact]
        public void op_RobocopyTo_DirectoryInfo_DirectoryInfoNotFound_boolFalse()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToDirectory("source").Make();
                source.ToFile("example").Create("test");

                var destination = temp.Info.ToDirectory("destination");

                source.RobocopyTo(destination, false);

                Assert.True(source.ToFile("example").Exists);
                Assert.True(destination.ToFile("example").Exists);
            }
        }

        [Fact]
        public void op_RobocopyTo_DirectoryInfo_DirectoryInfo_boolFalse()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToDirectory("source").Make();
                source.ToFile("example").Create("test");

                var destination = temp.Info.ToDirectory("destination").Make();

                source.RobocopyTo(destination, false);

                Assert.True(source.ToFile("example").Exists);
                Assert.True(destination.ToFile("example").Exists);
            }
        }

        [Fact]
        public void op_RobocopyTo_DirectoryInfoNull_DirectoryInfo_boolTrue()
        {
            using (var temp = new TempDirectory())
            {
                var destination = temp.Info.ToDirectory("destination").Make();

                Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).RobocopyTo(destination, true));
            }
        }

        [Fact]
        public void op_RobocopyTo_DirectoryInfoNotFound_DirectoryInfo_boolTrue()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToDirectory("source");
                var destination = temp.Info.ToDirectory("destination").Make();

                Assert.Throws<DirectoryNotFoundException>(() => source.RobocopyTo(destination, true));
            }
        }

        [Fact]
        public void op_RobocopyTo_DirectoryInfo_DirectoryInfoNull_boolTrue()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToDirectory("source").Make();

                Assert.Throws<ArgumentNullException>(() => source.RobocopyTo(null, true));
            }
        }

        [Fact]
        public void op_RobocopyTo_DirectoryInfo_DirectoryInfoNotFound_boolTrue()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToDirectory("source").Make();
                source.ToFile("example").Create("test");

                var destination = temp.Info.ToDirectory("destination");

                source.RobocopyTo(destination, true);

                Assert.False(source.ToFile("example").Exists);
                Assert.True(destination.ToFile("example").Exists);
            }
        }

        [Fact]
        public void op_RobocopyTo_DirectoryInfo_DirectoryInfo_boolTrue()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToDirectory("source").Make();
                source.ToFile("example").Create("test");

                var destination = temp.Info.ToDirectory("destination").Make();

                source.RobocopyTo(destination, true);

                Assert.False(source.ToFile("example").Exists);
                Assert.True(destination.ToFile("example").Exists);
            }
        }

        [Fact]
        public void op_SetDate_DirectoryInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).SetDate(DateTime.UtcNow));
        }

        [Fact]
        public void op_SetDate_DirectoryInfoNotFound()
        {
            using (var temp = new TempDirectory())
            {
                var directory = temp.Info.ToDirectory("example");

                Assert.Throws<DirectoryNotFoundException>(() => directory.SetDate(DateTime.UtcNow));
            }
        }

        [Fact]
        public void op_SetDate_DirectoryInfo()
        {
            using (var temp = new TempDirectory())
            {
                var directory = temp.Info.ToDirectory("example").Make();

                var expected = DateTime.UtcNow.AddYears(-1);

                directory.SetDate(expected);

                Assert.Equal(expected, directory.CreationTimeUtc);
                Assert.Equal(expected, directory.LastAccessTimeUtc);
                Assert.Equal(expected, directory.LastWriteTimeUtc);
            }
        }

        [Fact]
        public void op_TextFiles_DirectoryInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).TextFiles().First());
        }

        [Fact]
        public void op_TextFiles_DirectoryInfoNull_SearchOption()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).TextFiles(SearchOption.TopDirectoryOnly).First());
        }

        [Fact]
        public void op_TextFiles_DirectoryInfo_SearchOption()
        {
            using (var temp = new TempDirectory())
            {
                var expected = temp.Info
                                   .ToDirectory("a", true)
                                   .ToFile("example.txt")
                                   .Create(string.Empty)
                                   .FullName;

                var actual = temp.Info.TextFiles(SearchOption.AllDirectories).First().FullName;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_TextFiles_DirectoryInfo()
        {
            using (var temp = new TempDirectory())
            {
                var expected = temp.Info
                                   .ToFile("example.txt")
                                   .Create(string.Empty)
                                   .FullName;

                var actual = temp.Info.TextFiles().First().FullName;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_ToDirectory_DirectoryInfoNull_IEnumerableOfObject()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).ToDirectory(new List<object>()));
        }

        [Fact]
        public void op_ToDirectory_DirectoryInfoNull_object()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).ToDirectory("example"));
        }

        [Fact]
        public void op_ToDirectory_DirectoryInfo_IEnumerableOfObject()
        {
            var list = "a,b,c".Split(',');

            using (var temp = new TempDirectory())
            {
                var expected = temp.Info.FullName;
                foreach (var item in list)
                {
                    expected = Path.Combine(expected, item);
                }

                var actual = temp.Info.ToDirectory(list);

                Assert.True(actual.Exists);
                Assert.Equal(expected, actual.FullName);
            }
        }

        [Fact]
        public void op_ToDirectory_DirectoryInfo_IEnumerableOfObjectNull()
        {
            using (var temp = new TempDirectory())
            {
                // ReSharper disable AccessToDisposedClosure
                // ReSharper disable RedundantCast
                Assert.Throws<ArgumentNullException>(() => temp.Info.ToDirectory(null as IEnumerable<object>));
                // ReSharper restore RedundantCast
                // ReSharper restore AccessToDisposedClosure
            }
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

        [Fact]
        public void op_ToParent_DirectoryInfo()
        {
            using (var temp = new TempDirectory())
            {
                var expected = temp.Info.FullName;
                var actual = temp.Info.ToDirectory("example").ToParent().FullName;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_ToParent_DirectoryInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).ToParent());
        }

        [Fact]
        public void op_XmlFiles_DirectoryInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).XmlFiles().First());
        }

        [Fact]
        public void op_XmlFiles_DirectoryInfoNull_SearchOption()
        {
            Assert.Throws<ArgumentNullException>(() => (null as DirectoryInfo).XmlFiles(SearchOption.TopDirectoryOnly).First());
        }

        [Fact]
        public void op_XmlFiles_DirectoryInfo_SearchOption()
        {
            using (var temp = new TempDirectory())
            {
                var expected = temp.Info
                                   .ToDirectory("a", true)
                                   .ToFile("example.xml")
                                   .Create(string.Empty)
                                   .FullName;

                var actual = temp.Info.XmlFiles(SearchOption.AllDirectories).First().FullName;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_XmlFiles_DirectoryInfo()
        {
            using (var temp = new TempDirectory())
            {
                var expected = temp.Info
                                   .ToFile("example.xml")
                                   .Create(string.Empty)
                                   .FullName;

                var actual = temp.Info.XmlFiles().First().FullName;

                Assert.Equal(expected, actual);
            }
        }
    }
}