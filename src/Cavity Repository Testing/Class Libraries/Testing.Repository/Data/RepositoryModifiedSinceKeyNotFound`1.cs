﻿namespace Cavity.Data
{
    using System;
    using Cavity.Properties;

    public sealed class RepositoryModifiedSinceKeyNotFound<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            if (repository.ModifiedSince(AlphaDecimal.Random(), DateTime.MinValue))
            {
                throw new RepositoryTestException(Resources.Repository_ModifiedSince_ReturnsTrue_ExceptionMessage);
            }
        }
    }
}