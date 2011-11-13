namespace Cavity.Net
{
    using System;

    public abstract class HttpMessage
    {
        private static readonly string _break = "{0}{0}".FormatWith(Environment.NewLine);

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
    }
}