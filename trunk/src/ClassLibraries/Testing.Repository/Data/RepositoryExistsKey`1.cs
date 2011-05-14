namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryExistsKey<T> : VerifyRepositoryBase<T>
        where T : new()
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

            if (repository.Exists(Record1.Key.Value))
            {
                return;
            }

            throw new UnitTestException(Resources.Repository_ExpectWhenExists_UnitTestExceptionMessage.FormatWith("Exists", "true"));
        }
    }
}