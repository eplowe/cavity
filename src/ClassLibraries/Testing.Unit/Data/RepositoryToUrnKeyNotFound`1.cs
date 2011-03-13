namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryToUrnKeyNotFound<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            var urn = repository.ToUrn(AlphaDecimal.Random());

            if (null != urn)
            {
                throw new UnitTestException(Resources.Repository_ExpectNullWhenRecordNotFound_UnitTestExceptionMessage.FormatWith("ToUrn", "key"));
            }
        }
    }
}