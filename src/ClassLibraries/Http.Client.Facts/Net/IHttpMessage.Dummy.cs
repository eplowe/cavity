namespace Cavity.Net
{
    using System;
    using System.IO;
    using Cavity.Net.Mime;

    public class IHttpMessageDummy : IHttpMessage
    {
        public IContent Body
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

        public HttpHeaderCollection Headers
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

        public void Read(TextReader reader)
        {
            throw new NotSupportedException();
        }

        public void Write(TextWriter writer)
        {
            throw new NotSupportedException();
        }
    }
}