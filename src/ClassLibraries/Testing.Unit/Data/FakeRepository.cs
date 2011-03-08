namespace Cavity.Data
{
    using System.Collections.Generic;
    using System.Xml.XPath;

    public sealed class FakeRepository : IRepository
    {
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
            record.Key = AlphaDecimal.Random();

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