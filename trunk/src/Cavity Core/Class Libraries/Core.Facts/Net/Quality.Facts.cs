namespace Cavity.Net
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    using Xunit;
    using Xunit.Extensions;

    public sealed class QualityFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Quality>().IsValueType()
                                                       .Implements<IEquatable<Quality>>()
                                                       .Serializable()
                                                       .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new Quality());
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            var expected = new Quality(0.23f);
            Quality actual;

            using (Stream stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                var obj = new Quality(0.23f);
                formatter.Serialize(stream, obj);
                stream.Position = 0;
                actual = (Quality)formatter.Deserialize(stream);
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_float()
        {
            Assert.NotNull(new Quality(0.75f));
        }

        [Fact]
        public void opEquality_Quality_Quality()
        {
            var obj = new Quality();
            var comparand = new Quality();

            Assert.True(obj == comparand);
        }

        [Fact]
        public void opImplicit_Quality_float()
        {
            var expected = new Quality(0.62f);
            Quality actual = 0.62f;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_Quality_string()
        {
            var expected = new Quality(0.234f);
            Quality actual = "0.234";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_float_Quality()
        {
            const float expected = 0.75f;
            float actual = new Quality(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_string_Quality()
        {
            const string expected = "0.432";
            string actual = new Quality(0.432f);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opInequality_Quality_Quality()
        {
            var obj = new Quality();
            var comparand = new Quality();

            Assert.False(obj != comparand);
        }

        [Fact]
        public void op_Equals_Quality()
        {
            var obj = new Quality();

            Assert.True(new Quality().Equals(obj));
        }

        [Fact]
        public void op_Equals_object()
        {
            object obj = new Quality();

            Assert.True(new Quality().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectDiffers()
        {
            var obj = new Quality(0.78f);

            Assert.False(new Quality().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectInvalidCast()
        {
            var obj = new Uri("http://example.com/");

            Assert.Throws<InvalidCastException>(() => new Quality().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new Quality().Equals(null as object));
        }

        [Fact]
        public void op_FromString_string()
        {
            var expected = new Quality(0.45f);
            var actual = Quality.FromString("0.45");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void op_FromString_stringEmpty(string value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Quality.FromString(value));
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => Quality.FromString(null));
        }

        [Fact]
        public void op_GetHashCode()
        {
            var expected = 0.36f.GetHashCode();
            var actual = new Quality(0.36f).GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetObjectData_SerializationInfoNull_StreamingContext()
        {
            var context = new StreamingContext(StreamingContextStates.All);

            ISerializable value = new Quality(0.852f);

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => value.GetObjectData(null, context));

            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Fact]
        public void op_GetObjectData_SerializationInfo_StreamingContext()
        {
            var info = new SerializationInfo(typeof(Quality), new FormatterConverter());
            var context = new StreamingContext(StreamingContextStates.All);

            const string expected = "0.468";

            ISerializable value = new Quality(0.468f);

            value.GetObjectData(info, context);

            var actual = info.GetString("_value");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "0.1";
            var actual = new Quality(0.1f).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_whenTrimmed()
        {
            const string expected = "0.123";
            var actual = new Quality(0.1234f).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "propOne", Justification = "This naming is intentional.")]
        public void prop_One_get()
        {
            var expected = new Quality(1);
            var actual = Quality.One;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<Quality>(x => x.Value)
                            .IsAutoProperty<float>()
                            .ArgumentOutOfRangeException(-0.01f)
                            .Set(0)
                            .Set(1)
                            .ArgumentOutOfRangeException(1.01f)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Zero_get()
        {
            var expected = new Quality(1);
            var actual = Quality.Zero;

            Assert.Equal(expected, actual);
        }
    }
}