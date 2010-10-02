namespace Cavity.Net
{
    using System;
    using System.IO;
    using System.Net.Mime;

    public sealed class IHttpContentDummy : IHttpContent
    {
        ContentType IHttpContent.Type
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        void IHttpContent.Write(Stream stream)
        {
            throw new NotSupportedException();
        }
    }
}