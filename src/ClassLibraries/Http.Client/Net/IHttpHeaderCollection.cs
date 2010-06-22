namespace Cavity.Net
{
    using System.Collections.Generic;
    using System.Net.Mime;
    using Cavity.Net.Mime;

    public interface IHttpHeaderCollection : ICollection<IHttpHeader>, IContentType
    {
        string this[string name]
        {
            get;
        }
    }
}