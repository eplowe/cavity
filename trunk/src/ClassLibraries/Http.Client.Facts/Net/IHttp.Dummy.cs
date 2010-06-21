namespace Cavity.Net
{
    using System;

    public class IHttpDummy : IHttp
    {
        public IHttpResponse Send(IHttpRequest request)
        {
            throw new NotSupportedException();
        }
    }
}