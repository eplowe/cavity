namespace Cavity.Models
{
    using System;
    using System.Linq;

    public sealed class Telephone : ComparableObject
    {
        private Telephone()
        {
        }

        private Telephone(string number)
        {
            Number = number;
        }

        public string Number { get; set; }

        public static implicit operator Telephone(string value)
        {
            return ReferenceEquals(null, value) ? null : FromString(value);
        }

        public static Telephone FromString(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            Telephone result = null;

            if (0 != value.Length)
            {
                var number = new string(value.AsEnumerable().Where(c => char.IsDigit(c)).ToArray());
                if (1 < number.Length)
                {
                    if ('+' == value[0])
                    {
                        number = "+" + number;
                    }
                    else if (number.StartsWith("00", StringComparison.Ordinal))
                    {
                        number = "+" + number.Substring(2);
                    }
                    else if ('0' == number[0])
                    {
                        number = "+44" + number.Substring(1);
                    }
                    else
                    {
                        number = "+44" + number;
                    }

                    result = new Telephone(number);
                }
            }

            return result ?? new Telephone();
        }

        public override string ToString()
        {
            return Number ?? string.Empty;
        }
    }
}