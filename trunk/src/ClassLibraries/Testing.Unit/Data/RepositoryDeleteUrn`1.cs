namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryDeleteUrn<T> : VerifyRepositoryBase<T> where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            repository.Insert(Record1);

            if (!repository.Delete(Record1.Urn))
            {
                throw new UnitTestException(Resources.Repository_ExpectTrueWhenExistingRecord_UnitTestExceptionMessage.FormatWith("Delete", "deleted"));
            }

            if (repository.Exists(Record1.Urn))
            {
                throw new UnitTestException(Resources.Repository_ExpectWhenDoesNotExist_UnitTestExceptionMessage.FormatWith("Exists", "false"));
            }
        }
    }
}