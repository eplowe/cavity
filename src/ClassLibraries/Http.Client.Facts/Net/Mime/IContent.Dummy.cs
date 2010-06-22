namespace Cavity.Net.Mime
{
    using System;
    using System.IO;
    using System.Net.Mime;

    public class IContentDummy : IContent
    {
        ContentType IContentType.ContentType
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