namespace Cavity.Net.Mime
{
    using System.IO;

    public interface IContent : IContentType
    {
        void Write(StreamWriter writer);
    }
}