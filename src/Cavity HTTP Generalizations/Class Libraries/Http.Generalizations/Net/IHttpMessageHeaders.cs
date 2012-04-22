namespace Cavity.Net
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This naming is intentional.")]
    public interface IHttpMessageHeaders : IEnumerable<HttpHeader>
    {
        [SuppressMessage("Microsoft.Design", "CA1043:UseIntegralOrStringArgumentForIndexers", Justification = "Token is the correct type.")]
        string this[Token key] { get; set; }

        void Add(HttpHeader header);

        bool Contains(Token name);
    }
}