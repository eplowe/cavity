namespace Cavity.Net
{
    using System;

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

        IHttpBody IHttpMessage.Body
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        IHttpHeaderCollection IHttpMessage.Headers
        {
            get
            {
                throw new NotSupportedException();
            }
        }
    }
}