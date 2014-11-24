namespace Cavity
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class HttpTestException : Exception
    {
        public HttpTestException()
        {
        }

        public HttpTestException(string message)
            : base(message)
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