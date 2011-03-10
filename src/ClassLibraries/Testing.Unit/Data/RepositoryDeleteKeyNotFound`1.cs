namespace Cavity.Data
{
    using System;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryDeleteKeyNotFound<T> : IVerifyRepository<T>
    {
        public void Verify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            using (new TransactionScope())
            {
                if (repository.Delete(AlphaDecimal.Random()))
                {
                    throw new UnitTestException(Resources.Repository_Delete_ReturnsTrue_UnitTestExceptionMessage);
                }
            }
        }
    }
}