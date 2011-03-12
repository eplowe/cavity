namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryExistsKeyNotFound<T> : VerifyRepositoryBase<T>
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            if (repository.Exists(AlphaDecimal.Random()))
            {
                throw new UnitTestException(Resources.Repository_Exists_ReturnsTrue_UnitTestExceptionMessage);
            }
        }
    }
}