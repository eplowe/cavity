namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;
    using Moq;

    public sealed class RepositoryMatchKey<T> : IVerifyRepository<T>
    {
        public RepositoryMatchKey()
        {
            var record = new Mock<IRecord<T>>()
                .SetupProperty(x => x.Key);
            record
                .SetupGet(x => x.Etag)
                .Returns("\"abc\"");
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
                if (!Record.Object.Key.HasValue)
                {
                    throw new InvalidOperationException();
                }

                if (repository.Match(Record.Object.Key.Value, Record.Object.Etag))
                {
                    return;
                }

                throw new UnitTestException(Resources.Repository_Match_ReturnsFalse_UnitTestExceptionMessage);
            }
        }
    }
}