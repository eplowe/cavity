namespace Cavity.Data
{
    using System;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryToKeyUrnNotFound<T> : IVerifyRepository<T>
    {
        public void Verify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            using (new TransactionScope())
            {
                var key = repository.ToKey("urn://example.com/" + Guid.NewGuid());

                if (null != key)
                {
                    throw new UnitTestException(Resources.Repository_ToKey_UrnNotFound_UnitTestExceptionMessage);
                }
            }
        }
    }
}