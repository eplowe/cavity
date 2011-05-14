namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryExistsUrnNotFound<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            if (repository.Exists("urn://example.com/" + Guid.NewGuid()))
            {
                throw new UnitTestException(Resources.Repository_ExpectWhenDoesNotExist_UnitTestExceptionMessage.FormatWith("Exists", "false"));
            }
        }
    }
}