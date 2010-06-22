namespace Cavity.Net.Mime
{
    using System;
    using System.IO;

    public class IMediaTypeDummy : IMediaType
    {
        public IContent ToBody(StreamReader reader)
        {
            throw new NotSupportedException();
        }
    }
}