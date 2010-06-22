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
            this.Name = name;
            this.Value = value;
        }

        private HttpHeader()
        {
        }

        public Token Name
        {
            get
            {
                return this._name;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this._name = value;
            }
        }

        public string Value
        {
            get
            {
                return this._value;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this._value = value;
            }
        }

        public static implicit operator HttpHeader(string value)
        {
            return object.ReferenceEquals(null, value) ? null as HttpHeader : HttpHeader.Parse(value);
        }

        public static HttpHeader Parse(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }
            else if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            int index = value.IndexOf(':');
            if (1 > index)
            {
                throw new FormatException("value");
            }

            return new HttpHeader(value.Substring(0, index), value.Substring(index + 1).Trim());
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}: {1}", this.Name, this.Value);
        }
    }
}