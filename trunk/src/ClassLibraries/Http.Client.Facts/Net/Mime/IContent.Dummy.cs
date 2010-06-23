namespace Cavity.Net.Mime
{
    using System;
    using System.IO;

    public class IContentDummy : IContentTypeDummy, IContent
    {
        public void Write(TextWriter writer)
        {
            throw new NotSupportedException();
        }
    }
}