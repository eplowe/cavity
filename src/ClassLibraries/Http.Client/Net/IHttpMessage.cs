namespace Cavity.Net
{
    using System.IO;
    using Cavity.Net.Mime;

    public interface IHttpMessage
    {
        IContent Body { get; }

        HttpHeaderCollection Headers { get; }

        void Read(TextReader reader);

        void Write(TextWriter writer);
    }
}