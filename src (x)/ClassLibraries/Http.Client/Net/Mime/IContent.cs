namespace Cavity.Net.Mime
{
    using System.IO;

    public interface IContent : IContentType
    {
        object Content { get; set; }

        void Write(TextWriter writer);
    }
}