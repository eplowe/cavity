namespace Cavity.Net
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public interface IHttpMessageHeaders
    {
        IEnumerable<HttpHeader> List { get; }

        [SuppressMessage("Microsoft.Design", "CA1043:UseIntegralOrStringArgumentForIndexers", Justification = "Token is the correct type.")]
        string this[Token key] { get; set; }

        void Add(HttpHeader header);
    }
}