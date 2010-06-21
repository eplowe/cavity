namespace Cavity.Net
{
    using System.IO;

    public interface IMediaType
    {
        IHttpBody ToBody(StreamReader reader);
    }
}