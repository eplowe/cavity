namespace Cavity.Net
{
    using System.Collections.Generic;
    using System.Net.Mime;

    public interface IHttpHeaderCollection : ICollection<IHttpHeader>
    {
        ContentType ContentType { get; }

        string this[string name]
        {
            get;
        }
    }
}