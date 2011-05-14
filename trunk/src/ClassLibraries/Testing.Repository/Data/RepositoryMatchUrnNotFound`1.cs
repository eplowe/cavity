namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryMatchUrnNotFound<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            if (repository.Match("urn://example.com/" + Guid.NewGuid(), "\"{0}\"".FormatWith(Guid.NewGuid())))
            {
                throw new UnitTestException(Resources.Repository_Match_ReturnsTrue_UnitTestExceptionMessage);
            }
        }
    }
}