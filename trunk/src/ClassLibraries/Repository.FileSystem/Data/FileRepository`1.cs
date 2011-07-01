namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
    using System.Xml.XPath;
    using Cavity.Configuration;
    using Cavity.IO;
    using Cavity.Net;

    public sealed class FileRepository<T> : IRepository<T>
    {
        private IRepository<T> Repository
        {
            get
            {
                return this;
            }
        }

        bool IRepository<T>.Delete(AbsoluteUri urn)
        {
            var file = GetFile(urn);
            if (null == file)
            {
                return false;
            }

            file.Delete();
            return true;
        }

        bool IRepository<T>.Delete(AlphaDecimal key)
        {
            var file = GetFile(key);
            if (null == file)
            {
                return false;
            }

            file.Delete();
            return true;
        }

        bool IRepository<T>.Exists(AbsoluteUri urn)
        {
            return null != GetFile(urn);
        }

        bool IRepository<T>.Exists(AlphaDecimal key)
        {
            return null != GetFile(key);
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

            if (Repository.Exists(record.Urn))
            {
                throw new RepositoryException();
            }

            record.Created = DateTime.UtcNow;
            record.Key = AlphaDecimal.Random();
            record.Modified = DateTime.UtcNow;

            new RecordFile(record).Save(FileRepositoryConfiguration.Directory());

            return record;
        }

        bool IRepository<T>.Match(AbsoluteUri urn,
                                  EntityTag etag)
        {
            var file = GetFile(urn);
            return null != file &&
                   string.Equals(etag, RecordFile.Load(file).ToRecord<T>().Etag, StringComparison.OrdinalIgnoreCase);
        }

        bool IRepository<T>.Match(AlphaDecimal key,
                                  EntityTag etag)
        {
            var file = GetFile(key);
            return null != file &&
                   string.Equals(etag, RecordFile.Load(file).ToRecord<T>().Etag, StringComparison.OrdinalIgnoreCase);
        }

        bool IRepository<T>.ModifiedSince(AbsoluteUri urn,
                                          DateTime value)
        {
            var record = Repository.Select(urn);
            if (null == record)
            {
                return false;
            }

            return record.Modified.HasValue
                       ? record.Modified.Value > value
                       : false;
        }

        bool IRepository<T>.ModifiedSince(AlphaDecimal key,
                                          DateTime value)
        {
            var record = Repository.Select(key);
            if (null == record)
            {
                return false;
            }

            return record.Modified.HasValue
                       ? record.Modified.Value > value
                       : false;
        }

        IEnumerable<IRecord<T>> IRepository<T>.Query(XPathExpression expression)
        {
            if (null == expression)
            {
                throw new ArgumentNullException("expression");
            }

            if (null == expression.Expression)
            {
                throw new ArgumentOutOfRangeException("expression");
            }

            var result = new List<IRecord<T>>();

            var files = FileRepositoryConfiguration
                .Directory()
                .GetFiles("*.record", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var obj = RecordFile.Load(file);
                if (string.IsNullOrEmpty(obj.Body))
                {
                    continue;
                }

                var selection = obj.ToXml().CreateNavigator().Select(expression.Expression);
                if (0 != selection.Count)
                {
                    result.Add(obj.ToRecord<T>());
                }
            }

            return result;
        }

        IRecord<T> IRepository<T>.Select(AbsoluteUri urn)
        {
            var file = GetFile(urn);

            return null == file
                       ? null
                       : RecordFile.Load(file).ToRecord<T>();
        }

        IRecord<T> IRepository<T>.Select(AlphaDecimal key)
        {
            var file = GetFile(key);

            return null == file
                       ? null
                       : RecordFile.Load(file).ToRecord<T>();
        }

        AlphaDecimal? IRepository<T>.ToKey(AbsoluteUri urn)
        {
            var file = GetFile(urn);

            return null == file
                       ? null
                       : RecordFile.Load(file).ToRecord<T>().Key;
        }

        AbsoluteUri IRepository<T>.ToUrn(AlphaDecimal key)
        {
            var file = GetFile(key);

            return null == file
                       ? null
                       : RecordFile.Load(file).ToRecord<T>().Urn;
        }

        IRecord<T> IRepository<T>.Update(IRecord<T> record)
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

            var keySelection = Repository.Select(record.Key.Value);
            if (null == keySelection)
            {
                throw new RepositoryException();
            }

            var urnSelection = Repository.Select(record.Urn);
            if (null != urnSelection &&
                keySelection.Key != urnSelection.Key)
            {
                throw new RepositoryException();
            }

            if (!Repository.Exists(record.Key.Value))
            {
                return null;
            }

            record.Modified = DateTime.UtcNow;

            new RecordFile(record).Save(FileRepositoryConfiguration.Directory());

            return record;
        }

        IRecord<T> IRepository<T>.Upsert(IRecord<T> record)
        {
            if (null == record)
            {
                throw new ArgumentNullException("record");
            }

            return null == record.Key
                       ? Repository.Insert(record)
                       : Repository.Update(record);
        }

        private static FileInfo GetFile(AbsoluteUri urn)
        {
            if (null == urn)
            {
                throw new ArgumentNullException("urn");
            }

            var dir = new DirectoryInfo(urn.ToPath(FileRepositoryConfiguration.Directory()).FullName);
            return dir.Exists
                       ? dir.GetFiles("*.record", SearchOption.TopDirectoryOnly).FirstOrDefault()
                       : null;
        }

        private static FileInfo GetFile(AlphaDecimal key)
        {
            return FileRepositoryConfiguration
                .Directory()
                .GetFiles("{0}.record".FormatWith(key), SearchOption.AllDirectories)
                .FirstOrDefault();
        }
    }
}