namespace Cavity.Models
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    using Xunit;
    using Xunit.Extensions;

    public sealed class PostalAddressFileKeyFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<PostalAddressFileKey>()
                            .IsValueType()
                            .IsDecoratedWith<ImmutableObjectAttribute>()
                            .Implements<IComparable<PostalAddressFileKey>>()
                            .Implements<IEquatable<PostalAddressFileKey>>()
                            .Serializable()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new PostalAddressFileKey());
        }

        [Fact]
        public void ctor_AlphaDecimal_AlphaDecimal()
        {
            Assert.NotNull(new PostalAddressFileKey(123, 456));
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            PostalAddressFileKey expected = "3f·0";
            PostalAddressFileKey actual;

            using (Stream stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, PostalAddressFileKey.FromString("3f·0"));
                stream.Position = 0;
                actual = (PostalAddressFileKey)formatter.Deserialize(stream);
            }

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true, "0·0", "0·0")]
        [InlineData(false, "3f·0", "0·0")]
        [InlineData(true, "3f·0", "3f·0")]
        [InlineData(false, "0·0", "3f·0")]
        public void opEquality_PostalAddressFileKey_PostalAddressFileKey(bool expected,
                                                                         string value,
                                                                         string comparand)
        {
            var a = PostalAddressFileKey.FromString(value);
            var b = PostalAddressFileKey.FromString(comparand);
            var actual = a == b;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(false, "0·0", "0·0")]
        [InlineData(false, "3f·a", "3f·a")]
        [InlineData(true, "3f·0", "0·0")]
        [InlineData(false, "0·0", "3f·0")]
        [InlineData(true, "3f·a", "3f·0")]
        [InlineData(false, "3f·0", "3f·a")]
        public void opGreaterThan_PostalAddressFileKey_PostalAddressFileKey(bool expected,
                                                                            string operand1,
                                                                            string operand2)
        {
            var actual = PostalAddressFileKey.FromString(operand1) > PostalAddressFileKey.FromString(operand2);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("0·0")]
        [InlineData("3f·0")]
        [InlineData("3f·co")]
        public void opImplicit_PostalAddressFileKey_string(string value)
        {
            var expected = PostalAddressFileKey.FromString(value);
            PostalAddressFileKey actual = value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_string_PostalAddressFileKey()
        {
            const string expected = "0·0";
            string actual = new PostalAddressFileKey();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(false, "0·0", "0·0")]
        [InlineData(true, "3f·0", "0·0")]
        [InlineData(false, "3f·0", "3f·0")]
        [InlineData(true, "0·0", "3f·0")]
        public void opInequality_PostalAddressFileKey_PostalAddressFileKey(bool expected,
                                                                           string value,
                                                                           string comparand)
        {
            var a = PostalAddressFileKey.FromString(value);
            var b = PostalAddressFileKey.FromString(comparand);
            var actual = a != b;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(false, "0·0", "0·0")]
        [InlineData(false, "3f·a", "3f·a")]
        [InlineData(false, "3f·0", "0·0")]
        [InlineData(true, "0·0", "3f·0")]
        [InlineData(false, "3f·a", "3f·0")]
        [InlineData(true, "3f·0", "3f·a")]
        public void opLessThan_PostalAddressFileKey_PostalAddressFileKey(bool expected,
                                                                         string operand1,
                                                                         string operand2)
        {
            var actual = PostalAddressFileKey.FromString(operand1) < PostalAddressFileKey.FromString(operand2);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "0·0", "0·0")]
        [InlineData(0, "3f·a", "3f·a")]
        [InlineData(1, "3f·0", "0·0")]
        [InlineData(-1, "0·0", "3f·0")]
        [InlineData(1, "3f·a", "3f·0")]
        [InlineData(-1, "3f·0", "3f·a")]
        public void op_CompareTo_PostalAddressFileKey(int expected,
                                                      string operand1,
                                                      string operand2)
        {
            var actual = PostalAddressFileKey.FromString(operand1).CompareTo(operand2);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "0·0", "0·0")]
        [InlineData(0, "3f·a", "3f·a")]
        [InlineData(1, "3f·0", "0·0")]
        [InlineData(-1, "0·0", "3f·0")]
        [InlineData(1, "3f·a", "3f·0")]
        [InlineData(-1, "3f·0", "3f·a")]
        public void op_Compare_PostalAddressFileKey_PostalAddressFileKey(int expected,
                                                                         string operand1,
                                                                         string operand2)
        {
            var actual = PostalAddressFileKey.Compare(operand1, operand2);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true, "0·0", "0·0")]
        [InlineData(false, "3f·0", "0·0")]
        [InlineData(true, "3f·0", "3f·0")]
        [InlineData(false, "0·0", "3f·0")]
        public void op_Equals_PostalAddressFileKey(bool expected,
                                                   string value,
                                                   string comparand)
        {
            var a = PostalAddressFileKey.FromString(value);
            var b = PostalAddressFileKey.FromString(comparand);
            var actual = a.Equals(b);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true, "0·0", "0·0")]
        [InlineData(false, "3f·0", "0·0")]
        [InlineData(true, "3f·0", "3f·0")]
        [InlineData(false, "0·0", "3f·0")]
        public void op_Equals_object(bool expected,
                                     string value,
                                     string comparand)
        {
            var a = PostalAddressFileKey.FromString(value);
            object b = PostalAddressFileKey.FromString(comparand);
            var actual = a.Equals(b);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 0, "0·0")]
        [InlineData(123, 0, "3f")]
        [InlineData(123, 0, "3f·")]
        [InlineData(123, 0, "3f·0")]
        [InlineData(123, 456, "3f·co")]
        public void op_FromString_string(int urn,
                                         int umr,
                                         string value)
        {
            var actual = PostalAddressFileKey.FromString(value);

            Assert.Equal(urn, actual.UniqueDeliveryPointReferenceNumber);
            Assert.Equal(umr, actual.UniqueMultipleResidenceReferenceNumber);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public void op_FromString_stringEmpty(string value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => PostalAddressFileKey.FromString(value));
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => PostalAddressFileKey.FromString(null));
        }

        [Fact]
        public void op_GetHashCode()
        {
            var expected = "0·0".GetHashCode();
            var actual = new PostalAddressFileKey().GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetObjectData_SerializationInfoNull_StreamingContext()
        {
            var context = new StreamingContext(StreamingContextStates.All);

            ISerializable value = PostalAddressFileKey.FromString("3f·0");

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => value.GetObjectData(null, context));

            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Fact]
        public void op_GetObjectData_SerializationInfo_StreamingContext()
        {
            var info = new SerializationInfo(typeof(PostalAddressFileKey), new FormatterConverter());
            var context = new StreamingContext(StreamingContextStates.All);

            const string urn = "3f";
            const string umr = "0";

            ISerializable value = PostalAddressFileKey.FromString("{0}·{1}".FormatWith(urn, umr));

            value.GetObjectData(info, context);

            Assert.Equal(urn, info.GetString("_urn"));
            Assert.Equal(umr, info.GetString("_umr"));
        }

        [Theory]
        [InlineData("0·0", 0, 0)]
        [InlineData("3f·0", 123, 0)]
        [InlineData("3f·co", 123, 456)]
        public void op_ToString(string expected,
                                int urn,
                                int umr)
        {
            var actual = new PostalAddressFileKey(urn, umr).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_UniqueDeliveryPointReferenceNumber()
        {
            Assert.True(new PropertyExpectations<PostalAddressFileKey>(x => x.UniqueDeliveryPointReferenceNumber)
                            .IsAutoProperty<AlphaDecimal>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_UniqueMultipleResidenceReferenceNumber()
        {
            Assert.True(new PropertyExpectations<PostalAddressFileKey>(x => x.UniqueMultipleResidenceReferenceNumber)
                            .IsAutoProperty<AlphaDecimal>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}