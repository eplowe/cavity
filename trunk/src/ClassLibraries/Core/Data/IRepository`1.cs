namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Xml.XPath;

    public interface IRepository<T>
    {
        bool Delete(AbsoluteUri urn);

        bool Delete(AlphaDecimal key);

        bool Exists(AbsoluteUri urn);

        bool Exists(AlphaDecimal key);

        bool Exists(XPathExpression xpath);

        IRecord<T> Insert(IRecord<T> record);

        bool Match(AbsoluteUri urn,
                   string etag);

        bool Match(AlphaDecimal key,
                   string etag);

        bool ModifiedSince(AbsoluteUri urn,
                           DateTime value);

        bool ModifiedSince(AlphaDecimal key,
                           DateTime value);

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This design is intentional.")]
        IEnumerable<IRecord<T>> Query(XPathExpression xpath);

        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Select", Justification = "The naming is intentional.")]
        IRecord<T> Select(AbsoluteUri urn);

        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Select", Justification = "The naming is intentional.")]
        IRecord<T> Select(AlphaDecimal key);

        AlphaDecimal? ToKey(AbsoluteUri urn);

        AbsoluteUri ToUrn(AlphaDecimal key);

        bool Update(IRecord<T> record);

        void Upsert(IRecord<T> record);
    }
}