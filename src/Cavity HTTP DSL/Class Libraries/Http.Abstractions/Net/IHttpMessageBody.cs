namespace Cavity.Net
{
    using System.IO;

    public interface IHttpMessageBody
    {
        void Write(Stream stream);
    }
}