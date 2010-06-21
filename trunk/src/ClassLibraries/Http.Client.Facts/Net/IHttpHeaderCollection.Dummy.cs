namespace Cavity.Net
{
    using System;
    using System.Collections.ObjectModel;
    using System.Net.Mime;

    public class IHttpHeaderCollectionDummy : Collection<IHttpHeader>, IHttpHeaderCollection
    {
        public ContentType ContentType
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public string this[string name]
        {
            get
            {
                throw new NotSupportedException();
            }
        }
    }
}