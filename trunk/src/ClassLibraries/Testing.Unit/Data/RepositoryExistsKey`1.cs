namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryExistsKey<T> : VerifyRepositoryBase<T>
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

            if (repository.Exists(Record.Object.Key.Value))
            {
                return;
            }

            throw new UnitTestException(Resources.Repository_ExpectWhenExists_UnitTestExceptionMessage.FormatWith("Exists", "true"));
        }
    }
}