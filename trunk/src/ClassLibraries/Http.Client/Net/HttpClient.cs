namespace Cavity.Net
{
    using Microsoft.Practices.ServiceLocation;

    public sealed class HttpClient : IHttpClient
    {
        public string UserAgent
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IUserAgent>().Value;
            }
        }
    }
}