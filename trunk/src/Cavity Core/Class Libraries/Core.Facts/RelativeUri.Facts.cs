namespace Cavity
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using Xunit;

    public sealed class RelativeUriFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RelativeUri>().DerivesFrom<object>()
                                                           .IsConcreteClass()
                                                           .IsUnsealed()
                                                           .NoDefaultConstructor()
                                                           .Serializable()
                                                           .Implements<IComparable>()
                                                           .Implements<IComparable<RelativeUri>>()
                                                           .Implements<IEquatable<RelativeUri>>()
                                                           .Result);
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            var expected = new RelativeUri("/");
            RelativeUri actual;

            using (Stream stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new RelativeUri("/"));
                stream.Position = 0;
                actual = (RelativeUri)formatter.Deserialize(stream);
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_UriAbsolute()
        {
            Assert.Throws<UriFormatException>(() => new RelativeUri(new Uri("http://example.com/")));
        }

        [Fact]
        public void ctor_UriNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RelativeUri(null as Uri));
        }

        [Fact]
        public void ctor_UriRelative()
        {
            Assert.NotNull(new RelativeUri(new Uri("/", UriKind.Relative)));
        }

        [Fact]
        public void ctor_stringAbsolute()
        {
            Assert.Throws<UriFormatException>(() => new RelativeUri("http://example.com/"));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.NotNull(new RelativeUri(string.Empty));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RelativeUri(null as string));
        }

        [Fact]
        public void ctor_stringRelative()
        {
            Assert.NotNull(new RelativeUri("/"));
        }

        [Fact]
        public void opEquality_RelativeUri_RelativeUri()
        {
            var obj = new RelativeUri("/");
            var comparand = new RelativeUri("/");

            Assert.True(obj == comparand);
        }

        [Fact]
        public void opEquality_RelativeUri_RelativeUriDiffers()
        {
            var obj = new RelativeUri("/abc");
            var comparand = new RelativeUri("/xyz");

            Assert.False(obj == comparand);
        }

        [Fact]
        public void opEquality_RelativeUri_RelativeUriSame()
        {
            var obj = new RelativeUri("/");
            var comparand = obj;

            Assert.True(obj == comparand);
        }

        [Fact]
        public void opExplicit_RelativeUri_UriAbsolute()
        {
            Assert.Throws<UriFormatException>(() => (RelativeUri)new Uri("http://example.com/"));
        }

        [Fact]
        public void opExplicit_RelativeUri_UriRelative()
        {
            var expected = new RelativeUri("/");
            RelativeUri actual = new Uri("/", UriKind.Relative);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opExplicit_RelativeUri_string()
        {
            var expected = new RelativeUri("/");
            RelativeUri actual = "/";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opExplicit_RelativeUri_stringEmpty()
        {
            var expected = new RelativeUri(string.Empty);
            RelativeUri actual = string.Empty;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opExplicit_Uri_RelativeUri()
        {
            var expected = new Uri("/", UriKind.Relative);
            Uri actual = new RelativeUri(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opExplicit_string_RelativeUri()
        {
            const string expected = "/";
            string actual = new RelativeUri(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opGreaterThan_RelativeUriGreater_RelativeUri()
        {
            var obj = new RelativeUri("/xyz");
            var comparand = new RelativeUri("/abc");

            Assert.True(obj > comparand);
        }

        [Fact]
        public void opGreaterThan_RelativeUriNull_RelativeUri()
        {
            var comparand = new RelativeUri("/");

            Assert.False(null > comparand);
        }

        [Fact]
        public void opGreaterThan_RelativeUri_RelativeUri()
        {
            var obj = new RelativeUri("/");
            var comparand = new RelativeUri("/");

            Assert.False(obj > comparand);
        }

        [Fact]
        public void opGreaterThan_RelativeUri_RelativeUriGreater()
        {
            var obj = new RelativeUri("/abc");
            var comparand = new RelativeUri("/xyz");

            Assert.False(obj > comparand);
        }

        [Fact]
        public void opGreaterThan_RelativeUri_RelativeUriNull()
        {
            var obj = new RelativeUri("/");

            Assert.True(obj > null);
        }

        [Fact]
        public void opImplicit_RelativeUri_UriAbsolute()
        {
            Assert.Throws<UriFormatException>(() => (RelativeUri)new Uri("http://example.com/"));
        }

        [Fact]
        public void opImplicit_RelativeUri_UriRelative()
        {
            var expected = new RelativeUri("/");
            RelativeUri actual = new Uri("/", UriKind.Relative);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_RelativeUri_string()
        {
            var expected = new RelativeUri("/");
            RelativeUri actual = "/";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_RelativeUri_stringEmpty()
        {
            var expected = new RelativeUri(string.Empty);
            RelativeUri actual = string.Empty;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_Uri_RelativeUri()
        {
            var expected = new Uri("/", UriKind.Relative);
            Uri actual = new RelativeUri(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_string_RelativeUri()
        {
            const string expected = "/";
            string actual = new RelativeUri(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opInequality_RelativeUri_RelativeUri()
        {
            var obj = new RelativeUri("/");
            var comparand = new RelativeUri("/");

            Assert.False(obj != comparand);
        }

        [Fact]
        public void opInequality_RelativeUri_RelativeUriDiffers()
        {
            var obj = new RelativeUri("/abc");
            var comparand = new RelativeUri("/xyz");

            Assert.True(obj != comparand);
        }

        [Fact]
        public void opInequality_RelativeUri_RelativeUriSame()
        {
            var obj = new RelativeUri("/");
            var comparand = obj;

            Assert.False(obj != comparand);
        }

        [Fact]
        public void opLesserThan_RelativeUriLesser_RelativeUri()
        {
            var obj = new RelativeUri("/abc");
            var comparand = new RelativeUri("/xyz");

            Assert.True(obj < comparand);
        }

        [Fact]
        public void opLesserThan_RelativeUriNull_RelativeUri()
        {
            var comparand = new RelativeUri("/");

            Assert.True(null < comparand);
        }

        [Fact]
        public void opLesserThan_RelativeUri_RelativeUri()
        {
            var obj = new RelativeUri("/");
            var comparand = new RelativeUri("/");

            Assert.False(obj < comparand);
        }

        [Fact]
        public void opLesserThan_RelativeUri_RelativeUriLesser()
        {
            var obj = new RelativeUri("/xyz");
            var comparand = new RelativeUri("/abc");

            Assert.False(obj < comparand);
        }

        [Fact]
        public void opLesserThan_RelativeUri_RelativeUriNull()
        {
            var obj = new RelativeUri("/");

            Assert.False(obj < null);
        }

        [Fact]
        public void op_CompareTo_RelativeUri()
        {
            const int expected = 1;
            var actual = new RelativeUri("/").CompareTo(null);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_RelativeUriEqual()
        {
            var obj = new RelativeUri("/");
            var comparand = new RelativeUri("/");

            const int expected = 0;
            var actual = obj.CompareTo(comparand);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_RelativeUriGreater()
        {
            var obj = new RelativeUri("/abc");
            var comparand = new RelativeUri("/xyz");

            const int expected = -23;
            var actual = obj.CompareTo(comparand);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_RelativeUriLesser()
        {
            var obj = new RelativeUri("/xyz");
            var comparand = new RelativeUri("/abc");

            const int expected = 23;
            var actual = obj.CompareTo(comparand);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_RelativeUriSame()
        {
            var obj = new RelativeUri("/");

            const int expected = 0;
            var actual = obj.CompareTo(obj);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_object()
        {
            const int expected = 1;
            var actual = new RelativeUri("/").CompareTo(null as object);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectEqual()
        {
            var obj = new RelativeUri("/");
            object comparand = new RelativeUri("/");

            const int expected = 0;
            var actual = obj.CompareTo(comparand);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectGreater()
        {
            var obj = new RelativeUri("/abc");
            object comparand = new RelativeUri("/xyz");

            const int expected = -23;
            var actual = obj.CompareTo(comparand);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectLesser()
        {
            var obj = new RelativeUri("/xyz");
            object comparand = new RelativeUri("/abc");

            const int expected = 23;
            var actual = obj.CompareTo(comparand);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectSame()
        {
            var obj = new RelativeUri("/");

            const int expected = 0;
            var actual = obj.CompareTo(obj as object);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectString()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RelativeUri("/").CompareTo("/" as object));
        }

        [Fact]
        public void op_Equals_RelativeUriEqual()
        {
            var obj = new RelativeUri("/");
            var comparand = new RelativeUri("/");

            Assert.True(obj.Equals(comparand));
        }

        [Fact]
        public void op_Equals_RelativeUriNull()
        {
            Assert.False(new RelativeUri("/").Equals(null));
        }

        [Fact]
        public void op_Equals_RelativeUriSame()
        {
            var obj = new RelativeUri("/");
            var comparand = obj;

            Assert.True(obj.Equals(comparand));
        }

        [Fact]
        public void op_Equals_RelativeUriUnequal()
        {
            var obj = new RelativeUri("/abc");
            var comparand = new RelativeUri("/xyz");

            Assert.False(obj.Equals(comparand));
        }

        [Fact]
        public void op_Equals_object()
        {
            var obj = new RelativeUri("/");
            var comparand = new RelativeUri("/");

            Assert.True(obj.Equals(comparand as object));
        }

        [Fact]
        public void op_Equals_objectDiffer()
        {
            var obj = new RelativeUri("/abc");
            var comparand = new RelativeUri("/xyz");

            Assert.False(obj.Equals(comparand as object));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new RelativeUri("/").Equals(null as object));
        }

        [Fact]
        public void op_Equals_objectSame()
        {
            var obj = new RelativeUri("/");

            Assert.True(obj.Equals(obj as object));
        }

        [Fact]
        public void op_Equals_objectString()
        {
            Assert.False(new RelativeUri("/").Equals("/" as object));
        }

        [Fact]
        public void op_GetHashCode()
        {
            var obj = new RelativeUri("/");

            var expected = obj.ToString().GetHashCode();
            var actual = obj.GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetObjectData_SerializationInfoNull_StreamingContext()
        {
            var context = new StreamingContext(StreamingContextStates.All);

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => (new RelativeUri("/") as ISerializable).GetObjectData(null, context));

            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Fact]
        public void op_GetObjectData_SerializationInfo_StreamingContext()
        {
            var info = new SerializationInfo(typeof(RelativeUri), new FormatterConverter());
            var context = new StreamingContext(StreamingContextStates.All);

            const string expected = "/";

            (new RelativeUri(expected) as ISerializable).GetObjectData(info, context);

            var actual = info.GetString("_value");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "/";
            var actual = new RelativeUri(expected).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Length_get()
        {
            const int expected = 4;
            var actual = new RelativeUri("/234").Length;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Length_getEmpty()
        {
            const int expected = 0;
            var actual = new RelativeUri(string.Empty).Length;

            Assert.Equal(expected, actual);
        }
    }
}