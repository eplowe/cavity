namespace Cavity.Net
{
    using System;
    using System.Globalization;

    public struct HttpDate : IComparable
    {
        public HttpDate(DateTime value)
            : this()
        {
            this.Value = value;
        }

        public DateTime Value
        {
            get;
            private set;
        }

        public static implicit operator DateTime(HttpDate value)
        {
            return value.Value;
        }

        public static implicit operator HttpDate(DateTime value)
        {
            return new HttpDate(value);
        }

        public static implicit operator string(HttpDate value)
        {
            return value.ToString();
        }

        public static implicit operator HttpDate(string value)
        {
            return HttpDate.Parse(value);
        }

        public static bool operator ==(HttpDate operand1, HttpDate operand2)
        {
            return 0 == HttpDate.Compare(operand1, operand2);
        }

        public static bool operator !=(HttpDate operand1, HttpDate operand2)
        {
            return 0 != HttpDate.Compare(operand1, operand2);
        }

        public static bool operator <(HttpDate operand1, HttpDate operand2)
        {
            return HttpDate.Compare(operand1, operand2) < 0;
        }

        public static bool operator >(HttpDate operand1, HttpDate operand2)
        {
            return HttpDate.Compare(operand1, operand2) > 0;
        }

        public static int Compare(HttpDate comparand1, HttpDate comparand2)
        {
            return object.ReferenceEquals(comparand1, comparand2)
                ? 0
                : DateTime.Compare(comparand1.Value, comparand2.Value);
        }

        public static HttpDate Parse(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }
            else if (0 == value.Length)
            {
                throw new FormatException("value");
            }

            return new HttpDate(DateTime.Parse(value, CultureInfo.InvariantCulture));
        }

        public int CompareTo(object obj)
        {
            int comparison = 1;

            if (!object.ReferenceEquals(null, obj))
            {
                if (obj is HttpDate)
                {
                    comparison = HttpDate.Compare(this, (HttpDate)obj);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("obj");
                }
            }

            return comparison;
        }

        public override bool Equals(object obj)
        {
            bool result = false;

            if (obj is HttpDate)
            {
                result = 0 == HttpDate.Compare(this, (HttpDate)obj);
            }

            return result;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override string ToString()
        {
            return this.Value.ToString("R", CultureInfo.InvariantCulture);
        }
    }
}