namespace Cavity.Net
{
    public interface IHttp
    {
        IHttpResponse Send(IHttpRequest request);
    }
}