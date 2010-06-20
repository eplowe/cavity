namespace Cavity.Net
{
    using System;
    using System.Globalization;

    public sealed class StatusLine : IComparable
    {
        private int _code;
        private string _reason;
        private HttpVersion _version;

        public StatusLine(HttpVersion version, int code, string reason)
        {
            this.Version = version;
            this.Code = code;
            this.Reason = reason;
        }

        private StatusLine()
        {
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

        public static implicit operator string(StatusLine value)
        {
            return object.ReferenceEquals(null, value) ? null as string : value.ToString();
        }

        public static implicit operator StatusLine(string value)
        {
            return object.ReferenceEquals(null, value) ? null as StatusLine : StatusLine.Parse(value);
        }

        public static bool operator ==(StatusLine operand1, StatusLine operand2)
        {
            return 0 == StatusLine.Compare(operand1, operand2);
        }

        public static bool operator !=(StatusLine operand1, StatusLine operand2)
        {
            return 0 != StatusLine.Compare(operand1, operand2);
        }

        public static bool operator <(StatusLine operand1, StatusLine operand2)
        {
            return StatusLine.Compare(operand1, operand2) < 0;
        }

        public static bool operator >(StatusLine operand1, StatusLine operand2)
        {
            return StatusLine.Compare(operand1, operand2) > 0;
        }

        public static int Compare(StatusLine comparand1, StatusLine comparand2)
        {
            return object.ReferenceEquals(comparand1, comparand2)
                ? 0
                : string.Compare(
                    object.ReferenceEquals(null, comparand1) ? null as string : comparand1.ToString(),
                    object.ReferenceEquals(null, comparand2) ? null as string : comparand2.ToString(),
                    StringComparison.OrdinalIgnoreCase);
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

        public int CompareTo(object obj)
        {
            int comparison = 1;

            if (!object.ReferenceEquals(null, obj))
            {
                StatusLine value = obj as StatusLine;

                if (object.ReferenceEquals(null, value))
                {
                    throw new ArgumentOutOfRangeException("obj");
                }

                comparison = StatusLine.Compare(this, value);
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
                    StatusLine cast = obj as StatusLine;

                    if (!object.ReferenceEquals(null, cast))
                    {
                        result = 0 == StatusLine.Compare(this, cast);
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
            return string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", this.Version, this.Code, this.Reason);
        }
    }
}