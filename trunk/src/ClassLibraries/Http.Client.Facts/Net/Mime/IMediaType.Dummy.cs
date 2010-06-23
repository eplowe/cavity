namespace Cavity.Net.Mime
{
    using System;
    using System.IO;

    public class IMediaTypeDummy : IMediaType
    {
        public IContent ToContent(TextReader reader)
        {
            throw new NotSupportedException();
        }
    }
}