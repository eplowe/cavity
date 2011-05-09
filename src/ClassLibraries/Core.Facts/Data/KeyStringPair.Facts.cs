namespace Cavity.Data
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using Xunit;

    public sealed class KeyStringPairFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<KeyStringPair>()
                            .IsValueType()
                            .Implements<ISerializable>()
                            .Implements<IEquatable<KeyStringPair>>()
                            .Serializable()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new KeyStringPair());
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            var expected = new KeyStringPair("key", "value");
            KeyStringPair actual;

            using (Stream stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new KeyStringPair("key", "value"));
                stream.Position = 0;
                actual = (KeyStringPair)formatter.Deserialize(stream);
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_stringEmpty_string()
        {
            Assert.NotNull(new KeyStringPair(string.Empty, "value"));
        }

        [Fact]
        public void ctor_stringNull_string()
        {
            Assert.NotNull(new KeyStringPair(null, "value"));
        }

        [Fact]
        public void ctor_string_string()
        {
            Assert.NotNull(new KeyStringPair("key", "value"));
        }

        [Fact]
        public void ctor_string_stringEmpty()
        {
            Assert.NotNull(new KeyStringPair("key", string.Empty));
        }

        [Fact]
        public void ctor_string_stringNull()
        {
            Assert.NotNull(new KeyStringPair("key", null));
        }

        [Fact]
        public void opEquality_KeyStringPair_KeyStringPair()
        {
            var obj = new KeyStringPair();
            var comparand = new KeyStringPair();

            Assert.True(obj == comparand);
        }

        [Fact]
        public void opInequality_KeyStringPair_KeyStringPair()
        {
            var obj = new KeyStringPair();
            var comparand = new KeyStringPair();

            Assert.False(obj != comparand);
        }

        [Fact]
        public void op_Equals_KeyStringPair()
        {
            var obj = new KeyStringPair();

            Assert.True(new KeyStringPair().Equals(obj));
        }

        [Fact]
        public void op_Equals_object()
        {
            object obj = new KeyStringPair();

            Assert.True(new KeyStringPair().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectDiffers()
        {
            var obj = new KeyStringPair("key", "value");

            Assert.False(new KeyStringPair().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectInvalidCast()
        {
            var obj = new Uri("http://example.com/");

            Assert.Throws<InvalidCastException>(() => new KeyStringPair().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new KeyStringPair().Equals(null));
        }

        [Fact]
        public void op_GetHashCode()
        {
            var expected = string.Empty.GetHashCode() ^ string.Empty.GetHashCode();
            var actual = new KeyStringPair().GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetObjectData_SerializationInfoNull_StreamingContext()
        {
            var context = new StreamingContext(StreamingContextStates.All);

            ISerializable value = new KeyStringPair();

            Assert.Throws<ArgumentNullException>(() => value.GetObjectData(null, context));
        }

        [Fact]
        public void op_GetObjectData_SerializationInfo_StreamingContext()
        {
            var info = new SerializationInfo(typeof(KeyStringPair), new FormatterConverter());
            var context = new StreamingContext(StreamingContextStates.All);

            ISerializable value = new KeyStringPair("foo", "bar");

            value.GetObjectData(info, context);

            Assert.Equal("foo", info.GetString("_key"));
            Assert.Equal("bar", info.GetString("_value"));
        }

        [Fact]
        public void op_ToString()
        {
            var expected = "foo{0}---{0}bar".FormatWith(Environment.NewLine);
            var actual = new KeyStringPair("foo", "bar").ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Key()
        {
            Assert.True(new PropertyExpectations<KeyStringPair>(p => p.Key)
                            .IsAutoProperty<string>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<KeyStringPair>(p => p.Value)
                            .IsAutoProperty<string>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}