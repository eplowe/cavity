namespace Cavity
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public abstract class TestException : Exception
    {
        protected TestException()
        {
        }

        protected TestException(string message)
            : base(message)
        {
        }

        protected TestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected TestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}