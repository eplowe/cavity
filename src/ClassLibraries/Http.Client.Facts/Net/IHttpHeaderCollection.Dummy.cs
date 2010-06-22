namespace Cavity.Net
{
    using System;
    using System.Collections.ObjectModel;
    using System.Net.Mime;
    using Cavity.Net.Mime;

    public class IHttpHeaderCollectionDummy : Collection<IHttpHeader>, IHttpHeaderCollection
    {
        ContentType IContentType.ContentType
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