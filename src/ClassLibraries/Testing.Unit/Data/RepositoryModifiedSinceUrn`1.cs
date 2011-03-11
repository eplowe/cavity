namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;
    using Moq;

    public sealed class RepositoryModifiedSinceUrn<T> : IVerifyRepository<T>
    {
        public RepositoryModifiedSinceUrn()
        {
            var record = new Mock<IRecord<T>>();
            record
                .SetupGet(x => x.Cacheability)
                .Returns("public");
            record
                .SetupGet(x => x.Etag)
                .Returns("\"abc\"");
            record
                .SetupProperty(x => x.Key);
            record
                .SetupProperty(x => x.Modified);
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
                repository.Insert(Record.Object);

                if (repository.ModifiedSince(Record.Object.Urn, DateTime.MaxValue))
                {
                    throw new UnitTestException(Resources.Repository_ModifiedSince_ReturnsTrue_UnitTestExceptionMessage);
                }

                if (repository.ModifiedSince(Record.Object.Urn, DateTime.MinValue))
                {
                    return;
                }

                throw new UnitTestException(Resources.Repository_ModifiedSince_ReturnsFalse_UnitTestExceptionMessage);
            }
        }
    }
}