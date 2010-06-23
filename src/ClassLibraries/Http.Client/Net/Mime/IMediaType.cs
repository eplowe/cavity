namespace Cavity.Net.Mime
{
    using System.IO;

    public interface IMediaType
    {
        IContent ToContent(TextReader reader);
    }
}