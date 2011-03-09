namespace Cavity.Data
{
    using System;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryInsertRecordNull<T> : IVerifyRepository<T>
    {
        public void Verify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            using (new TransactionScope())
            {
                ArgumentNullException expected = null;
                try
                {
                    repository.Insert(null);
                }
                catch (ArgumentNullException exception)
                {
                    expected = exception;
                }
                catch (Exception exception)
                {
                    throw new UnitTestException(Resources.Repository_Insert_RecordNull_UnitTestExceptionMessage, exception);
                }

                if (null == expected)
                {
                    throw new UnitTestException(Resources.Repository_Insert_RecordNull_UnitTestExceptionMessage);
                }
            }
        }
    }
}