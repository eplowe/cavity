namespace Cavity.Net
{
    using System;
    using System.Globalization;

    public sealed class HttpVersion : ValueObject<HttpVersion>
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
            this.RegisterProperty(x => x.Major);
            this.RegisterProperty(x => x.Minor);
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

        public static implicit operator HttpVersion(string value)
        {
            return object.ReferenceEquals(null, value) ? null as HttpVersion : HttpVersion.Parse(value);
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
        
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "HTTP/{0}.{1}", this.Major, this.Minor);
        }
    }
}