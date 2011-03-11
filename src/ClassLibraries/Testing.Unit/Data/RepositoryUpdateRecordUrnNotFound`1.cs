namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;
    using Moq;

    public sealed class RepositoryUpdateRecordUrnNotFound<T> : IVerifyRepository<T>
    {
        public RepositoryUpdateRecordUrnNotFound()
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
                .SetupProperty(x => x.Urn);
            record.Object.Urn = "urn://example.com/" + Guid.NewGuid();
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
                var record = repository.Insert(Record.Object);
                record.Urn = "urn://example.com/" + Guid.NewGuid();

                if (repository.Update(record))
                {
                    return;
                }

                throw new UnitTestException(Resources.Repository_Update_ReturnsFalse_UnitTestExceptionMessage);
            }
        }
    }
}