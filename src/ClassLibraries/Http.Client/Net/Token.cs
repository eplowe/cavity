namespace Cavity.Net
{
    using System;
    using System.Linq;
    using Cavity.Properties;

    public sealed class Token : ComparableObject
    {
        private string _value;

        public Token(string value)
            : this()
        {
            Value = value;
        }

        private Token()
        {
        }

        public string Value
        {
            get
            {
                return _value;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }
                else if (0 == value.Length)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                else if (0 != value
                                  .ToUpperInvariant()
                                  .ToArray()
                                  .Where(x =>
                                         ' ' >= x || // 32
                                         '"' == x || // 34
                                         '@' == x || // 38
                                         '(' == x || // 40
                                         ')' == x || // 41
                                         ',' == x || // 44
                                         '/' == x || // 47
                                         ':' == x || // 58
                                         ';' == x || // 59
                                         '<' == x || // 60
                                         '=' == x || // 61
                                         '>' == x || // 62
                                         '?' == x || // 63
                                         '@' == x || // 64
                                         '[' == x || // 91
                                         '\\' == x || // 92
                                         ']' == x || // 93
                                         '{' == x || // 123
                                         '}' == x || // 125
                                         (char)127 <= x) // DEL
                                  .Count())
                {
                    throw new FormatException(Resources.Token_FormatException_Message);
                }

                _value = value;
            }
        }

        public static implicit operator Token(string value)
        {
            return ReferenceEquals(null, value)
                       ? null
                       : new Token(value);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}