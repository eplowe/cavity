namespace Cavity.Net
{
    using System;
    using System.Linq;

    public sealed class HttpMethod : ValueObject<HttpMethod>
    {
        private string _value;

        public HttpMethod(string value)
            : this()
        {
            this.Value = value;
        }

        private HttpMethod()
        {
            this.RegisterProperty(x => x.Value);
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
                else if (0 == value.Length)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                else if (0 != value.ToUpperInvariant().ToArray().Where(x => 'A' > x || 'Z' < x).Count())
                {
                    throw new FormatException("value");
                }

                this._value = value;
            }
        }

        public static implicit operator HttpMethod(string value)
        {
            return object.ReferenceEquals(null, value) ? null as HttpMethod : new HttpMethod(value);
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}