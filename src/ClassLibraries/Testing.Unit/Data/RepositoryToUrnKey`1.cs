namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;
    using Moq;

    public sealed class RepositoryToUrnKey<T> : IVerifyRepository<T>
    {
        public RepositoryToUrnKey()
        {
            var record = new Mock<IRecord<T>>();
            record
                .SetupGet(x => x.Cacheability)
                .Returns("public");
            record
                .SetupGet(x => x.Expiration)
                .Returns("P1D");
            record
                .SetupProperty(x => x.Key);
            record
                .SetupGet(x => x.Status)
                .Returns(200);
            record
                .SetupGet(x => x.Urn)
                .Returns("urn://example.com/" + Guid.NewGuid());
            Record = record;
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is required for mocking.")]
        public Mock<IRecord<T>> Record { get; set; }

        public void Verify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            using (new TransactionScope())
            {
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
}