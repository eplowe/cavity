namespace Cavity.Net.Mime
{
    using System;
    using System.IO;

    public class IContentDummy : IContentTypeDummy, IContent
    {
        public object Content
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

        public void Write(TextWriter writer)
        {
            throw new NotSupportedException();
        }
    }
}