namespace Cavity.Net
{
    using Microsoft.Practices.ServiceLocation;

    public sealed class HttpClient : ComparableObject, IHttpClient
    {
        public string UserAgent
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IUserAgent>().Value;
            }
        }

        public override string ToString()
        {
            return this.UserAgent;
        }
    }
}