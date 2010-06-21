namespace Cavity.Net
{
    public interface IHttpMessage
    {
        IHttpBody Body { get; }

        IHttpHeaderCollection Headers { get; }
    }
}