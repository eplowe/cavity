namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryUpdateRecordExpirationNull<T> : VerifyRepositoryBase<T> where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            Record2.Key = repository.Insert(Record1).Key;
            Record2.Expiration = null;

            RepositoryException expected = null;
            try
            {
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
                throw new UnitTestException(Resources.Repository_ExpectExceptionWhenRecordIncomplete_UnitTestExceptionMessage.FormatWith("Update", "expiration"));
            }
        }
    }
}