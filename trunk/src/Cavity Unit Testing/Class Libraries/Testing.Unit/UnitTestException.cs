namespace Cavity
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class UnitTestException : Exception
    {
        public UnitTestException()
        {
        }

        public UnitTestException(string message)
            : base(message)
        {
        }

        public UnitTestException(string message,
                                 Exception innerException)
            : base(message, innerException)
        {
        }

        private UnitTestException(SerializationInfo info,
                                  StreamingContext context)
            : base(info, context)
        {
        }
    }
}