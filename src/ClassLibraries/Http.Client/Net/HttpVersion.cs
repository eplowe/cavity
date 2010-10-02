namespace Cavity.Net
{
    using System;
    using System.Globalization;

    public sealed class HttpVersion : ComparableObject
    {
        private int _major;

        private int _minor;

        public HttpVersion(int major, int minor)
            : this()
        {
            Major = major;
            Minor = minor;
        }

        private HttpVersion()
        {
        }

        public int Major
        {
            get
            {
                return _major;
            }

            private set
            {
                if (value.IsBoundedBy(0, 9))
                {
                    _major = value;
                    return;
                }

                throw new ArgumentOutOfRangeException("value");
            }
        }

        public int Minor
        {
            get
            {
                return _minor;
            }

            private set
            {
                if (value.IsBoundedBy(0, 9))
                {
                    _minor = value;
                    return;
                }

                throw new ArgumentOutOfRangeException("value");
            }
        }

        public static implicit operator HttpVersion(string value)
        {
            return ReferenceEquals(null, value)
                       ? null
                       : FromString(value);
        }

        public static HttpVersion FromString(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                throw new FormatException("value");
            }

            if (!value.StartsWith("HTTP/", StringComparison.Ordinal))
            {
                throw new FormatException("value");
            }

            var parts = value.Substring("HTTP/".Length).Split('.');

            return new HttpVersion(
                int.Parse(parts[0], CultureInfo.InvariantCulture),
                int.Parse(parts[1], CultureInfo.InvariantCulture));
        }

        public override string ToString()
        {
            return "HTTP/{0}.{1}".FormatWith(Major, Minor);
        }
    }
}