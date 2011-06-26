namespace Cavity.Models
{
    using System;
    using System.Globalization;
    using Cavity.Data;

    public struct AddressNumber : IComparable, IComparable<AddressNumber>
    {
        public AddressNumber(string key,
                             string value)
            : this()
        {
            Parts = new KeyStringPair(key, value);
        }

        public string Value
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "{0}{1}", Parts.Key, Parts.Value);
            }
        }

        private KeyStringPair Parts { get; set; }

        public static bool operator ==(AddressNumber operand1,
                                       AddressNumber operand2)
        {
            return 0 == Compare(operand1, operand2);
        }

        public static bool operator >(AddressNumber operand1,
                                      AddressNumber operand2)
        {
            return Compare(operand1, operand2) > 0;
        }

        public static implicit operator string(AddressNumber value)
        {
            return value.ToString();
        }

        public static implicit operator AddressNumber(string value)
        {
            return FromString(value);
        }

        public static bool operator !=(AddressNumber operand1,
                                       AddressNumber operand2)
        {
            return 0 != Compare(operand1, operand2);
        }

        public static bool operator <(AddressNumber operand1,
                                      AddressNumber operand2)
        {
            return Compare(operand1, operand2) < 0;
        }

        public static int Compare(AddressNumber comparand1,
                                  AddressNumber comparand2)
        {
#if NET20
            var left = StringExtensionMethods.TryToInt32(comparand1.Parts.Key);
            var right = StringExtensionMethods.TryToInt32(comparand2.Parts.Key);
#else
            var left = comparand1.Parts.Key.TryToInt32();
            var right = comparand2.Parts.Key.TryToInt32();
#endif
            var key = left.HasValue && right.HasValue
                          ? left.Value - right.Value
                          : string.Compare(comparand1.Parts.Key, comparand2.Parts.Key, StringComparison.Ordinal);
            if (0 != key)
            {
                return key;
            }

#if NET20
            left = StringExtensionMethods.TryToInt32(comparand1.Parts.Value);
            right = StringExtensionMethods.TryToInt32(comparand2.Parts.Value);
#else
            left = comparand1.Parts.Value.TryToInt32();
            right = comparand2.Parts.Value.TryToInt32();
#endif
            return left.HasValue && right.HasValue
                          ? left.Value - right.Value
                          : string.Compare(comparand1.Parts.Value, comparand2.Parts.Value, StringComparison.Ordinal);
        }

        public static AddressNumber FromString(string expression)
        {
            if (null == expression)
            {
                throw new ArgumentNullException("expression");
            }

            if (0 == expression.Length)
            {
                return new AddressNumber();
            }

            var digit = char.IsDigit(expression[0]);
            var index = 0;
            foreach (var c in expression)
            {
                if (digit == char.IsDigit(c))
                {
                    index++;
                    continue;
                }

                break;
            }

            return 0 == index
                       ? new AddressNumber(expression, null)
                       : new AddressNumber(expression.Substring(0, index), expression.Substring(index));
        }

        public override bool Equals(object obj)
        {
            var result = false;

            if (obj is AddressNumber)
            {
                result = 0 == Compare(this, (AddressNumber)obj);
            }

            return result;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }

        public int CompareTo(AddressNumber other)
        {
            return Compare(this, other);
        }

        int IComparable.CompareTo(object obj)
        {
            var comparison = 1;

            if (!ReferenceEquals(null, obj))
            {
                if (obj is AddressNumber)
                {
                    comparison = Compare(this, (AddressNumber)obj);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("obj");
                }
            }

            return comparison;
        }
    }
}