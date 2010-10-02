namespace Cavity
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class DerivedTestException : TestException
    {
        public DerivedTestException()
        {
        }

        public DerivedTestException(string message)
            : base(message)
        {
        }

        public DerivedTestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        private DerivedTestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}