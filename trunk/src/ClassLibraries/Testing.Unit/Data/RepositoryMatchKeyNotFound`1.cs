namespace Cavity.Data
{
    using System;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryMatchKeyNotFound<T> : IVerifyRepository<T>
    {
        public void Verify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            using (new TransactionScope())
            {
                if (repository.Match(AlphaDecimal.Random(), Guid.NewGuid().ToString()))
                {
                    throw new UnitTestException(Resources.Repository_Match_ReturnsTrue_UnitTestExceptionMessage);
                }
            }
        }
    }
}