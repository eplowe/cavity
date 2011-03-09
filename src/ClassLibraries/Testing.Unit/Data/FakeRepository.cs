namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Xml.XPath;

    public sealed class FakeRepository : IRepository
    {
        public FakeRepository()
        {
            Keys = new HashSet<AlphaDecimal>();
            Urns = new HashSet<AbsoluteUri>();
        }

        private HashSet<AlphaDecimal> Keys { get; set; }

        private HashSet<AbsoluteUri> Urns { get; set; }

        bool IRepository.Delete(AbsoluteUri urn)
        {
            throw new System.NotImplementedException();
        }

        bool IRepository.Delete(AlphaDecimal key)
        {
            throw new System.NotImplementedException();
        }

        bool IRepository.Exists(AbsoluteUri urn)
        {
            throw new System.NotImplementedException();
        }

        bool IRepository.Exists(AlphaDecimal key)
        {
            throw new System.NotImplementedException();
        }

        bool IRepository.Exists(XPathExpression xpath)
        {
            throw new System.NotImplementedException();
        }

        IRecord IRepository.Insert(IRecord record)
        {
            if (null == record)
            {
                throw new ArgumentNullException("record");
            }

            if (null == record.Urn)
            {
                throw new RepositoryException();
            }

            if (Urns.Contains(record.Urn))
            {
                throw new RepositoryException();
            }

            if (!record.Key.HasValue)
            {
                record.Key = AlphaDecimal.Random();
            }

            if (Keys.Contains(record.Key.Value))
            {
                throw new RepositoryException();
            }

            Keys.Add(record.Key.Value);
            Urns.Add(record.Urn);

            return record;
        }

        bool IRepository.Match(AbsoluteUri urn, string etag)
        {
            throw new System.NotImplementedException();
        }

        bool IRepository.Match(AlphaDecimal key, string etag)
        {
            throw new System.NotImplementedException();
        }

        bool IRepository.ModifiedSince(AbsoluteUri urn, string etag)
        {
            throw new System.NotImplementedException();
        }

        bool IRepository.ModifiedSince(AlphaDecimal key, string etag)
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<T> IRepository.Query<T>(XPathExpression xpath)
        {
            throw new System.NotImplementedException();
        }

        T IRepository.Select<T>(AbsoluteUri urn)
        {
            throw new System.NotImplementedException();
        }

        T IRepository.Select<T>(AlphaDecimal key)
        {
            throw new System.NotImplementedException();
        }

        AlphaDecimal? IRepository.Key(AbsoluteUri urn)
        {
            throw new System.NotImplementedException();
        }

        bool IRepository.Update(IRecord record)
        {
            throw new System.NotImplementedException();
        }

        void IRepository.Upsert(IRecord record)
        {
            throw new System.NotImplementedException();
        }
    }
}