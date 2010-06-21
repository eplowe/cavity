namespace Cavity.Net
{
    using Microsoft.Practices.ServiceLocation;

    public sealed class HttpClient : ValueObject<HttpClient>, IHttpClient
    {
        public HttpClient()
        {
            this.RegisterProperty(x => x.UserAgent);
        }

        public string UserAgent
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IUserAgent>().Value;
            }
        }
    }
}