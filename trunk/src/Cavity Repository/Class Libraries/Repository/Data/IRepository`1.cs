namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Xml.XPath;

    using Cavity.Net;

    public interface IRepository<T>
    {
        bool Delete(AbsoluteUri urn);

        bool Delete(AlphaDecimal key);

        bool Exists(AbsoluteUri urn);

        bool Exists(AlphaDecimal key);

        IRecord<T> Insert(IRecord<T> record);

        bool Match(AbsoluteUri urn, 
                   EntityTag etag);

        bool Match(AlphaDecimal key, 
                   EntityTag etag);

        bool ModifiedSince(AbsoluteUri urn, 
                           DateTime value);

        bool ModifiedSince(AlphaDecimal key, 
                           DateTime value);

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This design is intentional.")]
        IEnumerable<IRecord<T>> Query(XPathExpression expression);

        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Select", Justification = "The naming is intentional.")]
        IRecord<T> Select(AbsoluteUri urn);

        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Select", Justification = "The naming is intentional.")]
        IRecord<T> Select(AlphaDecimal key);

        AlphaDecimal? ToKey(AbsoluteUri urn);

        AbsoluteUri ToUrn(AlphaDecimal key);

        IRecord<T> Update(IRecord<T> record);

        IRecord<T> Upsert(IRecord<T> record);
    }
}