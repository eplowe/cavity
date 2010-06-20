namespace Cavity.Net
{
    using System;
    using System.Globalization;

    public sealed class HttpVersion : IComparable
    {
        private int _major;
        private int _minor;

        public HttpVersion(int major, int minor)
            : this()
        {
            this.Major = major;
            this.Minor = minor;
        }
    
        private HttpVersion()
        {
        }
        
        public int Major
        {
            get
            {
                return this._major;
            }

            private set
            {
                if (value < 0 || value > 9)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                this._major = value;
            }
        }

        public int Minor
        {
            get
            {
                return this._minor;
            }

            private set
            {
                if (value < 0 || value > 9)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                this._minor = value;
            }
        }

        public static implicit operator string(HttpVersion value)
        {
            return object.ReferenceEquals(null, value) ? null as string : value.ToString();
        }

        public static implicit operator HttpVersion(string value)
        {
            return object.ReferenceEquals(null, value) ? null as HttpVersion : HttpVersion.Parse(value);
        }

        public static bool operator ==(HttpVersion operand1, HttpVersion operand2)
        {
            return 0 == HttpVersion.Compare(operand1, operand2);
        }

        public static bool operator !=(HttpVersion operand1, HttpVersion operand2)
        {
            return 0 != HttpVersion.Compare(operand1, operand2);
        }

        public static bool operator <(HttpVersion operand1, HttpVersion operand2)
        {
            return HttpVersion.Compare(operand1, operand2) < 0;
        }

        public static bool operator >(HttpVersion operand1, HttpVersion operand2)
        {
            return HttpVersion.Compare(operand1, operand2) > 0;
        }

        public static int Compare(HttpVersion comparand1, HttpVersion comparand2)
        {
            return object.ReferenceEquals(comparand1, comparand2)
                ? 0
                : string.Compare(
                    object.ReferenceEquals(null, comparand1) ? null as string : comparand1.ToString(),
                    object.ReferenceEquals(null, comparand2) ? null as string : comparand2.ToString(),
                    StringComparison.OrdinalIgnoreCase);
        }

        public static HttpVersion Parse(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }
            else if (0 == value.Length)
            {
                throw new FormatException("value");
            }
            else if (!value.StartsWith("HTTP/", StringComparison.Ordinal))
            {
                throw new FormatException("value");
            }

            string[] parts = value.Substring("HTTP/".Length).Split(new char[] { '.' });

            return new HttpVersion(
                int.Parse(parts[0], CultureInfo.InvariantCulture),
                int.Parse(parts[1], CultureInfo.InvariantCulture));
        }
        
        public int CompareTo(object obj)
        {
            int comparison = 1;

            if (!object.ReferenceEquals(null, obj))
            {
                HttpVersion value = obj as HttpVersion;

                if (object.ReferenceEquals(null, value))
                {
                    throw new ArgumentOutOfRangeException("obj");
                }

                comparison = HttpVersion.Compare(this, value);
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
                    HttpVersion cast = obj as HttpVersion;

                    if (!object.ReferenceEquals(null, cast))
                    {
                        result = 0 == HttpVersion.Compare(this, cast);
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
            return string.Format(CultureInfo.InvariantCulture, "HTTP/{0}.{1}", this.Major, this.Minor);
        }
    }
}