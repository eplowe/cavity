namespace Cavity.Net
{
    public interface IHttpHeader
    {
        Token Name { get; }

        string Value { get; }
    }
}