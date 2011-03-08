namespace Cavity.Data
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Xml.XPath;

    public interface IRepository
    {
        bool Delete(AbsoluteUri urn);

        bool Delete(AlphaDecimal key);

        bool Exists(AbsoluteUri urn);

        bool Exists(AlphaDecimal key);

        bool Exists(XPathExpression xpath);

        void Insert(IRecord record);

        bool Match(AbsoluteUri urn, string etag);

        bool Match(AlphaDecimal key, string etag);

        bool ModifiedSince(AbsoluteUri urn, string etag);

        bool ModifiedSince(AlphaDecimal key, string etag);

        IEnumerable<T> Query<T>(XPathExpression xpath)
            where T : IRecord;

        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Select", Justification = "The naming is intentional.")]
        T Select<T>(AbsoluteUri urn)
            where T : IRecord;

        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Select", Justification = "The naming is intentional.")]
        T Select<T>(AlphaDecimal key)
            where T : IRecord;

        AlphaDecimal? Key(AbsoluteUri urn);

        bool Update(IRecord record);

        void Upsert(IRecord record);
    }
}