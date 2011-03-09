namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.XPath;

    public sealed class FakeRepository<T> : IRepository<T>
    {
        public FakeRepository()
        {
            Records = new HashSet<IRecord<T>>();
        }

        private HashSet<IRecord<T>> Records { get; set; }

        bool IRepository<T>.Delete(AbsoluteUri urn)
        {
            throw new System.NotImplementedException();
        }

        bool IRepository<T>.Delete(AlphaDecimal key)
        {
            throw new System.NotImplementedException();
        }

        bool IRepository<T>.Exists(AbsoluteUri urn)
        {
            throw new System.NotImplementedException();
        }

        bool IRepository<T>.Exists(AlphaDecimal key)
        {
            throw new System.NotImplementedException();
        }

        bool IRepository<T>.Exists(XPathExpression xpath)
        {
            throw new System.NotImplementedException();
        }

        IRecord<T> IRepository<T>.Insert(IRecord<T> record)
        {
            if (null == record)
            {
                throw new ArgumentNullException("record");
            }

            if (null == record.Urn)
            {
                throw new RepositoryException();
            }

            if (Records.Where(x => x.Urn.Equals(record.Urn)).Any())
            {
                throw new RepositoryException();
            }

            if (!record.Key.HasValue)
            {
                record.Key = AlphaDecimal.Random();
            }

            if (Records.Where(x => x.Key.Equals(record.Key)).Any())
            {
                throw new RepositoryException();
            }

            Records.Add(record);

            return record;
        }

        bool IRepository<T>.Match(AbsoluteUri urn, string etag)
        {
            throw new System.NotImplementedException();
        }

        bool IRepository<T>.Match(AlphaDecimal key, string etag)
        {
            throw new System.NotImplementedException();
        }

        bool IRepository<T>.ModifiedSince(AbsoluteUri urn, string etag)
        {
            throw new System.NotImplementedException();
        }

        bool IRepository<T>.ModifiedSince(AlphaDecimal key, string etag)
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<IRecord<T>> IRepository<T>.Query(XPathExpression xpath)
        {
            throw new System.NotImplementedException();
        }

        IRecord<T> IRepository<T>.Select(AbsoluteUri urn)
        {
            if (null == urn)
            {
                throw new ArgumentNullException("urn");
            }

            return Records
                .Where(x => x.Urn.Equals(urn))
                .FirstOrDefault();
        }

        IRecord<T> IRepository<T>.Select(AlphaDecimal key)
        {
            throw new System.NotImplementedException();
        }

        AlphaDecimal? IRepository<T>.Key(AbsoluteUri urn)
        {
            throw new System.NotImplementedException();
        }

        bool IRepository<T>.Update(IRecord<T> record)
        {
            throw new System.NotImplementedException();
        }

        void IRepository<T>.Upsert(IRecord<T> record)
        {
            throw new System.NotImplementedException();
        }
    }
}