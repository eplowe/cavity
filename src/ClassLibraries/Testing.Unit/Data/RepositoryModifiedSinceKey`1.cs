namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryModifiedSinceKey<T> : VerifyRepositoryBase<T> where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            Record1 = repository.Insert(Record1);

            if (!Record1.Key.HasValue)
            {
                throw new InvalidOperationException();
            }

            if (repository.ModifiedSince(Record1.Key.Value, DateTime.MaxValue))
            {
                throw new UnitTestException(Resources.Repository_ModifiedSince_ReturnsTrue_UnitTestExceptionMessage);
            }

            if (repository.ModifiedSince(Record1.Key.Value, DateTime.MinValue))
            {
                return;
            }

            throw new UnitTestException(Resources.Repository_ModifiedSince_ReturnsFalse_UnitTestExceptionMessage);
        }
    }
}