namespace Cavity.Net
{
    public interface IHttpRequest
    {
        IHttpResponse ToResponse(IHttpClient client);
    }
}