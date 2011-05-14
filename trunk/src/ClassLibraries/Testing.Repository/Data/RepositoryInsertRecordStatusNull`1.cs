namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryInsertRecordStatusNull<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        public RepositoryInsertRecordStatusNull()
        {
            Record1.Status = null;
        }

        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            RepositoryException expected = null;
            try
            {
                repository.Insert(Record1);
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
                throw new UnitTestException(Resources.Repository_ExpectExceptionWhenRecordIncomplete_UnitTestExceptionMessage.FormatWith("Insert", "status"));
            }
        }
    }
}