namespace Cavity
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Cryptography;
    using System.Security.Permissions;

    /// <summary>
    /// Represents an integer with a radix of 36.
    /// </summary>
    /// <remarks>
    /// <see href="http://wikipedia.org/wiki/Base_36">Base 36</see>
    /// </remarks>
    [Serializable]
    public struct AlphaDecimal : ISerializable,
                                 IConvertible,
                                 IComparable,
                                 IComparable<AlphaDecimal>,
                                 IEquatable<AlphaDecimal>
    {
        private const string Chars = "0123456789abcdefghijklmnopqrstuvwxyz";

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

        private AlphaDecimal(SerializationInfo info,
                             StreamingContext context)
            : this()
        {
            Value = Parse(info.GetString("_value"));
        }

        public static AlphaDecimal MaxValue
        {
            get
            {
                return long.MaxValue;
            }
        }

        public static AlphaDecimal MinValue
        {
            get
            {
                return long.MinValue;
            }
        }

        public static AlphaDecimal Zero
        {
            get
            {
                return 0;
            }
        }

        public AlphaDecimal Abs
        {
            get
            {
                return Math.Abs(Value);
            }
        }

        private long Value { get; set; }

        public static AlphaDecimal operator +(AlphaDecimal operand1,
                                              AlphaDecimal operand2)
        {
            return operand1.Value + operand2.Value;
        }

        public static AlphaDecimal operator --(AlphaDecimal operand)
        {
            return operand.Value - 1;
        }

        public static AlphaDecimal operator /(AlphaDecimal operand1,
                                              AlphaDecimal operand2)
        {
            return operand1.Value / operand2.Value;
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
            return value.Value;
        }

        public static implicit operator AlphaDecimal(long value)
        {
            return new AlphaDecimal(value);
        }

        public static implicit operator string(AlphaDecimal value)
        {
            return value.ToString();
        }

        public static AlphaDecimal operator ++(AlphaDecimal operand)
        {
            return operand.Value + 1;
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
            return operand1.Value % operand2.Value;
        }

        public static AlphaDecimal operator *(AlphaDecimal operand1,
                                              AlphaDecimal operand2)
        {
            return operand1.Value * operand2.Value;
        }

        public static AlphaDecimal operator -(AlphaDecimal operand1,
                                              AlphaDecimal operand2)
        {
            return operand1.Value - operand2.Value;
        }

        public static long Compare(AlphaDecimal operand1,
                                   AlphaDecimal operand2)
        {
            return operand1 - operand2;
        }

        public static AlphaDecimal FromString(string expression)
        {
            return Parse(expression);
        }

        public static AlphaDecimal Random()
        {
            var buffer = new byte[8];
            new RNGCryptoServiceProvider().GetBytes(buffer);

            return BitConverter.ToInt64(buffer, 0);
        }

        public void Add(AlphaDecimal value)
        {
            Value += value;
        }

        public void Decrement()
        {
            Value--;
        }

        public void Divide(AlphaDecimal value)
        {
            Value /= value;
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && Equals((AlphaDecimal)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public void Increment()
        {
            Value++;
        }

        public AlphaDecimal Mod(AlphaDecimal value)
        {
            return this % value;
        }

        public void Multiply(AlphaDecimal value)
        {
            Value *= value;
        }

        public void Subtract(AlphaDecimal value)
        {
            Value -= value;
        }

        public override string ToString()
        {
            string buffer = null;
            var remainder = Math.Abs(Value);

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
            return Convert.ToBoolean((long)this);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte((long)this);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar((long)this);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return new DateTime(this);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal((long)this);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble((long)this);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16((long)this);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32((long)this);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64((long)this);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte((long)this);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle((long)this);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            return ToString();
        }

        object IConvertible.ToType(Type conversionType,
                                   IFormatProvider provider)
        {
            return Convert.ChangeType((long)this, conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16((long)this);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32((long)this);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64((long)this);
        }

        public bool Equals(AlphaDecimal other)
        {
            return Value == other.Value;
        }

#if !NET40
        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.SerializationFormatter)]
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

        private static long Parse(string expression)
        {
            if (null == expression)
            {
                throw new ArgumentNullException("expression");
            }

            if (0 == expression.Length)
            {
                throw new ArgumentOutOfRangeException("expression");
            }

            long value = 0;
            var radix = 0;

            var negative = false;
            if (expression.StartsWith("-", StringComparison.OrdinalIgnoreCase))
            {
                negative = true;
                expression = expression.Substring(1);
            }

            for (var i = expression.Length - 1; i > -1; i--)
            {
                if (!Chars.Contains(expression[i].ToString()))
                {
                    throw new FormatException("A base-36 string can only contain characters in the range [0-9] and [a-z].");
                }

                value += Chars.IndexOf(expression[i]) * _powers[radix++];
            }

            return negative ? (0 - value) : value;
        }
    }
}