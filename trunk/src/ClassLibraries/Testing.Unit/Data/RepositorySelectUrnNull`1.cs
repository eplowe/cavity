namespace Cavity.Data
{
    using System;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositorySelectUrnNull<T> : IVerifyRepository<T>
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
                    repository.Select(null);
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
                    throw new UnitTestException(Resources.Repository_Select_UrnNull_UnitTestExceptionMessage);
                }
            }
        }
    }
}