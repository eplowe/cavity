﻿namespace Cavity
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
                            .IsSealed()
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
        public void opExplicit_AbsoluteUri_UriNull()
        {
            Uri value = null;

            AbsoluteUri expected = null;
            AbsoluteUri actual = value;

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
        public void opExplicit_AbsoluteUri_stringNull()
        {
            string value = null;

            AbsoluteUri expected = null;
            AbsoluteUri actual = value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opExplicit_Uri_AbsoluteUri()
        {
            var expected = new Uri("http://example.com/");
            Uri actual = new AbsoluteUri(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opExplicit_Uri_AbsoluteUriNull()
        {
            Uri expected = null;
            Uri actual = null as AbsoluteUri;

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
        public void opExplicit_string_AbsoluteUriNull()
        {
            string expected = null;
            string actual = null as AbsoluteUri;

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
            AbsoluteUri obj = null;
            var comparand = new AbsoluteUri("http://example.com/");

            Assert.False(obj > comparand);
        }

        [Fact]
        public void opGreaterThan_AbsoluteUriNull_AbsoluteUriNull()
        {
            AbsoluteUri obj = null;
            AbsoluteUri comparand = null;

            Assert.False(obj > comparand);
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
            AbsoluteUri comparand = null;

            Assert.True(obj > comparand);
        }

        [Fact]
        public void opImplicit_AbsoluteUri_Uri()
        {
            var expected = new AbsoluteUri("http://example.com/");
            AbsoluteUri actual = new Uri("http://example.com/");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_AbsoluteUri_UriNull()
        {
            Uri value = null;

            AbsoluteUri expected = null;
            AbsoluteUri actual = value;

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
        public void opImplicit_AbsoluteUri_stringNull()
        {
            string value = null;

            AbsoluteUri expected = null;
            AbsoluteUri actual = value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_Uri_AbsoluteUri()
        {
            var expected = new Uri("http://example.com/");
            Uri actual = new AbsoluteUri(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_Uri_AbsoluteUriNull()
        {
            Uri expected = null;
            Uri actual = null as AbsoluteUri;

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
        public void opImplicit_string_AbsoluteUriNull()
        {
            string expected = null;
            string actual = null as AbsoluteUri;

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
            AbsoluteUri obj = null;
            var comparand = new AbsoluteUri("http://example.com/");

            Assert.True(obj < comparand);
        }

        [Fact]
        public void opLesserThan_AbsoluteUriNull_AbsoluteUriNull()
        {
            AbsoluteUri obj = null;
            AbsoluteUri comparand = null;

            Assert.False(obj < comparand);
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
            AbsoluteUri comparand = null;

            Assert.False(obj < comparand);
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

            Assert.Throws<ArgumentNullException>(() => (new AbsoluteUri("http://example.com/") as ISerializable).GetObjectData(null, context));
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
    }
}