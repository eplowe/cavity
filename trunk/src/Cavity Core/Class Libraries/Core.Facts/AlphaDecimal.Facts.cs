namespace Cavity
{
    using System;
    using System.Globalization;
    using System.IO;
#if NET40
    using System.Numerics;
#endif
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Xml;
    using Xunit;

    public sealed class AlphaDecimalFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<AlphaDecimal>()
                            .IsValueType()
                            .Implements<ISerializable>()
                            .Implements<IConvertible>()
                            .Implements<IComparable>()
                            .Implements<IComparable<AlphaDecimal>>()
                            .Implements<IEquatable<AlphaDecimal>>()
                            .Serializable()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new AlphaDecimal());
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            AlphaDecimal expected = 123;
            AlphaDecimal actual;

            using (Stream stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, (AlphaDecimal)123);
                stream.Position = 0;
                actual = (AlphaDecimal)formatter.Deserialize(stream);
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opAddition_AlphaDecimal_AlphaDecimal()
        {
            AlphaDecimal one = 1;
            AlphaDecimal two = 2;

            AlphaDecimal expected = 3;
            var actual = one + two;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opDecrement_AlphaDecimal()
        {
            AlphaDecimal actual = 2;

            AlphaDecimal expected = 1;
            actual--;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opDivision_AlphaDecimal_AlphaDecimal()
        {
            AlphaDecimal two = 2;
            AlphaDecimal six = 6;

            AlphaDecimal expected = 3;
            var actual = six / two;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opEquality_AlphaDecimal_AlphaDecimal()
        {
            var obj = new AlphaDecimal();
            var comparand = new AlphaDecimal();

            Assert.True(obj == comparand);
        }

        [Fact]
        public void opGreater_AlphaDecimal_AlphaDecimal()
        {
            AlphaDecimal one = 1;
            AlphaDecimal two = 2;

            Assert.True(two > one);
        }

        [Fact]
        public void opImplicit_AlphaDecimal_int()
        {
            const int value = 0;

            var expected = new AlphaDecimal();
            AlphaDecimal actual = value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_AlphaDecimal_long()
        {
            const long value = 0;

            var expected = new AlphaDecimal();
            AlphaDecimal actual = value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_AlphaDecimal_short()
        {
            const short value = 0;

            var expected = new AlphaDecimal();
            AlphaDecimal actual = value;

            Assert.Equal(expected, actual);
        }

#if NET40
        [Fact]
        public void opImplicit_AlphaDecimal_BigInteger()
        {
            var value = BigInteger.Zero;

            var expected = new AlphaDecimal();
            AlphaDecimal actual = value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_AlphaDecimal_Guid()
        {
            var value = new Guid("f71ae311-462f-4f65-a704-8eb9fe49a679");

            AlphaDecimal expected = new BigInteger(value.ToByteArray());
            AlphaDecimal actual = value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_AlphaDecimal_GuidEmpty()
        {
            var expected = new AlphaDecimal();
            AlphaDecimal actual = Guid.Empty;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_BigInteger_AlphaDecimal()
        {
            var expected = new BigInteger(123);

            AlphaDecimal value = expected;

            BigInteger actual = value;

            Assert.Equal(expected, actual);
        }
#endif

        [Fact]
        public void opImplicit_long_AlphaDecimal()
        {
            const long expected = 123;

            AlphaDecimal value = expected;

            long actual = value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_string_AlphaDecimal()
        {
            const string expected = "zik0zj";

            AlphaDecimal value = int.MaxValue;
            string actual = value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opIncrement_AlphaDecimal()
        {
            AlphaDecimal actual = 1;

            AlphaDecimal expected = 2;
            actual++;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opInequality_AlphaDecimal_AlphaDecimal()
        {
            var obj = new AlphaDecimal();
            var comparand = new AlphaDecimal();

            Assert.False(obj != comparand);
        }

        [Fact]
        public void opLesser_AlphaDecimal_AlphaDecimal()
        {
            AlphaDecimal one = 1;
            AlphaDecimal two = 2;

            Assert.True(one < two);
        }

        [Fact]
        public void opModulus_AlphaDecimal_AlphaDecimal()
        {
            AlphaDecimal two = 2;
            AlphaDecimal six = 6;

            AlphaDecimal expected = 2;
            var actual = two % six;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opMultiplication_AlphaDecimal_AlphaDecimal()
        {
            AlphaDecimal two = 2;
            AlphaDecimal six = 6;

            AlphaDecimal expected = 12;
            var actual = two * six;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opSubtraction_AlphaDecimal_AlphaDecimal()
        {
            AlphaDecimal one = 1;
            AlphaDecimal three = 3;

            AlphaDecimal expected = 2;
            var actual = three - one;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Add_AlphaDecimal()
        {
            AlphaDecimal one = 1;
            AlphaDecimal two = 2;

            AlphaDecimal expected = 3;
            var actual = one.Add(two);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_AlphaDecimal()
        {
            AlphaDecimal two = 2;
            AlphaDecimal six = 6;

            const long expected = -4;
            var actual = two.CompareTo(six);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_object()
        {
            AlphaDecimal two = 2;
            object six = (AlphaDecimal)6;

            const long expected = -4;
            var actual = two.CompareTo(six);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectInvalidCast()
        {
            var obj = new Uri("http://example.com/");

            Assert.Throws<InvalidCastException>(() => new AlphaDecimal().CompareTo(obj));
        }

        [Fact]
        public void op_CompareTo_objectNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AlphaDecimal().CompareTo(null));
        }

        [Fact]
        public void op_Compare_AlphaDecimal3_AlphaDecimal6()
        {
            const long expected = -3;
            var actual = AlphaDecimal.Compare(3, 6);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compare_AlphaDecimal6_AlphaDecimal3()
        {
            const long expected = 3;
            var actual = AlphaDecimal.Compare(6, 3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compare_AlphaDecimal_AlphaDecimal()
        {
            const long expected = 0;
            var actual = AlphaDecimal.Compare(2, 2);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Decrement()
        {
            AlphaDecimal two = 2;

            AlphaDecimal expected = 1;
            var actual = two.Decrement();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Divide_AlphaDecimal()
        {
            AlphaDecimal two = 2;
            AlphaDecimal six = 6;

            AlphaDecimal expected = 3;
            var actual = six.Divide(two);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Equals_AlphaDecimal()
        {
            AlphaDecimal obj = 0;

            Assert.True(new AlphaDecimal().Equals(obj));
        }

        [Fact]
        public void op_Equals_object()
        {
            object obj = (AlphaDecimal)0;

            Assert.True(new AlphaDecimal().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectDiffers()
        {
            AlphaDecimal obj = 123;

            Assert.False(new AlphaDecimal().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectInvalidCast()
        {
            var obj = new Uri("http://example.com/");

            Assert.Throws<InvalidCastException>(() => new AlphaDecimal().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new AlphaDecimal().Equals(null));
        }

        [Fact]
        public void op_FromString_string()
        {
            const long expected = 0;
            long actual = AlphaDecimal.FromString("0");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => AlphaDecimal.FromString(string.Empty));
        }

        [Fact]
        public void op_FromString_stringIntMax()
        {
            const long expected = int.MaxValue;
            long actual = AlphaDecimal.FromString("zik0zj");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringIntMin()
        {
            const long expected = int.MinValue + 1;
            long actual = AlphaDecimal.FromString("-zik0zj");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringInvalid()
        {
            Assert.Throws<FormatException>(() => AlphaDecimal.FromString("!"));
        }

        [Fact]
        public void op_FromString_stringLongMax()
        {
            const long expected = long.MaxValue;
            long actual = AlphaDecimal.FromString("1y2p0ij32e8e7");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringLongMin()
        {
            const long expected = long.MinValue + 1;
            long actual = AlphaDecimal.FromString("-1y2p0ij32e8e7");

            Assert.Equal(expected, actual);
        }

#if NET40
        [Fact]
        public void op_FromString_stringBig()
        {
            var value = new Guid("f71ae311-462f-4f65-a704-8eb9fe49a679");

            AlphaDecimal expected = new BigInteger(value.ToByteArray());
            var actual = AlphaDecimal.FromString("779q4dambvi6xhpfak8ne5ekx");

            Assert.Equal(expected, actual);
        }
#endif

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => AlphaDecimal.FromString(null));
        }

        [Fact]
        public void op_FromString_stringZz()
        {
            const long expected = (36 * 36) - 1;
            long actual = AlphaDecimal.FromString("zz");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetHashCode()
        {
            var expected = 0.GetHashCode();
            var actual = new AlphaDecimal().GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetObjectData_SerializationInfoNull_StreamingContext()
        {
            var context = new StreamingContext(StreamingContextStates.All);

            ISerializable value = (AlphaDecimal)123;

// ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => value.GetObjectData(null, context));
// ReSharper restore AssignNullToNotNullAttribute
        }

        [Fact]
        public void op_GetObjectData_SerializationInfo_StreamingContext()
        {
            var info = new SerializationInfo(typeof(AlphaDecimal), new FormatterConverter());
            var context = new StreamingContext(StreamingContextStates.All);

            const string expected = "zik0zj";

            ISerializable value = (AlphaDecimal)int.MaxValue;

            value.GetObjectData(info, context);

            var actual = info.GetString("_value");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetTypeCode()
        {
            const TypeCode expected = TypeCode.Int64;

            IConvertible value = new AlphaDecimal();
            var actual = value.GetTypeCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Increment()
        {
            AlphaDecimal one = 1;

            AlphaDecimal expected = 2;
            var actual = one.Increment();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Mod_AlphaDecimal()
        {
            AlphaDecimal two = 2;
            AlphaDecimal six = 6;

            AlphaDecimal expected = 2;
            var actual = two.Mod(six);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Multiply_AlphaDecimal()
        {
            AlphaDecimal two = 2;
            AlphaDecimal six = 6;

            AlphaDecimal expected = 12;
            var actual = two.Multiply(six);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Random()
        {
            AlphaDecimal.Random();
        }

        [Fact]
        public void op_Subtract_AlphaDecimal()
        {
            AlphaDecimal one = 1;
            AlphaDecimal three = 3;

            AlphaDecimal expected = 2;
            var actual = three.Subtract(one);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToBoolean_IFormatProvider()
        {
            const bool expected = true;

            IConvertible value = (AlphaDecimal)1;
            var actual = value.ToBoolean(CultureInfo.InvariantCulture);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToByte_IFormatProvider()
        {
            const byte expected = (byte)123;

            IConvertible value = (AlphaDecimal)123;
            var actual = value.ToByte(CultureInfo.InvariantCulture);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToChar_IFormatProvider()
        {
            const char expected = (char)123;

            IConvertible value = (AlphaDecimal)123;
            var actual = value.ToChar(CultureInfo.InvariantCulture);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToDateTime_IFormatProvider()
        {
            var expected = new DateTime(123);

            IConvertible value = (AlphaDecimal)123;
            var actual = value.ToDateTime(CultureInfo.InvariantCulture);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToDecimal_IFormatProvider()
        {
            const decimal expected = 123;

            IConvertible value = (AlphaDecimal)123;
            var actual = value.ToDecimal(CultureInfo.InvariantCulture);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToDouble_IFormatProvider()
        {
            const double expected = 123;

            IConvertible value = (AlphaDecimal)123;
            var actual = value.ToDouble(CultureInfo.InvariantCulture);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToInt16_IFormatProvider()
        {
            const short expected = 123;

            IConvertible value = (AlphaDecimal)123;
            var actual = value.ToInt16(CultureInfo.InvariantCulture);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToInt32_IFormatProvider()
        {
            const int expected = 123;

            IConvertible value = (AlphaDecimal)123;
            var actual = value.ToInt32(CultureInfo.InvariantCulture);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToInt64_IFormatProvider()
        {
            const long expected = 123;

            IConvertible value = (AlphaDecimal)123;
            var actual = value.ToInt64(CultureInfo.InvariantCulture);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToSByte_IFormatProvider()
        {
            const sbyte expected = 123;

            IConvertible value = (AlphaDecimal)123;
            var actual = value.ToSByte(CultureInfo.InvariantCulture);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToSingle_IFormatProvider()
        {
            const float expected = 123;

            IConvertible value = (AlphaDecimal)123;
            var actual = value.ToSingle(CultureInfo.InvariantCulture);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "0";
            var actual = new AlphaDecimal().ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString0to9()
        {
            for (var i = 0; i < 10; i++)
            {
                var expected = XmlConvert.ToString(i);

                AlphaDecimal value = i;
                var actual = value.ToString();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_ToString10()
        {
            const string expected = "10";

            AlphaDecimal value = 36;
            var actual = value.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString100()
        {
            const string expected = "100";

            AlphaDecimal value = 36 * 36;
            var actual = value.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToStringAtoZ()
        {
            for (var i = 10; i < 36; i++)
            {
                var expected = ((char)(i + 87)).ToString();

                AlphaDecimal value = i;
                var actual = value.ToString();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_ToStringIntMax()
        {
            const string expected = "zik0zj";

            AlphaDecimal value = int.MaxValue;
            var actual = value.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToStringIntMin()
        {
            const string expected = "-zik0zj";

            AlphaDecimal value = int.MinValue + 1;
            var actual = value.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToStringLongMax()
        {
            const string expected = "1y2p0ij32e8e7";

            AlphaDecimal value = long.MaxValue;
            var actual = value.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToStringLongMin()
        {
            const string expected = "-1y2p0ij32e8e7";

            AlphaDecimal value = long.MinValue + 1;
            var actual = value.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToStringZz()
        {
            const string expected = "zz";

            AlphaDecimal value = (36 * 36) - 1;
            var actual = value.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_IFormatProvider()
        {
            const string expected = "3f";

            IConvertible value = (AlphaDecimal)123;
            var actual = value.ToString(CultureInfo.InvariantCulture);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToType_Type_IFormatProvider()
        {
            const decimal expected = 123;

            IConvertible value = (AlphaDecimal)123;
            var actual = value.ToType(typeof(decimal), CultureInfo.InvariantCulture);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToUInt16_IFormatProvider()
        {
            const ushort expected = 123;

            IConvertible value = (AlphaDecimal)123;
            var actual = value.ToUInt16(CultureInfo.InvariantCulture);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToUInt32_IFormatProvider()
        {
            const uint expected = 123;

            IConvertible value = (AlphaDecimal)123;
            var actual = value.ToUInt32(CultureInfo.InvariantCulture);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToUInt64_IFormatProvider()
        {
            const ulong expected = 123;

            IConvertible value = (AlphaDecimal)123;
            var actual = value.ToUInt64(CultureInfo.InvariantCulture);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Abs()
        {
            AlphaDecimal expected = 1;

            AlphaDecimal value = -1;
            var actual = value.Abs;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Zero()
        {
            AlphaDecimal expected = 0;
            var actual = AlphaDecimal.Zero;

            Assert.Equal(expected, actual);
        }
    }
}