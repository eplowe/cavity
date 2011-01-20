namespace Cavity.Net
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class HttpTestException : TestException
    {
        public HttpTestException()
        {
        }

        public HttpTestException(string message)
            : base(message)
        {
        }

        public HttpTestException(Response response,
                                 string message)
            : this(string.Format(CultureInfo.InvariantCulture, "{0}\r\n\r\n{1}", message, response))
        {
        }

        public HttpTestException(string message,
                                 Exception innerException)
            : base(message, innerException)
        {
        }

        private HttpTestException(SerializationInfo info,
                                  StreamingContext context)
            : base(info, context)
        {
        }
    }
}