﻿namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryMatchUrn<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            repository.Insert(Record1);

            if (repository.Match(Record1.Urn, Record1.Etag))
            {
                return;
            }

            throw new UnitTestException(Resources.Repository_Match_ReturnsFalse_UnitTestExceptionMessage);
        }
    }
}