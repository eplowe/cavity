namespace Cavity.Globalization
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using Xunit;

    public sealed class TranslationOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Translation<int>>()
                            .IsValueType()
                            .Implements<IEquatable<Translation<int>>>()
                            .Serializable()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new Translation<int>());
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            var expected = new Translation<int>(123, "en");
            Translation<int> actual;

            using (Stream stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new Translation<int>(123, "en"));
                stream.Position = 0;
                actual = (Translation<int>)formatter.Deserialize(stream);
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_T()
        {
            const int expected = 123;
            var actual = new Translation<int>(expected).Value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_T_Language()
        {
            var obj = new Translation<int>(123, "en");

            Assert.Equal(new Language("en"), obj.Language);
            Assert.Equal(123, obj.Value);
        }

        [Fact]
        public void opEquality_Language_Language()
        {
            var obj = new Translation<int>(123, "en");
            var comparand = new Translation<int>(123, "en");

            Assert.True(obj == comparand);
        }

        [Fact]
        public void opInequality_Language_Language()
        {
            var obj = new Translation<int>(123, "en");
            var comparand = new Translation<int>(123, "en");

            Assert.False(obj != comparand);
        }

        [Fact]
        public void op_Equals_Language()
        {
            var obj = new Translation<int>();

            Assert.True(new Translation<int>().Equals(obj));
        }

        [Fact]
        public void op_Equals_object()
        {
            object obj = new Translation<int>();

            Assert.True(new Translation<int>().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectDiffers()
        {
            var obj = new Translation<int>(123, "en");

            Assert.False(new Translation<int>().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectInvalidCast()
        {
            var obj = new Uri("http://example.com/");

            Assert.Throws<InvalidCastException>(() => new Translation<int>().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new Translation<int>().Equals(null));
        }

        [Fact]
        public void op_GetHashCode()
        {
            var expected = ": 0".GetHashCode();
            var actual = new Translation<int>().GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetHashCode_whenValue()
        {
            var expected = "en: 123".GetHashCode();
            var actual = new Translation<int>(123, "en").GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetObjectData_SerializationInfoNull_StreamingContext()
        {
            var context = new StreamingContext(StreamingContextStates.All);

            ISerializable value = new Translation<int>();

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => value.GetObjectData(null, context));

            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Fact]
        public void op_GetObjectData_SerializationInfo_StreamingContext()
        {
            var info = new SerializationInfo(typeof(Translation<int>), new FormatterConverter());
            var context = new StreamingContext(StreamingContextStates.All);

            ISerializable value = new Translation<int>(123, "en");

            value.GetObjectData(info, context);

            Assert.Equal("en", info.GetString("_language"));
            Assert.Equal(123, (int)info.GetValue("_value", typeof(int)));
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = ": 0";
            var actual = new Translation<int>().ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_whenValue()
        {
            const string expected = "en: 123";
            var actual = new Translation<int>(123, "en").ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Language()
        {
            Assert.True(new PropertyExpectations<Translation<int>>(x => x.Language)
                            .IsAutoProperty<Language>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<Translation<int>>(x => x.Value)
                            .IsAutoProperty<int>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}