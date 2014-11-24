namespace Cavity.Globalization
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class TranslationException : Exception
    {
        public TranslationException()
        {
        }

        public TranslationException(string message)
            : base(message)
        {
        }

        public TranslationException(string message,
                                    Exception innerException)
            : base(message, innerException)
        {
        }

        private TranslationException(SerializationInfo info,
                                     StreamingContext context)
            : base(info, context)
        {
        }
    }
}