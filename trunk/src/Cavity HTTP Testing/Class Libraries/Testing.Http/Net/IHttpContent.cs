namespace Cavity.Net
{
    using System.IO;
    using System.Net.Mime;

    public interface IHttpContent
    {
        ContentType Type { get; }

        void Write(Stream stream);
    }
}