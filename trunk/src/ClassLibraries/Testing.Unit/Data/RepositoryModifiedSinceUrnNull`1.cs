namespace Cavity.Data
{
    using System;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryModifiedSinceUrnNull<T> : IVerifyRepository<T>
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
                    repository.ModifiedSince(null, DateTime.UtcNow);
                }
                catch (ArgumentNullException exception)
                {
                    expected = exception;
                }
                catch (Exception exception)
                {
                    throw new UnitTestException(Resources.Repository_ModifiedSince_UrnNull_UnitTestExceptionMessage, exception);
                }

                if (null == expected)
                {
                    throw new UnitTestException(Resources.Repository_ModifiedSince_UrnNull_UnitTestExceptionMessage);
                }
            }
        }
    }
}