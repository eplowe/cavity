namespace Cavity.Net
{
    using System;
    using Cavity.Net.Mime;

    public class IHttpResponseDummy : IHttpResponse
    {
        public StatusLine StatusLine
        {
            get
            {
                throw new NotSupportedException();
            }
            
            set
            {
                throw new NotSupportedException();
            }
        }

        IContent IHttpMessage.Body
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        HttpHeaderCollection IHttpMessage.Headers
        {
            get
            {
                throw new NotSupportedException();
            }
        }
    }
}