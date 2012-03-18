namespace Cavity.Data
{
    using System;
    using System.Xml.XPath;

    using Cavity.Net;

    public interface IRecord
    {
        string Cacheability { get; set; }

        DateTime? Created { get; set; }

        EntityTag Etag { get; set; }

        string Expiration { get; set; }

        AlphaDecimal? Key { get; set; }

        DateTime? Modified { get; set; }

        int? Status { get; set; }

        AbsoluteUri Urn { get; set; }

        string ToEntity();

        IXPathNavigable ToXml();
    }
}