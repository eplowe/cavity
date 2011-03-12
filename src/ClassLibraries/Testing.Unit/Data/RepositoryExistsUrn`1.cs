namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryExistsUrn<T> : VerifyRepositoryBase<T> where T : new()
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

            throw new UnitTestException(Resources.Repository_ExpectWhenExists_UnitTestExceptionMessage.FormatWith("Exists", "true"));
        }
    }
}