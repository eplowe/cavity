namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryToKeyUrnNotFound<T> : VerifyRepositoryBase<T>
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            var key = repository.ToKey("urn://example.com/" + Guid.NewGuid());

            if (null != key)
            {
                throw new UnitTestException(Resources.Repository_ToKey_UrnNotFound_UnitTestExceptionMessage);
            }
        }
    }
}