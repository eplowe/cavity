namespace Cavity.Net
{
    using System.IO;

    public interface IHttpHeader
    {
        Token Name { get; }

        string Value { get; }
    }
}