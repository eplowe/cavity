namespace Cavity.Net.Mime
{
    using System.Net.Mime;

    public interface IContentType
    {
        ContentType ContentType { get; }
    }
}