namespace Cavity.Data
{
    using System;
    using Cavity.Net;

    public interface IRecord<T>
    {
        string Cacheability { get; set; }

        DateTime? Created { get; set; }

        EntityTag Etag { get; set; }

        string Expiration { get; set; }

        AlphaDecimal? Key { get; set; }

        DateTime? Modified { get; set; }

        int? Status { get; set; }

        AbsoluteUri Urn { get; set; }

        T Value { get; set; }

        string ToEntity();
    }
}