namespace Cavity.Net
{
    using System;
    using System.Globalization;

    public sealed class StatusLine : ComparableObject
    {
        private int _code;
        private string _reason;
        private HttpVersion _version;

        public StatusLine(HttpVersion version, int code, string reason)
            : this()
        {
            Version = version;
            Code = code;
            Reason = reason;
        }

        private StatusLine()
        {
        }

        public int Code
        {
            get
            {
                return _code;
            }

            private set
            {
                if (value < 100 ||
                    value > 999)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                _code = value;
            }
        }

        public string Reason
        {
            get
            {
                return _reason;
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
                else if (value.Contains("\n") ||
                         value.Contains("\r"))
                {
                    throw new FormatException("value");
                }

                _reason = value;
            }
        }

        public HttpVersion Version
        {
            get
            {
                return _version;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                _version = value;
            }
        }

        public static implicit operator StatusLine(string value)
        {
            return ReferenceEquals(null, value)
                       ? null
                       : FromString(value);
        }

        public static StatusLine FromString(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }
            else if (0 == value.Length)
            {
                throw new FormatException("value");
            }

            var parts = value.Split(new[]
            {
                ' '
            });

            if (3 > parts.Length)
            {
                throw new FormatException("value");
            }

            string reason = null;
            for (var i = 2; i < parts.Length; i++)
            {
                if (null == reason)
                {
                    reason += parts[i];
                }
                else
                {
                    reason += " " + parts[i];
                }
            }

            return new StatusLine(
                parts[0],
                int.Parse(parts[1], CultureInfo.InvariantCulture),
                reason);
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", Version, Code, Reason);
        }
    }
}