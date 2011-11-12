namespace Cavity.Globalization
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using Xunit;

    public sealed class LanguageFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Language>()
                            .IsValueType()
                            .Implements<IEquatable<Language>>()
                            .Serializable()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new Language());
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            var expected = new Language("fr");
            Language actual;

            using (Stream stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new Language("fr"));
                stream.Position = 0;
                actual = (Language)formatter.Deserialize(stream);
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new Language("en"));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.NotNull(new Language(string.Empty));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.NotNull(new Language(null));
        }

        [Fact]
        public void opEquality_Language_Language()
        {
            var obj = new Language();
            var comparand = new Language();

            Assert.True(obj == comparand);
        }

        [Fact]
        public void opImplicit_Language_string()
        {
            var expected = new Language("en");
            Language actual = "en";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_string_Language()
        {
            const string expected = "en";
            string actual = new Language("en");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_Language_CultureInfo()
        {
            var expected = new Language("en");
            Language actual = new CultureInfo("en");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_Language_CultureInfoNull()
        {
            var expected = new Language();
            Language actual = null as CultureInfo;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_Language_CultureInfoInvariantCulture()
        {
            var expected = new Language();
            Language actual = CultureInfo.InvariantCulture;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_CultureInfo_Language()
        {
            var expected = new CultureInfo("en");
            CultureInfo actual = new Language("en");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_CultureInfoInvariantCulture_Language()
        {
            var expected = CultureInfo.InvariantCulture;
            CultureInfo actual = new Language();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opInequality_Language_Language()
        {
            var obj = new Language();
            var comparand = new Language();

            Assert.False(obj != comparand);
        }

        [Fact]
        public void op_Equals_Language()
        {
            var obj = new Language(null);

            Assert.True(new Language().Equals(obj));
        }

        [Fact]
        public void op_Equals_object()
        {
            object obj = new Language(null);

            Assert.True(new Language().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectDiffers()
        {
            var obj = new Language("en");

            Assert.False(new Language().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectInvalidCast()
        {
            var obj = new Uri("http://example.com/");

            Assert.Throws<InvalidCastException>(() => new Language().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new Language().Equals(null as object));
        }

        [Fact]
        public void op_GetHashCode()
        {
            var expected = string.Empty.GetHashCode();
            var actual = new Language().GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetHashCode_whenValue()
        {
            var expected = "en".GetHashCode();
            var actual = new Language("en").GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetObjectData_SerializationInfoNull_StreamingContext()
        {
            var context = new StreamingContext(StreamingContextStates.All);

            ISerializable value = new Language("fr");

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => value.GetObjectData(null, context));
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Fact]
        public void op_GetObjectData_SerializationInfo_StreamingContext()
        {
            var info = new SerializationInfo(typeof(Language), new FormatterConverter());
            var context = new StreamingContext(StreamingContextStates.All);

            const string expected = "fr";

            ISerializable value = new Language("fr");

            value.GetObjectData(info, context);

            var actual = info.GetString("_value");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToCultureInfo()
        {
            var expected = CultureInfo.InvariantCulture;
            var actual = new Language().ToCultureInfo();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToCultureInfo_whenValue()
        {
            const string expected = "en";
            var actual = new Language("en").ToCultureInfo().Name;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            var expected = string.Empty;
            var actual = new Language().ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_whenValue()
        {
            const string expected = "en";
            var actual = new Language(expected).ToString();

            Assert.Equal(expected, actual);
        }
    }
}