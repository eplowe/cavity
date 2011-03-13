﻿namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryToKeyUrn<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            var insert = repository.Insert(Record1).Key;
            var key = repository.ToKey(Record1.Urn);

            if (null == key)
            {
                throw new UnitTestException(Resources.Repository_ExpectResult_UnitTestExceptionMessage.FormatWith("ToKey", "key"));
            }

            if (key != insert)
            {
                throw new UnitTestException(Resources.Repository_ExpectCorrectValue_UnitTestExceptionMessage.FormatWith("ToKey", "key"));
            }
        }
    }
}