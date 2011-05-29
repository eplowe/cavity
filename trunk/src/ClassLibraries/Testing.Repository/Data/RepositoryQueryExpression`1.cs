﻿namespace Cavity.Data
{
    using System;
    using System.Linq;
    using System.Xml.XPath;
    using Cavity.Properties;

    public sealed class RepositoryQueryExpression<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            repository.Insert(Record1);
            repository.Insert(Record2);

            var records = repository.Query(XPathExpression.Compile("/*"));

            if (null == records)
            {
                throw new RepositoryTestException(Resources.Repository_QueryReturnsNull_ExceptionMessage);
            }

            if (0 == records.Count())
            {
                throw new RepositoryTestException(Resources.Repository_QueryReturnsEmpty_ExceptionMessage);
            }
        }
    }
}