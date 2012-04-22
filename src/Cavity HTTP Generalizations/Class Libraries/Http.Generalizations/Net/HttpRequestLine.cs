namespace Cavity.Net
{
    using System;

    public class HttpRequestLine : ComparableObject
    {
        public HttpRequestLine(Token method, 
                               AbsoluteUri requestUri)
            : this(method, requestUri, HttpVersion.Latest)
        {
        }

        public HttpRequestLine(Token method, 
                               AbsoluteUri requestUri, 
                               HttpVersion version)
            : this()
        {
            if (null == method)
            {
                throw new ArgumentNullException("method");
            }

            if (null == requestUri)
            {
                throw new ArgumentNullException("requestUri");
            }

            if (null == version)
            {
                throw new ArgumentNullException("version");
            }

            Method = method;
            RequestUri = requestUri;
            Version = version;
        }

        private HttpRequestLine()
        {
        }

        public Token Method { get; private set; }

        public AbsoluteUri RequestUri { get; private set; }

        public HttpVersion Version { get; private set; }

        public static implicit operator string(HttpRequestLine obj)
        {
            return ReferenceEquals(null, obj)
                       ? null
                       : obj.ToString();
        }

        public static HttpRequestLine FromString(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

#if NET20
            var parts = StringExtensionMethods.Split(value, ' ', StringSplitOptions.RemoveEmptyEntries);
#else
            var parts = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
#endif
            if (2 == parts.Length)
            {
                return new HttpRequestLine(parts[0], parts[1], HttpVersion.Latest);
            }

            if (3 == parts.Length)
            {
                return new HttpRequestLine(parts[0], parts[1], parts[2]);
            }

            throw new FormatException();
        }

        public override string ToString()
        {
#if NET20
            return StringExtensionMethods.FormatWith("{0} {1} {2}", Method, RequestUri, Version);
#else
            return "{0} {1} {2}".FormatWith(Method, RequestUri, Version);
#endif
        }
    }
}