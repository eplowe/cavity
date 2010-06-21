﻿namespace Cavity.Net
{
    using System;
    using System.IO;

    public class IHttpRequestDummy : IHttpRequest
    {
        public Uri AbsoluteUri
        {
            get
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

        public void Write(StreamWriter writer)
        {
            throw new NotSupportedException();
        }
    }
}