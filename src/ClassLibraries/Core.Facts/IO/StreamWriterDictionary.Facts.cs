namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Serialization;
    using Xunit;

    public sealed class StreamWriterDictionaryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<StreamWriterDictionary>()
                            .DerivesFrom<Dictionary<string, StreamWriter>>()
                            .IsConcreteClass()
                            .IsSealed()
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
            var file = new FileInfo(Path.GetTempFileName());

            try
            {
                var obj = new StreamWriterDictionary();

                obj.Dispose();

                Assert.Throws<InvalidOperationException>(() => obj.Item(file));
            }
            finally
            {
                file.Delete();
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
            var file = new FileInfo(Path.GetTempFileName());

            try
            {
                using (var obj = new StreamWriterDictionary())
                {
                    obj.Item(file).Write("example");
                }

                Assert.True(file.Exists);
            }
            finally
            {
                file.Delete();
            }
        }

        [Fact]
        public void op_Item_FileInfoNull()
        {
            using (var obj = new StreamWriterDictionary())
            {
                Assert.Throws<ArgumentNullException>(() => obj.Item(null as FileInfo));
            }
        }

        [Fact]
        public void op_Item_FileInfo_string()
        {
            var file = new FileInfo(Path.GetTempFileName());

            try
            {
                using (var obj = new StreamWriterDictionary())
                {
                    obj.Item(file, "one").Write("two");
                }

                var expected = "one{0}two".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.FullName);

                Assert.Equal(expected, actual);
            }
            finally
            {
                file.Delete();
            }
        }

        [Fact]
        public void op_Item_string()
        {
            var file = new FileInfo(Path.GetTempFileName());

            try
            {
                using (var obj = new StreamWriterDictionary())
                {
                    obj.Item(file.FullName).Write("example");
                }

                Assert.True(file.Exists);
            }
            finally
            {
                file.Delete();
            }
        }

        [Fact]
        public void op_Item_stringEmpty()
        {
            using (var obj = new StreamWriterDictionary())
            {
                Assert.Throws<ArgumentException>(() => obj.Item(string.Empty));
            }
        }

        [Fact]
        public void op_Item_stringNull()
        {
            using (var obj = new StreamWriterDictionary())
            {
                Assert.Throws<ArgumentNullException>(() => obj.Item(null as string));
            }
        }

        [Fact]
        public void op_Item_string_FileModeCreateNew_FileAccessReadWrite_FileShareWrite()
        {
            var file = new FileInfo(Path.GetTempFileName());

            try
            {
                using (var obj = new StreamWriterDictionary("one"))
                {
                    obj.Item(file.FullName, FileMode.Truncate, FileAccess.ReadWrite, FileShare.Write).Write("two");
                }

                var expected = "one{0}two".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.FullName);

                Assert.Equal(expected, actual);
            }
            finally
            {
                file.Delete();
            }
        }

        [Fact]
        public void op_Item_string_string()
        {
            var file = new FileInfo(Path.GetTempFileName());

            try
            {
                using (var obj = new StreamWriterDictionary())
                {
                    obj.Item(file.FullName, "one").Write("two");
                }

                var expected = "one{0}two".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.FullName);

                Assert.Equal(expected, actual);
            }
            finally
            {
                file.Delete();
            }
        }

        [Fact]
        public void op_Item_string_stringEmpty_FileModeCreateNew_FileAccessReadWrite_FileShareWrite()
        {
            var file = new FileInfo(Path.GetTempFileName());

            try
            {
                using (var obj = new StreamWriterDictionary())
                {
                    obj.Item(file.FullName, string.Empty, FileMode.Truncate, FileAccess.ReadWrite, FileShare.Write);
                }

                var expected = Environment.NewLine;
                var actual = File.ReadAllText(file.FullName);

                Assert.Equal(expected, actual);
            }
            finally
            {
                file.Delete();
            }
        }

        [Fact]
        public void op_Item_string_stringNull_FileModeCreateNew_FileAccessReadWrite_FileShareWrite()
        {
            var file = new FileInfo(Path.GetTempFileName());

            try
            {
                using (var obj = new StreamWriterDictionary())
                {
                    obj.Item(file.FullName, null, FileMode.Truncate, FileAccess.ReadWrite, FileShare.Write);
                }

                var expected = string.Empty;
                var actual = File.ReadAllText(file.FullName);

                Assert.Equal(expected, actual);
            }
            finally
            {
                file.Delete();
            }
        }

        [Fact]
        public void op_Item_string_string_FileModeCreateNew_FileAccessReadWrite_FileShareWrite()
        {
            var file = new FileInfo(Path.GetTempFileName());

            try
            {
                using (var obj = new StreamWriterDictionary("example"))
                {
                    obj.Item(file.FullName, "one", FileMode.Truncate, FileAccess.ReadWrite, FileShare.Write).Write("two");
                }

                var expected = "one{0}two".FormatWith(Environment.NewLine);
                var actual = File.ReadAllText(file.FullName);

                Assert.Equal(expected, actual);
            }
            finally
            {
                file.Delete();
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
                            .IsAutoProperty(FileShare.None)
                            .IsNotDecorated()
                            .Result);
        }
    }
}