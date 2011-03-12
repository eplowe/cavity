namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryDeleteKey<T> : VerifyRepositoryBase<T>
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

            if (!repository.Delete(Record.Object.Key.Value))
            {
                throw new UnitTestException(Resources.Repository_Delete_ReturnsFalse_UnitTestExceptionMessage);
            }

            if (repository.Exists(Record.Object.Key.Value))
            {
                throw new UnitTestException(Resources.Repository_Exists_ReturnsTrue_UnitTestExceptionMessage);
            }
        }
    }
}