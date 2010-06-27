namespace Cavity.Net
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    public sealed class RequestLine : ComparableObject
    {
        private string _method;
        private string _requestUri;
        private HttpVersion _version;

        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#", Justification = "The requestUri is intentionally a string.")]
        public RequestLine(string method, string requestUri, HttpVersion version)
            : this()
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

        public static implicit operator RequestLine(string value)
        {
            return object.ReferenceEquals(null, value) ? null as RequestLine : RequestLine.FromString(value);
        }

        public static RequestLine FromString(string value)
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

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", this.Method, this.RequestUri, this.Version);
        }
    }
}