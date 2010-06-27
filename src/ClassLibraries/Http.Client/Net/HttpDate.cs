namespace Cavity.Net
{
    using System;
    using System.Globalization;

    public struct HttpDate : IComparable
    {
        private DateTime _value;

        public HttpDate(DateTime value)
            : this()
        {
            this._value = value;
        }

        public static implicit operator DateTime(HttpDate value)
        {
            return value.ToDateTime();
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
            return HttpDate.FromString(value);
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
                : DateTime.Compare(comparand1.ToDateTime(), comparand2.ToDateTime());
        }

        public static HttpDate FromString(string value)
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
            int result = 1;

            if (!object.ReferenceEquals(null, obj))
            {
                if (obj is HttpDate)
                {
                    result = HttpDate.Compare(this, (HttpDate)obj);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("obj");
                }
            }

            return result;
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

        public DateTime ToDateTime()
        {
            return this._value;
        }

        public override string ToString()
        {
            return this.ToDateTime().ToString("R", CultureInfo.InvariantCulture);
        }
    }
}