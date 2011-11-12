namespace Cavity
{
    using System;
#if NET40
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
#endif
    using System.Runtime.Serialization;
#if NET20 || NET35
    using System.Security.Cryptography;
    using System.Security.Permissions;
#endif

    /// <summary>
    /// Represents an integer with a radix of 36.
    /// </summary>
    /// <remarks>
    /// Wikipedia: <see href="http://wikipedia.org/wiki/Base_36">Base 36</see>.
    /// </remarks>
    [Serializable]
    public struct AlphaDecimal : ISerializable,
                                 IConvertible,
                                 IComparable,
                                 IComparable<AlphaDecimal>,
                                 IEquatable<AlphaDecimal>
    {
        private const string Chars = "0123456789abcdefghijklmnopqrstuvwxyz";

#if NET20 || NET35
        private static readonly long[] _powers =
            {
                1,
                36,
                1296,
                46656,
                1679616,
                60466176,
                2176782336,
                78364164096,
                2821109907456,
                101559956668416,
                3656158440062976,
                131621703842267136,
                4738381338321616896
            };

        private AlphaDecimal(long value)
            : this()
        {
            Value = value;
        }
#else
        private AlphaDecimal(BigInteger value)
            : this()
        {
            Value = value;
        }
#endif

        private AlphaDecimal(SerializationInfo info,
                             StreamingContext context)
            : this()
        {
            Value = Parse(info.GetString("_value"));
        }

        public static AlphaDecimal Zero
        {
            get
            {
#if NET20 || NET35
                return 0;
#else
                return BigInteger.Zero;
#endif
            }
        }

        public AlphaDecimal Abs
        {
            get
            {
#if NET20 || NET35
                return Math.Abs(Value);
#else
                return BigInteger.Abs(Value);
#endif
            }
        }

#if NET20 || NET35
        private long Value { get; set; }
#else
        private BigInteger Value { get; set; }
#endif

        public static AlphaDecimal operator +(AlphaDecimal operand1,
                                              AlphaDecimal operand2)
        {
            return operand1.Add(operand2);
        }

        public static AlphaDecimal operator --(AlphaDecimal operand)
        {
            return operand.Decrement();
        }

        public static AlphaDecimal operator /(AlphaDecimal operand1,
                                              AlphaDecimal operand2)
        {
            return operand1.Divide(operand2);
        }

        public static bool operator ==(AlphaDecimal obj,
                                       AlphaDecimal comparand)
        {
            return obj.Equals(comparand);
        }

        public static bool operator >(AlphaDecimal operand1,
                                      AlphaDecimal operand2)
        {
            return operand1.Value > operand2.Value;
        }

        public static implicit operator long(AlphaDecimal value)
        {
            return (long)value.Value;
        }

        public static implicit operator AlphaDecimal(long value)
        {
            return new AlphaDecimal(value);
        }

#if NET40
        public static implicit operator BigInteger(AlphaDecimal value)
        {
            return value.Value;
        }

        public static implicit operator AlphaDecimal(BigInteger value)
        {
            return new AlphaDecimal(value);
        }

        public static implicit operator AlphaDecimal(Guid value)
        {
            return new AlphaDecimal(new BigInteger(value.ToByteArray()));
        }
#endif

        public static implicit operator string(AlphaDecimal value)
        {
            return value.ToString();
        }

        public static AlphaDecimal operator ++(AlphaDecimal operand)
        {
            return operand.Increment();
        }

        public static bool operator !=(AlphaDecimal obj,
                                       AlphaDecimal comparand)
        {
            return !obj.Equals(comparand);
        }

        public static bool operator <(AlphaDecimal operand1,
                                      AlphaDecimal operand2)
        {
            return operand1.Value < operand2.Value;
        }

        public static AlphaDecimal operator %(AlphaDecimal operand1,
                                              AlphaDecimal operand2)
        {
            return operand1.Mod(operand2);
        }

        public static AlphaDecimal operator *(AlphaDecimal operand1,
                                              AlphaDecimal operand2)
        {
            return operand1.Multiply(operand2);
        }

        public static AlphaDecimal operator -(AlphaDecimal operand1,
                                              AlphaDecimal operand2)
        {
            return operand1.Subtract(operand2);
        }

#if NET20 || NET35
        public static long Compare(AlphaDecimal operand1,
                                   AlphaDecimal operand2)
        {
            return operand1 - operand2;
        }
#else
        public static BigInteger Compare(AlphaDecimal operand1,
                                         AlphaDecimal operand2)
        {
            return operand1 - operand2;
        }
#endif

        public static AlphaDecimal FromString(string expression)
        {
            return Parse(expression);
        }

        public static AlphaDecimal Random()
        {
#if NET20 || NET35
            var buffer = new byte[8];
            new RNGCryptoServiceProvider().GetBytes(buffer);

            return BitConverter.ToInt64(buffer, 0);
#else
            return new BigInteger(Guid.NewGuid().ToByteArray());
#endif
        }

        public AlphaDecimal Add(AlphaDecimal value)
        {
            return Value + value.Value;
        }

        public AlphaDecimal Decrement()
        {
            return Value - 1;
        }

        public AlphaDecimal Divide(AlphaDecimal value)
        {
            return Value / value.Value;
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && Equals((AlphaDecimal)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public AlphaDecimal Increment()
        {
            return Value + 1;
        }

        public AlphaDecimal Mod(AlphaDecimal value)
        {
            return Value % value.Value;
        }

        public AlphaDecimal Multiply(AlphaDecimal value)
        {
            return Value * value.Value;
        }

        public AlphaDecimal Subtract(AlphaDecimal value)
        {
            return Value - value.Value;
        }

        public override string ToString()
        {
            string buffer = null;
#if NET20 || NET35
            var remainder = Math.Abs(Value);
#else
            var remainder = BigInteger.Abs(Value);
#endif

            while (remainder > 35)
            {
                var index = remainder % 36;
                buffer = Chars[(int)index] + buffer;
                remainder /= 36;
            }

            return string.Concat(
                Value < 0 ? "-" : string.Empty,
                Chars[(int)remainder],
                buffer);
        }

        public int CompareTo(object obj)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return CompareTo((AlphaDecimal)obj);
        }

        public int CompareTo(AlphaDecimal other)
        {
            return (int)Compare(this, other);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Int64;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean((long)Value);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte((long)Value);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar((long)Value);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return new DateTime((long)Value);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal((long)Value);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble((long)Value);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16((long)Value);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32((long)Value);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64((long)Value);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte((long)Value);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle((long)Value);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            return ToString();
        }

        object IConvertible.ToType(Type conversionType,
                                   IFormatProvider provider)
        {
            return Convert.ChangeType((long)Value, conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16((long)Value);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32((long)Value);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64((long)Value);
        }

        public bool Equals(AlphaDecimal other)
        {
            return Value == other.Value;
        }

#if NET20 || NET35
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
#endif

        void ISerializable.GetObjectData(SerializationInfo info,
                                         StreamingContext context)
        {
            if (null == info)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("_value", ToString());
        }

#if NET20 || NET35
        private static long Parse(string expression)
#else
        private static BigInteger Parse(string expression)
#endif
        {
            if (null == expression)
            {
                throw new ArgumentNullException("expression");
            }

            if (0 == expression.Length)
            {
                throw new ArgumentOutOfRangeException("expression");
            }

            var radix = 0;

            var negative = false;
            if (expression.StartsWith("-", StringComparison.OrdinalIgnoreCase))
            {
                negative = true;
                expression = expression.Substring(1);
            }

#if NET20 || NET35
            long value = 0;
#else
            BigInteger value = 0;
            var powers = new List<BigInteger>
            {
                1,
                36,
                1296,
                46656,
                1679616,
                60466176,
                2176782336,
                78364164096,
                2821109907456,
                101559956668416,
                3656158440062976,
                131621703842267136,
                4738381338321616896
            };
            for (var i = 1; i < expression.Length + 1; i++)
            {
                if (i > powers.Count)
                {
                    powers.Add(powers.Last() * 36);
                }
            }
#endif

            for (var i = expression.Length - 1; i > -1; i--)
            {
                if (!Chars.Contains(expression[i].ToString()))
                {
                    throw new FormatException("A base-36 string can only contain characters in the range [0-9] and [a-z].");
                }

#if NET20 || NET35
                value += Chars.IndexOf(expression[i]) * _powers[radix++];
#else
                value += Chars.IndexOf(expression[i]) * powers[radix++];
#endif
            }

            return negative ? (0 - value) : value;
        }
    }
}