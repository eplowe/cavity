namespace Cavity.Net
{
    using System;

    public sealed class HttpRequest : IComparable, IHttpRequest
    {
        private RequestLine _requestLine;

        public HttpRequest(RequestLine requestLine)
        {
            this.RequestLine = requestLine;
        }

        private HttpRequest()
        {
        }

        public RequestLine RequestLine
        {
            get
            {
                return this._requestLine;
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this._requestLine = value;
            }
        }

        public static implicit operator string(HttpRequest value)
        {
            return object.ReferenceEquals(null, value) ? null as string : value.ToString();
        }

        public static implicit operator HttpRequest(string value)
        {
            return object.ReferenceEquals(null, value) ? null as HttpRequest : HttpRequest.Parse(value);
        }

        public static bool operator ==(HttpRequest operand1, HttpRequest operand2)
        {
            return 0 == HttpRequest.Compare(operand1, operand2);
        }

        public static bool operator !=(HttpRequest operand1, HttpRequest operand2)
        {
            return 0 != HttpRequest.Compare(operand1, operand2);
        }

        public static bool operator <(HttpRequest operand1, HttpRequest operand2)
        {
            return HttpRequest.Compare(operand1, operand2) < 0;
        }

        public static bool operator >(HttpRequest operand1, HttpRequest operand2)
        {
            return HttpRequest.Compare(operand1, operand2) > 0;
        }

        public static int Compare(HttpRequest comparand1, HttpRequest comparand2)
        {
            return object.ReferenceEquals(comparand1, comparand2)
                ? 0
                : string.Compare(
                    object.ReferenceEquals(null, comparand1) ? null as string : comparand1.ToString(),
                    object.ReferenceEquals(null, comparand2) ? null as string : comparand2.ToString(),
                    StringComparison.OrdinalIgnoreCase);
        }

        public static HttpRequest Parse(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            return new HttpRequest(RequestLine.Parse(value));
        }

        public int CompareTo(object obj)
        {
            int comparison = 1;

            if (!object.ReferenceEquals(null, obj))
            {
                HttpRequest value = obj as HttpRequest;

                if (object.ReferenceEquals(null, value))
                {
                    throw new ArgumentOutOfRangeException("obj");
                }

                comparison = HttpRequest.Compare(this, value);
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
                    HttpRequest cast = obj as HttpRequest;

                    if (!object.ReferenceEquals(null, cast))
                    {
                        result = 0 == HttpRequest.Compare(this, cast);
                    }
                }
            }

            return result;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public IHttpResponse ToResponse(IHttpClient client)
        {
            if (null == client)
            {
                throw new ArgumentNullException("client");
            }

            throw new NotSupportedException();
        }

        public override string ToString()
        {
            return this.RequestLine;
        }
    }
}