namespace Cavity.Data
{
    using System;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositorySelectUrnNotFound<T> : IVerifyRepository<T>
    {
        public void Verify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            using (new TransactionScope())
            {
                var record = repository.Select("urn://example.com/" + Guid.NewGuid());

                if (null != record)
                {
                    throw new UnitTestException(Resources.Repository_Select_UrnNotFound_UnitTestExceptionMessage);
                }
            }
        }
    }
}