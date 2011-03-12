﻿namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryUpdateRecord<T> : VerifyRepositoryBase<T>
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            var record = repository.Insert(Record.Object);
            if (record.Key == repository.Update(record).Key)
            {
                return;
            }

            throw new UnitTestException(Resources.Repository_Update_ReturnsFalse_UnitTestExceptionMessage);
        }
    }
}