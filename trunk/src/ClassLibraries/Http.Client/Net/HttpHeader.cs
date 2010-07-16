namespace Cavity.Net
{
    using System;
    using System.Globalization;

    public sealed class HttpHeader : ComparableObject, IHttpHeader
    {
        private Token _name;

        private string _value;

        public HttpHeader(Token name, string value)
            : this()
        {
            Name = name;
            Value = value;
        }

        private HttpHeader()
        {
        }

        public Token Name
        {
            get
            {
                return _name;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                _name = value;
            }
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

                _value = value;
            }
        }

        public static implicit operator HttpHeader(string value)
        {
            return ReferenceEquals(null, value)
                       ? null
                       : FromString(value);
        }

        public static HttpHeader FromString(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }
            else if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            var index = value.IndexOf(':');
            if (1 > index)
            {
                throw new FormatException("value");
            }

            return new HttpHeader(value.Substring(0, index), value.Substring(index + 1).Trim());
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}: {1}", Name, Value);
        }
    }
}