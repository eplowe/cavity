namespace Cavity
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using Xunit;

    public sealed class AbsoluteUriFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<AbsoluteUri>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .NoDefaultConstructor()
                            .Serializable()
                            .Implements<IComparable>()
                            .Implements<IComparable<AbsoluteUri>>()
                            .Implements<IEquatable<AbsoluteUri>>()
                            .Result);
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            var expected = new AbsoluteUri("http://example.com");
            AbsoluteUri actual;

            using (Stream stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new AbsoluteUri("http://example.com"));
                stream.Position = 0;
                actual = (AbsoluteUri)formatter.Deserialize(stream);
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_UriAbsolute()
        {
            Assert.NotNull(new AbsoluteUri(new Uri("http://example.com/")));
        }

        [Fact]
        public void ctor_UriNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AbsoluteUri(null as Uri));
        }

        [Fact]
        public void ctor_UriRelative()
        {
            Assert.Throws<UriFormatException>(() => new AbsoluteUri(new Uri("/", UriKind.Relative)));
        }

        [Fact]
        public void ctor_stringAbsolute()
        {
            Assert.NotNull(new AbsoluteUri("http://example.com/"));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.Throws<UriFormatException>(() => new AbsoluteUri(string.Empty));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AbsoluteUri(null as string));
        }

        [Fact]
        public void ctor_stringRelative()
        {
            Assert.Throws<UriFormatException>(() => new AbsoluteUri("/"));
        }

        [Fact]
        public void opEquality_AbsoluteUri_AbsoluteUri()
        {
            var obj = new AbsoluteUri("http://example.com/");
            var comparand = new AbsoluteUri("http://example.com/");

            Assert.True(obj == comparand);
        }

        [Fact]
        public void opEquality_AbsoluteUri_AbsoluteUriDiffers()
        {
            var obj = new AbsoluteUri("http://example.com/");
            var comparand = new AbsoluteUri("http://example.net/");

            Assert.False(obj == comparand);
        }

        [Fact]
        public void opEquality_AbsoluteUri_AbsoluteUriSame()
        {
            var obj = new AbsoluteUri("http://example.com/");
            var comparand = obj;

            Assert.True(obj == comparand);
        }

        [Fact]
        public void opExplicit_AbsoluteUri_UriAbsolute()
        {
            var expected = new AbsoluteUri(new Uri("http://example.com/"));
            AbsoluteUri actual = new Uri("http://example.com/");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opExplicit_AbsoluteUri_UriRelative()
        {
            Assert.Throws<UriFormatException>(() => (AbsoluteUri)new Uri("/", UriKind.Relative));
        }

        [Fact]
        public void opExplicit_AbsoluteUri_string()
        {
            var expected = new AbsoluteUri("http://example.com/");
            AbsoluteUri actual = "http://example.com/";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opExplicit_AbsoluteUri_stringEmpty()
        {
            Assert.Throws<UriFormatException>(() => (AbsoluteUri)string.Empty);
        }

        [Fact]
        public void opExplicit_Uri_AbsoluteUri()
        {
            var expected = new Uri("http://example.com/");
            Uri actual = new AbsoluteUri(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opExplicit_string_AbsoluteUri()
        {
            const string expected = "http://example.com/";
            string actual = new AbsoluteUri(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opGreaterThan_AbsoluteUriGreater_AbsoluteUri()
        {
            var obj = new AbsoluteUri("http://example.net/");
            var comparand = new AbsoluteUri("http://example.com/");

            Assert.True(obj > comparand);
        }

        [Fact]
        public void opGreaterThan_AbsoluteUriNull_AbsoluteUri()
        {
            var comparand = new AbsoluteUri("http://example.com/");

            Assert.False(null > comparand);
        }

        [Fact]
        public void opGreaterThan_AbsoluteUri_AbsoluteUri()
        {
            var obj = new AbsoluteUri("http://example.com/");
            var comparand = new AbsoluteUri("http://example.com/");

            Assert.False(obj > comparand);
        }

        [Fact]
        public void opGreaterThan_AbsoluteUri_AbsoluteUriGreater()
        {
            var obj = new AbsoluteUri("http://example.com/");
            var comparand = new AbsoluteUri("http://example.net/");

            Assert.False(obj > comparand);
        }

        [Fact]
        public void opGreaterThan_AbsoluteUri_AbsoluteUriNull()
        {
            var obj = new AbsoluteUri("http://example.com/");

            Assert.True(obj > null);
        }

        [Fact]
        public void opImplicit_AbsoluteUri_Uri()
        {
            var expected = new AbsoluteUri("http://example.com/");
            AbsoluteUri actual = new Uri("http://example.com/");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_AbsoluteUri_UriRelative()
        {
            Assert.Throws<UriFormatException>(() => (AbsoluteUri)new Uri("/", UriKind.Relative));
        }

        [Fact]
        public void opImplicit_AbsoluteUri_string()
        {
            var expected = new AbsoluteUri("http://example.com/");
            AbsoluteUri actual = "http://example.com/";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_AbsoluteUri_stringEmpty()
        {
            Assert.Throws<UriFormatException>(() => (AbsoluteUri)string.Empty);
        }

        [Fact]
        public void opImplicit_Uri_AbsoluteUri()
        {
            var expected = new Uri("http://example.com/");
            Uri actual = new AbsoluteUri(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_string_AbsoluteUri()
        {
            const string expected = "http://example.com/";
            string actual = new AbsoluteUri(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opInequality_AbsoluteUri_AbsoluteUri()
        {
            var obj = new AbsoluteUri("http://example.com/");
            var comparand = new AbsoluteUri("http://example.com/");

            Assert.False(obj != comparand);
        }

        [Fact]
        public void opInequality_AbsoluteUri_AbsoluteUriDiffers()
        {
            var obj = new AbsoluteUri("http://example.com/");
            var comparand = new AbsoluteUri("http://example.net/");

            Assert.True(obj != comparand);
        }

        [Fact]
        public void opInequality_AbsoluteUri_AbsoluteUriSame()
        {
            var obj = new AbsoluteUri("http://example.com/");
            var comparand = obj;

            Assert.False(obj != comparand);
        }

        [Fact]
        public void opLesserThan_AbsoluteUriLesser_AbsoluteUri()
        {
            var obj = new AbsoluteUri("http://example.com/");
            var comparand = new AbsoluteUri("http://example.net/");

            Assert.True(obj < comparand);
        }

        [Fact]
        public void opLesserThan_AbsoluteUriNull_AbsoluteUri()
        {
            var comparand = new AbsoluteUri("http://example.com/");

            Assert.True(null < comparand);
        }

        [Fact]
        public void opLesserThan_AbsoluteUri_AbsoluteUri()
        {
            var obj = new AbsoluteUri("http://example.com/");
            var comparand = new AbsoluteUri("http://example.com/");

            Assert.False(obj < comparand);
        }

        [Fact]
        public void opLesserThan_AbsoluteUri_AbsoluteUriLesser()
        {
            var obj = new AbsoluteUri("http://example.net/");
            var comparand = new AbsoluteUri("http://example.com/");

            Assert.False(obj < comparand);
        }

        [Fact]
        public void opLesserThan_AbsoluteUri_AbsoluteUriNull()
        {
            var obj = new AbsoluteUri("http://example.com/");

            Assert.False(obj < null);
        }

        [Fact]
        public void op_CompareTo_AbsoluteUri()
        {
            const int expected = 1;
            var actual = new AbsoluteUri("http://example.com/").CompareTo(null);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_AbsoluteUriEqual()
        {
            var obj = new AbsoluteUri("http://example.com/");
            var comparand = new AbsoluteUri("http://example.com/");

            const int expected = 0;
            var actual = obj.CompareTo(comparand);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_AbsoluteUriGreater()
        {
            var obj = new AbsoluteUri("http://example.com/");
            var comparand = new AbsoluteUri("http://example.net/");

            const int expected = -11;
            var actual = obj.CompareTo(comparand);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_AbsoluteUriLesser()
        {
            var obj = new AbsoluteUri("http://example.net/");
            var comparand = new AbsoluteUri("http://example.com/");

            const int expected = 11;
            var actual = obj.CompareTo(comparand);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_AbsoluteUriSame()
        {
            var obj = new AbsoluteUri("http://example.com/");

            const int expected = 0;
            var actual = obj.CompareTo(obj);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_object()
        {
            const int expected = 1;
            var actual = new AbsoluteUri("http://example.com/").CompareTo(null as object);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectEqual()
        {
            var obj = new AbsoluteUri("http://example.com/");
            object comparand = new AbsoluteUri("http://example.com/");

            const int expected = 0;
            var actual = obj.CompareTo(comparand);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectGreater()
        {
            var obj = new AbsoluteUri("http://example.com/");
            object comparand = new AbsoluteUri("http://example.net/");

            const int expected = -11;
            var actual = obj.CompareTo(comparand);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectLesser()
        {
            var obj = new AbsoluteUri("http://example.net/");
            object comparand = new AbsoluteUri("http://example.com/");

            const int expected = 11;
            var actual = obj.CompareTo(comparand);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectSame()
        {
            var obj = new AbsoluteUri("http://example.com/");

            const int expected = 0;
            var actual = obj.CompareTo(obj as object);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectString()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new AbsoluteUri("http://example.com/").CompareTo("http://example.com/" as object));
        }

        [Fact]
        public void op_Equals_AbsoluteUriEqual()
        {
            var obj = new AbsoluteUri("http://example.com/");
            var comparand = new AbsoluteUri("http://example.com/");

            Assert.True(obj.Equals(comparand));
        }

        [Fact]
        public void op_Equals_AbsoluteUriNull()
        {
            Assert.False(new AbsoluteUri("http://example.com/").Equals(null));
        }

        [Fact]
        public void op_Equals_AbsoluteUriSame()
        {
            var obj = new AbsoluteUri("http://example.com/");
            var comparand = obj;

            Assert.True(obj.Equals(comparand));
        }

        [Fact]
        public void op_Equals_AbsoluteUriUnequal()
        {
            var obj = new AbsoluteUri("http://example.com/");
            var comparand = new AbsoluteUri("http://example.net/");

            Assert.False(obj.Equals(comparand));
        }

        [Fact]
        public void op_Equals_object()
        {
            var obj = new AbsoluteUri("http://example.com/");
            var comparand = new AbsoluteUri("http://example.com/");

            Assert.True(obj.Equals(comparand as object));
        }

        [Fact]
        public void op_Equals_objectDiffer()
        {
            var obj = new AbsoluteUri("http://example.com/");
            var comparand = new AbsoluteUri("http://example.net/");

            Assert.False(obj.Equals(comparand as object));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new AbsoluteUri("http://example.com/").Equals(null as object));
        }

        [Fact]
        public void op_Equals_objectSame()
        {
            var obj = new AbsoluteUri("http://example.com/");

            Assert.True(obj.Equals(obj as object));
        }

        [Fact]
        public void op_Equals_objectString()
        {
            Assert.False(new AbsoluteUri("http://example.com/").Equals("http://example.com/" as object));
        }

        [Fact]
        public void op_GetHashCode()
        {
            var obj = new AbsoluteUri("http://example.com/");

            var expected = obj.ToString().GetHashCode();
            var actual = obj.GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetObjectData_SerializationInfoNull_StreamingContext()
        {
            var context = new StreamingContext(StreamingContextStates.All);

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => (new AbsoluteUri("http://example.com/") as ISerializable).GetObjectData(null, context));

            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Fact]
        public void op_GetObjectData_SerializationInfo_StreamingContext()
        {
            var info = new SerializationInfo(typeof(AbsoluteUri), new FormatterConverter());
            var context = new StreamingContext(StreamingContextStates.All);

            const string expected = "http://example.com/";

            (new AbsoluteUri(expected) as ISerializable).GetObjectData(info, context);

            var actual = info.GetString("_value");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "http://example.com/";
            var actual = new AbsoluteUri(expected).ToString();

            Assert.Equal(expected, actual);
        }

#if !NET20
        [Fact]
        public void op_ToPath_FileSystemInfo()
        {
            AbsoluteUri obj = "http://example.com/foo/bar?x=1&y=2#fragment";

            var expected = new DirectoryInfo(Path.Combine(Path.GetTempPath(), @"http\example.com\foo\bar&quest;x=1&y=2#fragment"));
            var actual = obj.ToPath(new DirectoryInfo(Path.GetTempPath()));

            Assert.Equal(expected.FullName, actual.FullName);
        }

        [Fact]
        public void op_ToPath_FileSystemInfo_whenHttp()
        {
            AbsoluteUri obj = "http://example.com";

            var expected = new DirectoryInfo(Path.Combine(Path.GetTempPath(), @"http\example.com\"));
            var actual = obj.ToPath(new DirectoryInfo(Path.GetTempPath()));

            Assert.Equal(expected.FullName, actual.FullName);
        }

        [Fact]
        public void op_ToPath_FileSystemInfo_whenTel()
        {
            AbsoluteUri obj = "tel:+441234555666";

            var expected = new DirectoryInfo(Path.Combine(Path.GetTempPath(), @"tel\+441234555666"));
            var actual = obj.ToPath(new DirectoryInfo(Path.GetTempPath()));

            Assert.Equal(expected.FullName, actual.FullName);
        }

        [Fact]
        public void op_ToPath_FileSystemInfo_whenUrn()
        {
            AbsoluteUri obj = "urn://example.com";

            var expected = new DirectoryInfo(Path.Combine(Path.GetTempPath(), @"urn\example.com\"));
            var actual = obj.ToPath(new DirectoryInfo(Path.GetTempPath()));

            Assert.Equal(expected.FullName, actual.FullName);
        }

        [Fact]
        public void op_ToPath_FileSystemInfo_whenHttpPath()
        {
            AbsoluteUri obj = "http://example.com/foo/bar";

            var expected = new DirectoryInfo(Path.Combine(Path.GetTempPath(), @"http\example.com\foo\bar"));
            var actual = obj.ToPath(new DirectoryInfo(Path.GetTempPath()));

            Assert.Equal(expected.FullName, actual.FullName);
        }

        [Fact]
        public void op_ToPath_FileSystemInfo_whenHttpQuery()
        {
            AbsoluteUri obj = "http://example.com/foo/bar?x=1&y=2";

            var expected = new DirectoryInfo(Path.Combine(Path.GetTempPath(), @"http\example.com\foo\bar&quest;x=1&y=2"));
            var actual = obj.ToPath(new DirectoryInfo(Path.GetTempPath()));

            Assert.Equal(expected.FullName, actual.FullName);
        }

        [Fact]
        public void op_ToPath_FileSystemInfo_whenHttpQueryAsterix()
        {
            AbsoluteUri obj = "http://example.com/foo/bar?q=*";

            var expected = new DirectoryInfo(Path.Combine(Path.GetTempPath(), @"http\example.com\foo\bar&quest;q=&ast;"));
            var actual = obj.ToPath(new DirectoryInfo(Path.GetTempPath()));

            Assert.Equal(expected.FullName, actual.FullName);
        }

        [Fact]
        public void op_ToPath_FileSystemInfo_whenHttpQueryBackslash()
        {
            AbsoluteUri obj = @"http://example.com/foo/bar?q=a\b";

            var expected = new DirectoryInfo(Path.Combine(Path.GetTempPath(), @"http\example.com\foo\bar&quest;q=a&bsol;b"));
            var actual = obj.ToPath(new DirectoryInfo(Path.GetTempPath()));

            Assert.Equal(expected.FullName, actual.FullName);
        }

        [Fact]
        public void op_ToPath_FileSystemInfo_whenHttpQueryColon()
        {
            AbsoluteUri obj = "http://example.com/foo/bar?q=a:b";

            var expected = new DirectoryInfo(Path.Combine(Path.GetTempPath(), @"http\example.com\foo\bar&quest;q=a&colon;b"));
            var actual = obj.ToPath(new DirectoryInfo(Path.GetTempPath()));

            Assert.Equal(expected.FullName, actual.FullName);
        }

        [Fact]
        public void op_ToPath_FileSystemInfo_whenHttpQueryGreaterThan()
        {
            AbsoluteUri obj = "http://example.com/foo/bar?q=>5";

            var expected = new DirectoryInfo(Path.Combine(Path.GetTempPath(), @"http\example.com\foo\bar&quest;q=&gt;5"));
            var actual = obj.ToPath(new DirectoryInfo(Path.GetTempPath()));

            Assert.Equal(expected.FullName, actual.FullName);
        }

        [Fact]
        public void op_ToPath_FileSystemInfo_whenHttpQueryLesserThan()
        {
            AbsoluteUri obj = "http://example.com/foo/bar?q=<5";

            var expected = new DirectoryInfo(Path.Combine(Path.GetTempPath(), @"http\example.com\foo\bar&quest;q=&lt;5"));
            var actual = obj.ToPath(new DirectoryInfo(Path.GetTempPath()));

            Assert.Equal(expected.FullName, actual.FullName);
        }

        [Fact]
        public void op_ToPath_FileSystemInfo_whenHttpQueryQuote()
        {
            AbsoluteUri obj = "http://example.com/foo/bar?q=\"5\"";

            var expected = new DirectoryInfo(Path.Combine(Path.GetTempPath(), @"http\example.com\foo\bar&quest;q=&quot;5&quot;"));
            var actual = obj.ToPath(new DirectoryInfo(Path.GetTempPath()));

            Assert.Equal(expected.FullName, actual.FullName);
        }

        [Fact]
        public void op_ToPath_FileSystemInfo_whenHttpQueryVerticalBar()
        {
            AbsoluteUri obj = "http://example.com/foo/bar?q=4|5";

            var expected = new DirectoryInfo(Path.Combine(Path.GetTempPath(), @"http\example.com\foo\bar&quest;q=4&verbar;5"));
            var actual = obj.ToPath(new DirectoryInfo(Path.GetTempPath()));

            Assert.Equal(expected.FullName, actual.FullName);
        }

        [Fact]
        public void op_ToPath_FileSystemInfoNull()
        {
            AbsoluteUri obj = "http://example.com/";

            Assert.Throws<ArgumentNullException>(() => obj.ToPath(null));
        }

#endif
    }
}