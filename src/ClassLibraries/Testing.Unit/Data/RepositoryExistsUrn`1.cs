namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryExistsUrn<T> : VerifyRepositoryBase<T>
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            repository.Insert(Record.Object);

            if (repository.Exists(Record.Object.Urn))
            {
                return;
            }

            throw new UnitTestException(Resources.Repository_Exists_ReturnsFalse_UnitTestExceptionMessage);
        }
    }
}