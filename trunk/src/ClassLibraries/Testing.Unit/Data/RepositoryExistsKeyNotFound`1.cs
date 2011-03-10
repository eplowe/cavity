namespace Cavity.Data
{
    using System;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryExistsKeyNotFound<T> : IVerifyRepository<T>
    {
        public void Verify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            using (new TransactionScope())
            {
                if (repository.Exists(AlphaDecimal.Random()))
                {
                    throw new UnitTestException(Resources.Repository_Exists_ReturnsTrue_UnitTestExceptionMessage);
                }
            }
        }
    }
}