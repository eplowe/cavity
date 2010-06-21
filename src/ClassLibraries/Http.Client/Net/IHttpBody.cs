namespace Cavity.Net
{
    using System.IO;

    public interface IHttpBody
    {
        IHttpBody Read(StreamReader reader);
    }
}