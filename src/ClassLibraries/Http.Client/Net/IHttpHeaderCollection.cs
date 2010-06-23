namespace Cavity.Net
{
    using System.Collections.Generic;
    using Cavity.Net.Mime;

    public interface IHttpHeaderCollection : ICollection<IHttpHeader>, IContentType
    {
        string this[string name]
        {
            get;
        }
    }
}