namespace Cavity
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class TestException : Exception
    {
        public TestException()
        {
        }

        public TestException(string message)
            : base(message)
        {
        }

        public TestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        private TestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}