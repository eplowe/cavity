namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryDeleteUrn<T> : VerifyRepositoryBase<T>
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            repository.Insert(Record.Object);

            if (!repository.Delete(Record.Object.Urn))
            {
                throw new UnitTestException(Resources.Repository_Delete_ReturnsFalse_UnitTestExceptionMessage);
            }

            if (repository.Exists(Record.Object.Urn))
            {
                throw new UnitTestException(Resources.Repository_Exists_ReturnsTrue_UnitTestExceptionMessage);
            }
        }
    }
}