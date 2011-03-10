namespace Cavity.Data
{
    using System;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryToUrnKeyNotFound<T> : IVerifyRepository<T>
    {
        public void Verify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            using (new TransactionScope())
            {
                var urn = repository.ToUrn(AlphaDecimal.Random());

                if (null != urn)
                {
                    throw new UnitTestException(Resources.Repository_ToUrn_KeyNotFound_UnitTestExceptionMessage);
                }
            }
        }
    }
}