namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryToUrnKey<T> : VerifyRepositoryBase<T> where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            var insert = repository.Insert(Record.Object).Urn;

            if (!Record.Object.Key.HasValue)
            {
                throw new InvalidOperationException();
            }

            var urn = repository.ToUrn(Record.Object.Key.Value);

            if (null == urn)
            {
                throw new UnitTestException(Resources.Repository_ExpectResult_UnitTestExceptionMessage.FormatWith("ToUrn", "URN"));
            }

            if (urn != insert)
            {
                throw new UnitTestException(Resources.Repository_ExpectCorrectValue_UnitTestExceptionMessage.FormatWith("ToUrn", "URN"));
            }
        }
    }
}