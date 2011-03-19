namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Xml.XPath;
    using Cavity.Net;

    public sealed class FileRepository<T> : IRepository<T>
    {
        bool IRepository<T>.Delete(AbsoluteUri urn)
        {
            throw new NotImplementedException();
        }

        bool IRepository<T>.Delete(AlphaDecimal key)
        {
            throw new NotImplementedException();
        }

        bool IRepository<T>.Exists(AbsoluteUri urn)
        {
            throw new NotImplementedException();
        }

        bool IRepository<T>.Exists(AlphaDecimal key)
        {
            throw new NotImplementedException();
        }

        IRecord<T> IRepository<T>.Insert(IRecord<T> record)
        {
            throw new NotImplementedException();
        }

        bool IRepository<T>.Match(AbsoluteUri urn, EntityTag etag)
        {
            throw new NotImplementedException();
        }

        bool IRepository<T>.Match(AlphaDecimal key, EntityTag etag)
        {
            throw new NotImplementedException();
        }

        bool IRepository<T>.ModifiedSince(AbsoluteUri urn, DateTime value)
        {
            throw new NotImplementedException();
        }

        bool IRepository<T>.ModifiedSince(AlphaDecimal key, DateTime value)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IRecord<T>> IRepository<T>.Query(XPathExpression expression)
        {
            throw new NotImplementedException();
        }

        IRecord<T> IRepository<T>.Select(AbsoluteUri urn)
        {
            throw new NotImplementedException();
        }

        IRecord<T> IRepository<T>.Select(AlphaDecimal key)
        {
            throw new NotImplementedException();
        }

        AlphaDecimal? IRepository<T>.ToKey(AbsoluteUri urn)
        {
            throw new NotImplementedException();
        }

        AbsoluteUri IRepository<T>.ToUrn(AlphaDecimal key)
        {
            throw new NotImplementedException();
        }

        IRecord<T> IRepository<T>.Update(IRecord<T> record)
        {
            throw new NotImplementedException();
        }

        IRecord<T> IRepository<T>.Upsert(IRecord<T> record)
        {
            throw new NotImplementedException();
        }
    }
}