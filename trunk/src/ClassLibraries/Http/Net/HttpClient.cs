namespace Cavity.Net
{
    public sealed class HttpClient
    {
        public HttpClient()
        {
            this.Settings = new HttpClientSettings();
        }

        public HttpClientSettings Settings
        {
            get;
            set;
        }
    }
}