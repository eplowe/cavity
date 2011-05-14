namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryToUrnKey<T> : VerifyRepositoryBase<T>
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

            var urn = repository.ToUrn(Record1.Key.Value);

            if (null == urn)
            {
                throw new UnitTestException(Resources.Repository_ExpectResult_UnitTestExceptionMessage.FormatWith("ToUrn", "URN"));
            }

            if (urn != Record1.Urn)
            {
                throw new UnitTestException(Resources.Repository_ExpectCorrectValue_UnitTestExceptionMessage.FormatWith("ToUrn", "URN"));
            }
        }
    }
}