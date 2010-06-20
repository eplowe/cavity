namespace Cavity.Net
{
    using System;
    using System.Linq;

    public sealed class HttpMethod : IComparable
    {
        private string _value;

        public HttpMethod(string value)
        {
            this.Value = value;
        }

        private HttpMethod()
        {
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

        public static implicit operator string(HttpMethod value)
        {
            return object.ReferenceEquals(null, value) ? null as string : value.ToString();
        }

        public static implicit operator HttpMethod(string value)
        {
            return object.ReferenceEquals(null, value) ? null as HttpMethod : new HttpMethod(value);
        }

        public static bool operator ==(HttpMethod operand1, HttpMethod operand2)
        {
            return 0 == HttpMethod.Compare(operand1, operand2);
        }

        public static bool operator !=(HttpMethod operand1, HttpMethod operand2)
        {
            return 0 != HttpMethod.Compare(operand1, operand2);
        }

        public static bool operator <(HttpMethod operand1, HttpMethod operand2)
        {
            return HttpMethod.Compare(operand1, operand2) < 0;
        }

        public static bool operator >(HttpMethod operand1, HttpMethod operand2)
        {
            return HttpMethod.Compare(operand1, operand2) > 0;
        }

        public static int Compare(HttpMethod comparand1, HttpMethod comparand2)
        {
            return object.ReferenceEquals(comparand1, comparand2)
                ? 0
                : string.Compare(
                    object.ReferenceEquals(null, comparand1) ? null as string : comparand1.ToString(),
                    object.ReferenceEquals(null, comparand2) ? null as string : comparand2.ToString(),
                    StringComparison.OrdinalIgnoreCase);
        }

        public int CompareTo(object obj)
        {
            int comparison = 1;

            if (!object.ReferenceEquals(null, obj))
            {
                HttpMethod value = obj as HttpMethod;

                if (object.ReferenceEquals(null, value))
                {
                    throw new ArgumentOutOfRangeException("obj");
                }

                comparison = HttpMethod.Compare(this, value);
            }

            return comparison;
        }

        public override bool Equals(object obj)
        {
            bool result = false;

            if (!object.ReferenceEquals(null, obj))
            {
                if (object.ReferenceEquals(this, obj))
                {
                    result = true;
                }
                else
                {
                    HttpMethod cast = obj as HttpMethod;

                    if (!object.ReferenceEquals(null, cast))
                    {
                        result = 0 == HttpMethod.Compare(this, cast);
                    }
                }
            }

            return result;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}