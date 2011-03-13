namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryMatchKeyNotFound<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            if (repository.Match(AlphaDecimal.Random(), Guid.NewGuid().ToString()))
            {
                throw new UnitTestException(Resources.Repository_Match_ReturnsTrue_UnitTestExceptionMessage);
            }
        }
    }
}