namespace Cavity.Net
{
    using System;

    public class IHttpRequestDummy : IHttpMessageDummy, IHttpRequest
    {
        public Uri AbsoluteUri
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public RequestLine RequestLine
        {
            get
            {
                throw new NotSupportedException();
            }
        }
    }
}