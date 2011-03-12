namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryModifiedSinceUrn<T> : VerifyRepositoryBase<T>
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            repository.Insert(Record.Object);

            if (repository.ModifiedSince(Record.Object.Urn, DateTime.MaxValue))
            {
                throw new UnitTestException(Resources.Repository_ModifiedSince_ReturnsTrue_UnitTestExceptionMessage);
            }

            if (repository.ModifiedSince(Record.Object.Urn, DateTime.MinValue))
            {
                return;
            }

            throw new UnitTestException(Resources.Repository_ModifiedSince_ReturnsFalse_UnitTestExceptionMessage);
        }
    }
}