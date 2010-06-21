namespace Cavity.Net
{
    using System;
    using System.Globalization;

    public sealed class StatusLine : ValueObject<StatusLine>
    {
        private int _code;
        private string _reason;
        private HttpVersion _version;

        public StatusLine(HttpVersion version, int code, string reason)
            : this()
        {
            this.Version = version;
            this.Code = code;
            this.Reason = reason;
        }

        private StatusLine()
        {
            this.RegisterProperty(x => x.Version);
            this.RegisterProperty(x => x.Code);
            this.RegisterProperty(x => x.Reason);
        }

        public int Code
        {
            get
            {
                return this._code;
            }

            private set
            {
                if (value < 100 || value > 999)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                this._code = value;
            }
        }

        public string Reason
        {
            get
            {
                return this._reason;
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
                else if (value.Contains("\n") || value.Contains("\r"))
                {
                    throw new FormatException("value");
                }

                this._reason = value;
            }
        }

        public HttpVersion Version
        {
            get
            {
                return this._version;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this._version = value;
            }
        }

        public static implicit operator StatusLine(string value)
        {
            return object.ReferenceEquals(null, value) ? null as StatusLine : StatusLine.Parse(value);
        }

        public static StatusLine Parse(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }
            else if (0 == value.Length)
            {
                throw new FormatException("value");
            }

            string[] parts = value.Split(new char[] { ' ' });

            if (3 > parts.Length)
            {
                throw new FormatException("value");
            }

            string reason = null;
            for (int i = 2; i < parts.Length; i++)
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
            return string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", this.Version, this.Code, this.Reason);
        }
    }
}