namespace Cavity.Net
{
    using System;
    using System.IO;

    public class IMediaTypeDummy : IMediaType
    {
        public IHttpBody ToBody(StreamReader reader)
        {
            throw new NotSupportedException();
        }
    }
}