namespace Cavity.Net
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    public sealed class RequestLine : IComparable
    {
        private string _method;
        private string _requestUri;
        private HttpVersion _version;

        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#", Justification = "The requestUri is intentionally a string.")]
        public RequestLine(string method, string requestUri, HttpVersion version)
        {
            this.Method = method;
            this.RequestUri = requestUri;
            this.Version = version;
        }

        private RequestLine()
        {
        }

        public string Method
        {
            get
            {
                return this._method;
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

                this._method = value;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "The RequestUri is intentionally a string.")]
        public string RequestUri
        {
            get
            {
                return this._requestUri;
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

                this._requestUri = value;
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

        public static implicit operator string(RequestLine value)
        {
            return object.ReferenceEquals(null, value) ? null as string : value.ToString();
        }

        public static implicit operator RequestLine(string value)
        {
            return object.ReferenceEquals(null, value) ? null as RequestLine : RequestLine.Parse(value);
        }

        public static bool operator ==(RequestLine operand1, RequestLine operand2)
        {
            return 0 == RequestLine.Compare(operand1, operand2);
        }

        public static bool operator !=(RequestLine operand1, RequestLine operand2)
        {
            return 0 != RequestLine.Compare(operand1, operand2);
        }

        public static bool operator <(RequestLine operand1, RequestLine operand2)
        {
            return RequestLine.Compare(operand1, operand2) < 0;
        }

        public static bool operator >(RequestLine operand1, RequestLine operand2)
        {
            return RequestLine.Compare(operand1, operand2) > 0;
        }

        public static int Compare(RequestLine comparand1, RequestLine comparand2)
        {
            return object.ReferenceEquals(comparand1, comparand2)
                ? 0
                : string.Compare(
                    object.ReferenceEquals(null, comparand1) ? null as string : comparand1.ToString(),
                    object.ReferenceEquals(null, comparand2) ? null as string : comparand2.ToString(),
                    StringComparison.OrdinalIgnoreCase);
        }

        public static RequestLine Parse(string value)
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

            return new RequestLine(
                parts[0],
                parts[1],
                parts[2]);
        }

        public int CompareTo(object obj)
        {
            int comparison = 1;

            if (!object.ReferenceEquals(null, obj))
            {
                RequestLine value = obj as RequestLine;

                if (object.ReferenceEquals(null, value))
                {
                    throw new ArgumentOutOfRangeException("obj");
                }

                comparison = RequestLine.Compare(this, value);
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
                    RequestLine cast = obj as RequestLine;

                    if (!object.ReferenceEquals(null, cast))
                    {
                        result = 0 == RequestLine.Compare(this, cast);
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
            return string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", this.Method, this.RequestUri, this.Version);
        }
    }
}