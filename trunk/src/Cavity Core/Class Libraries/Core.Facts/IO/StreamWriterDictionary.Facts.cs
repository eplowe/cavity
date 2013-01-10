namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;

    using Xunit;

    public sealed class StreamWriterDictionaryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<StreamWriterDictionary>().DerivesFrom<Dictionary<string, StreamWriter>>()
                                                                      .IsConcreteClass()
                                                                      .IsUnsealed()
                                                                      .HasDefaultConstructor()
                                                                      .Serializable()
                                                                      .Implements<IDisposable>()
                                                                      .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new StreamWriterDictionary());
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            var types = new Type[2];
            types[0] = typeof(SerializationInfo);
            types[1] = typeof(StreamingContext);

            var ctor = typeof(StreamWriterDictionary)
                .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, types, null);

            var args = new object[2];
            args[0] = new SerializationInfo(typeof(StreamWriterDictionary), new FormatterConverter());
            args[1] = new StreamingContext(StreamingContextStates.All);

            Assert.NotNull(ctor.Invoke(BindingFlags.NonPublic, null, args, CultureInfo.InvariantCulture));
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new StreamWriterDictionary("example"));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.NotNull(new StreamWriterDictionary(string.Empty));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.NotNull(new StreamWriterDictionary(null));
        }

        [Fact]
        public void op_Dispose()
        {
            using (var file = new TempFile())
            {
                var obj = new StreamWriterDictionary();

                obj.Dispose();

                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<InvalidOperationException>(() => obj.Item(file.Info));

                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void op_GetObjectData_SerializationInfo_StreamingContext()
        {
            var info = new SerializationInfo(typeof(StreamWriterDictionary), new FormatterConverter());
            var context = new StreamingContext(StreamingContextStates.All);

            new StreamWriterDictionary().GetObjectData(info, context);
        }

        [Fact]
        public void op_Item_FileInfo()
        {
            using (var file = new TempFile())
            {
                using (var obj = new StreamWriterDictionary())
                {
                    obj.Item(file.Info).Write("example");
                }

                file.Info.Refresh();
                Assert.True(file.Info.Exists);
            }
        }

        [Fact]
        public void op_Item_FileInfoNull()
        {
            using (var obj = new StreamWriterDictionary())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<ArgumentNullException>(() => obj.Item(null as FileInfo));

                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void op_Item_FileInfo_string()
        {
            using (var file = new TempFile())
            {
                using (var obj = new StreamWriterDictionary())
                {
                    obj.Item(file.Info, "one").Write("two");
                }

                var expected = "one{0}two".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string()
        {
            using (var file = new TempFile())
            {
                using (var obj = new StreamWriterDictionary())
                {
                    obj.Item(file.Info.FullName).Write("example");
                }

                file.Info.Refresh();
                Assert.True(file.Info.Exists);
            }
        }

        [Fact]
        public void op_Item_string_caseInsensitive()
        {
            using (var temp = new TempDirectory())
            {
                var lower = temp.Info.ToFile("abc");
                var upper = temp.Info.ToFile("ABC");
                using (var obj = new StreamWriterDictionary())
                {
                    obj.Item(lower.FullName).Write("example");
                    obj.Item(upper.FullName).Write("example");
                }

                const int expected = 1;
                var actual = temp.Info.GetFiles().Length;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_stringEmpty()
        {
            using (var obj = new StreamWriterDictionary())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<ArgumentException>(() => obj.Item(string.Empty));

                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void op_Item_stringNull()
        {
            using (var obj = new StreamWriterDictionary())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<ArgumentNullException>(() => obj.Item(null as string));

                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void op_Item_string_FileModeAppend_FileAccessWrite_FileShareWrite()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                using (var obj = new StreamWriterDictionary("one"))
                {
                    obj.Item(file.Info.FullName, FileMode.Append, FileAccess.Write, FileShare.Write).Write("two");
                }

                var expected = "one{0}two".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_FileModeAppend_FileAccessWrite_FileShareWrite_whenCalledAgain()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                using (var obj = new StreamWriterDictionary("one"))
                {
                    obj.Item(file.Info.FullName, FileMode.Append, FileAccess.Write, FileShare.Write).WriteLine("two");
                }

                using (var obj = new StreamWriterDictionary("one"))
                {
                    obj.Item(file.Info.FullName, FileMode.Append, FileAccess.Write, FileShare.Write).Write("three");
                }

                var expected = "one{0}two{0}three".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_FileModeCreateNew_FileAccessReadWrite_FileShareWrite()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                using (var obj = new StreamWriterDictionary("one"))
                {
                    obj.Item(file.Info.FullName, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Write).Write("two");
                }

                var expected = "one{0}two".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_FileModeCreate_FileAccessReadWrite_FileShareWrite()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                using (var obj = new StreamWriterDictionary("one"))
                {
                    obj.Item(file.Info.FullName, FileMode.Create, FileAccess.ReadWrite, FileShare.Write).Write("two");
                }

                var expected = "one{0}two".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_FileModeCreate_FileAccessReadWrite_FileShareWrite_whenCalledAgain()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                using (var obj = new StreamWriterDictionary("one"))
                {
                    obj.Item(file.Info.FullName, FileMode.Create, FileAccess.ReadWrite, FileShare.Write).Write("two");
                }

                using (var obj = new StreamWriterDictionary("one"))
                {
                    obj.Item(file.Info.FullName, FileMode.Create, FileAccess.ReadWrite, FileShare.Write).Write("two");
                }

                var expected = "one{0}two".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_FileModeOpenOrCreate_FileAccessReadWrite_FileShareWrite()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                using (var obj = new StreamWriterDictionary("one"))
                {
                    obj.Item(file.Info.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Write).Write("two");
                }

                var expected = "one{0}two".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_FileModeOpenOrCreate_FileAccessReadWrite_FileShareWrite_whenCalledAgain()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                using (var obj = new StreamWriterDictionary("one"))
                {
                    obj.Item(file.Info.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Write).Write("two");
                }

                using (var obj = new StreamWriterDictionary("one"))
                {
                    obj.Item(file.Info.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Write).Write("two");
                }

                var expected = "one{0}two".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_FileModeTruncate_FileAccessReadWrite_FileShareWrite()
        {
            using (var file = new TempFile())
            {
                using (var obj = new StreamWriterDictionary("one"))
                {
                    obj.Item(file.Info.FullName, FileMode.Truncate, FileAccess.ReadWrite, FileShare.Write).Write("two");
                }

                var expected = "one{0}two".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_FileModeTruncate_FileAccessReadWrite_FileShareWrite_whenCalledAgain()
        {
            using (var file = new TempFile())
            {
                using (var obj = new StreamWriterDictionary("one"))
                {
                    obj.Item(file.Info.FullName, FileMode.Truncate, FileAccess.ReadWrite, FileShare.Write).Write("two");
                }

                using (var obj = new StreamWriterDictionary("one"))
                {
                    obj.Item(file.Info.FullName, FileMode.Truncate, FileAccess.ReadWrite, FileShare.Write).Write("two");
                }

                var expected = "one{0}two".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_string()
        {
            using (var file = new TempFile())
            {
                using (var obj = new StreamWriterDictionary())
                {
                    obj.Item(file.Info.FullName, "one").Write("two");
                }

                var expected = "one{0}two".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_stringEmpty_FileModeCreateNew_FileAccessReadWrite_FileShareWrite()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                using (var obj = new StreamWriterDictionary())
                {
                    obj.Item(file.Info.FullName, string.Empty, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Write);
                }

                var expected = Environment.NewLine;
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_stringEmpty_FileModeCreate_FileAccessReadWrite_FileShareWrite()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                using (var obj = new StreamWriterDictionary())
                {
                    obj.Item(file.Info.FullName, string.Empty, FileMode.Create, FileAccess.ReadWrite, FileShare.Write);
                }

                var expected = Environment.NewLine;
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_stringEmpty_FileModeCreate_FileAccessReadWrite_FileShareWrite_whenCalledAgain()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                using (var obj = new StreamWriterDictionary())
                {
                    obj.Item(file.Info.FullName, string.Empty, FileMode.Create, FileAccess.ReadWrite, FileShare.Write);
                }

                using (var obj = new StreamWriterDictionary())
                {
                    obj.Item(file.Info.FullName, string.Empty, FileMode.Create, FileAccess.ReadWrite, FileShare.Write);
                }

                var expected = Environment.NewLine;
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_stringEmpty_FileModeTruncate_FileAccessReadWrite_FileShareWrite()
        {
            using (var file = new TempFile())
            {
                using (var obj = new StreamWriterDictionary())
                {
                    obj.Item(file.Info.FullName, string.Empty, FileMode.Truncate, FileAccess.ReadWrite, FileShare.Write);
                }

                var expected = Environment.NewLine;
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_stringNull_FileModeCreateNew_FileAccessReadWrite_FileShareWrite()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                using (var obj = new StreamWriterDictionary())
                {
                    obj.Item(file.Info.FullName, null, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Write);
                }

                var expected = string.Empty;
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_stringNull_FileModeCreate_FileAccessReadWrite_FileShareWrite()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                using (var obj = new StreamWriterDictionary())
                {
                    obj.Item(file.Info.FullName, null, FileMode.Create, FileAccess.ReadWrite, FileShare.Write);
                }

                var expected = string.Empty;
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_stringNull_FileModeCreate_FileAccessReadWrite_FileShareWrite_whenCalledAgain()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                using (var obj = new StreamWriterDictionary())
                {
                    obj.Item(file.Info.FullName, null, FileMode.Create, FileAccess.ReadWrite, FileShare.Write);
                }

                using (var obj = new StreamWriterDictionary())
                {
                    obj.Item(file.Info.FullName, null, FileMode.Create, FileAccess.ReadWrite, FileShare.Write);
                }

                var expected = string.Empty;
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_stringNull_FileModeTruncate_FileAccessReadWrite_FileShareWrite()
        {
            using (var file = new TempFile())
            {
                using (var obj = new StreamWriterDictionary())
                {
                    obj.Item(file.Info.FullName, null, FileMode.Truncate, FileAccess.ReadWrite, FileShare.Write);
                }

                var expected = string.Empty;
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_string_FileModeAppend_FileAccessWrite_FileShareWrite()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                using (var obj = new StreamWriterDictionary("example"))
                {
                    obj.Item(file.Info.FullName, "one", FileMode.Append, FileAccess.Write, FileShare.Write).Write("two");
                }

                var expected = "one{0}two".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_string_FileModeAppend_FileAccessWrite_FileShareWrite_whenCalledAgain()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                using (var obj = new StreamWriterDictionary("example"))
                {
                    obj.Item(file.Info.FullName, "one", FileMode.Append, FileAccess.Write, FileShare.Write).WriteLine("two");
                }

                using (var obj = new StreamWriterDictionary("example"))
                {
                    obj.Item(file.Info.FullName, "one", FileMode.Append, FileAccess.Write, FileShare.Write).Write("three");
                }

                var expected = "one{0}two{0}three".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_string_FileModeCreate_FileAccessReadWrite_FileShareWrite()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                using (var obj = new StreamWriterDictionary("example"))
                {
                    obj.Item(file.Info.FullName, "one", FileMode.Create, FileAccess.ReadWrite, FileShare.Write).Write("two");
                }

                var expected = "one{0}two".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_string_FileModeCreate_FileAccessReadWrite_FileShareWrite_whenCalledAgain()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                using (var obj = new StreamWriterDictionary("example"))
                {
                    obj.Item(file.Info.FullName, "one", FileMode.Create, FileAccess.ReadWrite, FileShare.Write).Write("two");
                }

                using (var obj = new StreamWriterDictionary("example"))
                {
                    obj.Item(file.Info.FullName, "one", FileMode.Create, FileAccess.ReadWrite, FileShare.Write).Write("two");
                }

                var expected = "one{0}two".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_string_FileModeOpenOrCreate_FileAccessReadWrite_FileShareWrite()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                using (var obj = new StreamWriterDictionary("example"))
                {
                    obj.Item(file.Info.FullName, "one", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Write).Write("two");
                }

                var expected = "one{0}two".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_string_FileModeOpenOrCreate_FileAccessReadWrite_FileShareWrite_whenCalledAgain()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                using (var obj = new StreamWriterDictionary("example"))
                {
                    obj.Item(file.Info.FullName, "one", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Write).Write("two");
                }

                using (var obj = new StreamWriterDictionary("example"))
                {
                    obj.Item(file.Info.FullName, "one", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Write).Write("two");
                }

                var expected = "one{0}two".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_string_FileModeTruncate_FileAccessReadWrite_FileShareWrite()
        {
            using (var file = new TempFile())
            {
                using (var obj = new StreamWriterDictionary("example"))
                {
                    obj.Item(file.Info.FullName, "one", FileMode.Truncate, FileAccess.ReadWrite, FileShare.Write).Write("two");
                }

                var expected = "one{0}two".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Item_string_string_FileModeTruncate_FileAccessReadWrite_FileShareWrite_whenCalledAgain()
        {
            using (var file = new TempFile())
            {
                using (var obj = new StreamWriterDictionary("example"))
                {
                    obj.Item(file.Info.FullName, "one", FileMode.Truncate, FileAccess.ReadWrite, FileShare.Write).Write("two");
                }

                using (var obj = new StreamWriterDictionary("example"))
                {
                    obj.Item(file.Info.FullName, "one", FileMode.Truncate, FileAccess.ReadWrite, FileShare.Write).Write("two");
                }

                var expected = "one{0}two".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.Info.FullName);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void prop_Access()
        {
            Assert.True(new PropertyExpectations<StreamWriterDictionary>(p => p.Access)
                            .IsAutoProperty(FileAccess.Write)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_FirstLine()
        {
            Assert.True(new PropertyExpectations<StreamWriterDictionary>(p => p.FirstLine)
                            .IsAutoProperty<string>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Mode()
        {
            Assert.True(new PropertyExpectations<StreamWriterDictionary>(p => p.Mode)
                            .IsAutoProperty(FileMode.OpenOrCreate)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Share()
        {
            Assert.True(new PropertyExpectations<StreamWriterDictionary>(p => p.Share)
                            .IsAutoProperty(FileShare.Read)
                            .IsNotDecorated()
                            .Result);
        }
    }
}