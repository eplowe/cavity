namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryMatchKey<T> : VerifyRepositoryBase<T> where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            repository.Insert(Record.Object);

            if (!Record.Object.Key.HasValue)
            {
                throw new InvalidOperationException();
            }

            if (repository.Match(Record.Object.Key.Value, Record.Object.Etag))
            {
                return;
            }

            throw new UnitTestException(Resources.Repository_Match_ReturnsFalse_UnitTestExceptionMessage);
        }
    }
}