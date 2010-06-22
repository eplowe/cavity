namespace Cavity.Net.Mime
{
    using System;
    using System.Net.Mime;

    public class IContentTypeDummy : IContentType
    {
        public ContentType ContentType
        {
            get
            {
                throw new NotSupportedException();
            }
        }
    }
}