namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryDeleteKeyNotFound<T> : VerifyRepositoryBase<T>
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            if (repository.Delete(AlphaDecimal.Random()))
            {
                throw new UnitTestException(Resources.Repository_ExpectWhenDoesNotExist_UnitTestExceptionMessage.FormatWith("Delete", "false"));
            }
        }
    }
}