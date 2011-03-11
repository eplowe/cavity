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

        private IRepository<T> Repository
        {
            get
            {
                return this;
            }
        }

        bool IRepository<T>.Delete(AbsoluteUri urn)
        {
            var record = Repository.Select(urn);
            if (null == record)
            {
                return false;
            }

            Records.Remove(record);

            return true;
        }

        bool IRepository<T>.Delete(AlphaDecimal key)
        {
            var record = Repository.Select(key);
            if (null == record)
            {
                return false;
            }

            Records.Remove(record);

            return true;
        }

        bool IRepository<T>.Exists(AbsoluteUri urn)
        {
            return null != Repository.Select(urn);
        }

        bool IRepository<T>.Exists(AlphaDecimal key)
        {
            return null != Repository.Select(key);
        }

        bool IRepository<T>.Exists(XPathExpression xpath)
        {
            throw new NotImplementedException();
        }

        IRecord<T> IRepository<T>.Insert(IRecord<T> record)
        {
            if (null == record)
            {
                throw new ArgumentNullException("record");
            }

            if (null == record.Cacheability)
            {
                throw new RepositoryException();
            }

            if (null == record.Expiration)
            {
                throw new RepositoryException();
            }

            if (null == record.Status)
            {
                throw new RepositoryException();
            }

            if (null == record.Urn)
            {
                throw new RepositoryException();
            }

            if (record.Key.HasValue)
            {
                throw new RepositoryException();
            }

            if (null != Repository.Select(record.Urn))
            {
                throw new RepositoryException();
            }

            var date = DateTime.UtcNow;
            record.Created = date;
            record.Key = AlphaDecimal.Random();
            record.Modified = date;

            Records.Add(record);

            return record;
        }

        bool IRepository<T>.Match(AbsoluteUri urn,
                                  string etag)
        {
            if (null == urn)
            {
                throw new ArgumentNullException("urn");
            }

            return null != Records
                               .Where(x => x.Urn.Equals(urn) && x.Etag.Equals(etag))
                               .FirstOrDefault();
        }

        bool IRepository<T>.Match(AlphaDecimal key,
                                  string etag)
        {
            return null != Records
                               .Where(x => x.Key.Equals(key) && x.Etag.Equals(etag))
                               .FirstOrDefault();
        }

        bool IRepository<T>.ModifiedSince(AbsoluteUri urn,
                                          DateTime value)
        {
            if (null == urn)
            {
                throw new ArgumentNullException("urn");
            }

            return null != Records
                               .Where(x => x.Urn.Equals(urn) && x.Modified > value)
                               .FirstOrDefault();
        }

        bool IRepository<T>.ModifiedSince(AlphaDecimal key,
                                          DateTime value)
        {
            return null != Records
                               .Where(x => x.Key.Equals(key) && x.Modified > value)
                               .FirstOrDefault();
        }

        IEnumerable<IRecord<T>> IRepository<T>.Query(XPathExpression xpath)
        {
            throw new NotImplementedException();
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
            return Records
                .Where(x => x.Key.Equals(key))
                .FirstOrDefault();
        }

        AlphaDecimal? IRepository<T>.ToKey(AbsoluteUri urn)
        {
            var record = Repository.Select(urn);

            return null == record
                       ? null
                       : record.Key;
        }

        AbsoluteUri IRepository<T>.ToUrn(AlphaDecimal key)
        {
            var record = Repository.Select(key);

            return null == record
                       ? null
                       : record.Urn;
        }

        bool IRepository<T>.Update(IRecord<T> record)
        {
            if (null == record)
            {
                throw new ArgumentNullException("record");
            }

            if (null == record.Cacheability)
            {
                throw new RepositoryException();
            }

            if (null == record.Expiration)
            {
                throw new RepositoryException();
            }

            if (null == record.Status)
            {
                throw new RepositoryException();
            }

            if (null == record.Urn)
            {
                throw new RepositoryException();
            }

            if (!record.Key.HasValue)
            {
                throw new RepositoryException();
            }

            var existing = Repository.Select(record.Key.Value);
            if (null == existing)
            {
                throw new RepositoryException();
            }

            return true;
        }

        IRecord<T> IRepository<T>.Upsert(IRecord<T> record)
        {
            if (null == record)
            {
                throw new ArgumentNullException("record");
            }

            if (null == record.Key)
            {
                return Repository.Insert(record);
            }

            Repository.Update(record);

            return Repository.Select(record.Key.Value);
        }
    }
}