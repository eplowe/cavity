namespace Cavity.Net
{
    using System;

    public class IHttpRequestDummy : IHttpRequest
    {
        public IHttpResponse ToResponse(IHttpClient client)
        {
            throw new NotSupportedException();
        }
    }
}