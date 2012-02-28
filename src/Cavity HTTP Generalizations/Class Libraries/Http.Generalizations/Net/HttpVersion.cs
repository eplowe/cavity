namespace Cavity.Net
{
    using System;
    using System.Xml;

    public class HttpVersion : ComparableObject
    {
        private static readonly HttpVersion _latest = new HttpVersion(1, 1);

        public HttpVersion()
            : this(1, 1)
        {
        }

        public HttpVersion(int major, 
                           int minor)
        {
            if (0 > major)
            {
                throw new ArgumentOutOfRangeException("major");
            }

            if (0 > minor)
            {
                throw new ArgumentOutOfRangeException("minor");
            }

            Major = major;
            Minor = minor;
        }

        public static HttpVersion Latest
        {
            get
            {
                return _latest;
            }
        }

        public int Major { get; set; }

        public int Minor { get; set; }

        public static implicit operator HttpVersion(string value)
        {
            return ReferenceEquals(null, value)
                       ? null
                       : FromString(value);
        }

        public static implicit operator string(HttpVersion obj)
        {
            return ReferenceEquals(null, obj)
                       ? null
                       : obj.ToString();
        }

        public static HttpVersion FromString(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            if (!value.StartsWith("HTTP/", StringComparison.Ordinal))
            {
                throw new FormatException();
            }

            if ("HTTP/1.1".Equals(value, StringComparison.Ordinal))
            {
                return Latest;
            }

            var parts = value.Substring("HTTP/".Length).Split('.', StringSplitOptions.RemoveEmptyEntries);
            if (2 != parts.Length)
            {
                throw new FormatException();
            }

            return new HttpVersion(XmlConvert.ToInt32(parts[0]), XmlConvert.ToInt32(parts[1]));
        }

        public override string ToString()
        {
            return "HTTP/{0}.{1}".FormatWith(Major, Minor);
        }
    }
}