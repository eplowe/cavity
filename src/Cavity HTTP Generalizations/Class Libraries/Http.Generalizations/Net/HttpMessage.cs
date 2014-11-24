namespace Cavity.Net
{
    using System;

    public abstract class HttpMessage
    {
#if NET20
        private static readonly string _break = StringExtensionMethods.FormatWith("{0}{0}", Environment.NewLine);
#else
        private static readonly string _break = "{0}{0}".FormatWith(Environment.NewLine);
#endif

        public IHttpMessageBody Body { get; set; }

        public IHttpMessageHeaders Headers { get; set; }

        protected string Load(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            string body = null;
            var index = value.IndexOf(_break, StringComparison.Ordinal);
            if (-1 == index)
            {
                index = value.Length;
            }
            else
            {
                body = value.Substring(index + _break.Length);
            }

            var headers = value.Substring(0, index);
            index = headers.IndexOf(Environment.NewLine, StringComparison.Ordinal);

            string line;
            if (-1 == index)
            {
                line = headers;
                headers = string.Empty;
            }
            else
            {
                line = headers.Substring(0, index);
                headers = headers.Substring(index + Environment.NewLine.Length);
            }

            Headers = HttpHeaderDictionary.FromString(headers);
            Body = null == body
                       ? null
                       : new TextBody(body);

            return line;
        }

        protected string GetHeader(Token name)
        {
            return Headers.Contains(name)
                       ? Headers[name]
                       : null;
        }

        protected void SetHeader(Token name,
                                 string value)
        {
            if (Headers.Contains(name))
            {
                Headers[name] = value;
            }
            else
            {
                Headers.Add(new HttpHeader(name, value));
            }
        }
    }
}