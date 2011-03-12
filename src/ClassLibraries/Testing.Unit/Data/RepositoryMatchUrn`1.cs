namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryMatchUrn<T> : VerifyRepositoryBase<T>
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            repository.Insert(Record.Object);

            if (repository.Match(Record.Object.Urn, Record.Object.Etag))
            {
                return;
            }

            throw new UnitTestException(Resources.Repository_Match_ReturnsFalse_UnitTestExceptionMessage);
        }
    }
}