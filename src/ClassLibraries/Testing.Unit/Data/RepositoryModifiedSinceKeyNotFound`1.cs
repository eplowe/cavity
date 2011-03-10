namespace Cavity.Data
{
    using System;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryModifiedSinceKeyNotFound<T> : IVerifyRepository<T>
    {
        public void Verify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            using (new TransactionScope())
            {
                if (repository.ModifiedSince(AlphaDecimal.Random(), DateTime.MinValue))
                {
                    throw new UnitTestException(Resources.Repository_ModifiedSince_ReturnsTrue_UnitTestExceptionMessage);
                }
            }
        }
    }
}