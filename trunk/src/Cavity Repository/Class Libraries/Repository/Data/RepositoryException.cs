namespace Cavity.Data
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class RepositoryException : Exception
    {
        public RepositoryException()
        {
        }

        public RepositoryException(string message)
            : base(message)
        {
        }

        public RepositoryException(Exception innerException)
            : this("A exception was encountered in the repository.", innerException)
        {
        }

        public RepositoryException(string message, 
                                   Exception innerException)
            : base(message, innerException)
        {
        }

        private RepositoryException(SerializationInfo info, 
                                    StreamingContext context)
            : base(info, context)
        {
        }
    }
}