namespace Cavity.Net
{
    using System;
    using System.IO;

    public interface IHttpRequest : IHttpMessage
    {
        Uri AbsoluteUri { get; }

        void Write(StreamWriter writer);
    }
}