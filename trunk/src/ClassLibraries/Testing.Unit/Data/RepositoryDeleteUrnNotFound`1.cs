namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryDeleteUrnNotFound<T> : VerifyRepositoryBase<T>
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            if (repository.Delete("urn://example.com/" + Guid.NewGuid()))
            {
                throw new UnitTestException(Resources.Repository_Delete_ReturnsTrue_UnitTestExceptionMessage);
            }
        }
    }
}