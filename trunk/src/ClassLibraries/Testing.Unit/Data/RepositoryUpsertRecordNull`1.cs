namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryUpsertRecordNull<T> : VerifyRepositoryBase<T> where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            ArgumentNullException expected = null;
            try
            {
                repository.Upsert(null);
            }
            catch (ArgumentNullException exception)
            {
                expected = exception;
            }
            catch (Exception exception)
            {
                throw new UnitTestException(Resources.Repository_UnexpectedException_UnitTestExceptionMessage, exception);
            }

            if (null == expected)
            {
                throw new UnitTestException(Resources.Repository_ExpectExceptionWhenRecordNull_UnitTestExceptionMessage.FormatWith("Upsert"));
            }
        }
    }
}