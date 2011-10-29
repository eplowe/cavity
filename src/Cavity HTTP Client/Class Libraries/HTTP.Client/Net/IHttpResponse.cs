namespace Cavity.Net
{
    public interface IHttpResponse : IHttpMessage
    {
        StatusLine StatusLine { get; }
    }
}