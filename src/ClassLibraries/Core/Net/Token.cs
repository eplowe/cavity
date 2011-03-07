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

                if (0 == value.Length)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                if (0 != value.ToUpperInvariant()
                             .ToArray()
                             .Where(x => ' ' >= x ||
                                         x.EqualsOneOf('"', '@', '(', ')', ',', '/', ':', ';', '<', '=', '>', '?', '[', '\\', ']', '{', '}') ||
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