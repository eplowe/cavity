﻿namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryUpdateRecordKeyNotFound<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            repository.Insert(Record1);

            RepositoryException expected = null;
            try
            {
                Record2.Key = AlphaDecimal.Random();
                repository.Update(Record2);
            }
            catch (RepositoryException exception)
            {
                expected = exception;
            }
            catch (Exception exception)
            {
                throw new UnitTestException(Resources.Repository_UnexpectedException_UnitTestExceptionMessage, exception);
            }

            if (null == expected)
            {
                throw new UnitTestException(Resources.Repository_ExpectExceptionWhenRecordKeyNotFound_UnitTestExceptionMessage.FormatWith("Update"));
            }
        }
    }
}