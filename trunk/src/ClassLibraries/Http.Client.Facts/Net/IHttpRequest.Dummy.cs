namespace Cavity.Net
{
    using System;
    using System.IO;
    using Cavity.Net.Mime;

    public class IHttpRequestDummy : IHttpRequest
    {
        public Uri AbsoluteUri
        {
            get
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

        public void Write(StreamWriter writer)
        {
            throw new NotSupportedException();
        }
    }
}