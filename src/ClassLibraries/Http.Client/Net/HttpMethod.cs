namespace Cavity.Net
{
    using System;
    using System.Linq;

    public sealed class HttpMethod : ComparableObject
    {
        private static readonly HttpMethod _connect = new HttpMethod("CONNECT");

        private static readonly HttpMethod _delete = new HttpMethod("DELETE");

        private static readonly HttpMethod _get = new HttpMethod("GET");

        private static readonly HttpMethod _head = new HttpMethod("HEAD");

        private static readonly HttpMethod _options = new HttpMethod("OPTIONS");

        private static readonly HttpMethod _post = new HttpMethod("POST");

        private static readonly HttpMethod _put = new HttpMethod("PUT");

        private static readonly HttpMethod _trace = new HttpMethod("TRACE");

        private string _value;

        public HttpMethod(string value)
            : this()
        {
            Value = value;
        }

        private HttpMethod()
        {
        }

        public static HttpMethod Connect
        {
            get
            {
                return _connect;
            }
        }

        public static HttpMethod Delete
        {
            get
            {
                return _delete;
            }
        }

        public static HttpMethod Get
        {
            get
            {
                return _get;
            }
        }

        public static HttpMethod Head
        {
            get
            {
                return _head;
            }
        }

        public static HttpMethod Options
        {
            get
            {
                return _options;
            }
        }

        public static HttpMethod Post
        {
            get
            {
                return _post;
            }
        }

        public static HttpMethod Put
        {
            get
            {
                return _put;
            }
        }

        public static HttpMethod Trace
        {
            get
            {
                return _trace;
            }
        }

        public string Value
        {
            get
            {
                return _value;
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
                else if (0 != value
                                  .ToUpperInvariant()
                                  .ToArray()
                                  .Where(index => 'A' > index || 'Z' < index)
                                  .Count())
                {
                    throw new FormatException("value");
                }

                _value = value;
            }
        }

        public static implicit operator HttpMethod(string value)
        {
            return ReferenceEquals(null, value)
                       ? null
                       : new HttpMethod(value);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}