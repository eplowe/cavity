namespace Cavity.Net
{
    using System;

    public class IHttpClientDummy : IHttpClient
    {
        public string UserAgent
        {
            get
            {
                throw new NotSupportedException();
            }
        }
    }
}