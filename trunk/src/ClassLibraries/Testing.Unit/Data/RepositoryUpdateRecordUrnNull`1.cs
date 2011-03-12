namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;
    using Moq;

    public sealed class RepositoryUpdateRecordUrnNull<T> : IVerifyRepository<T>
    {
        public RepositoryUpdateRecordUrnNull()
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
                record.Urn = null;

                RepositoryException expected = null;
                try
                {
                    repository.Update(record);
                }
                catch (RepositoryException exception)
                {
                    expected = exception;
                }
                catch (Exception exception)
                {
                    throw new UnitTestException(Resources.Repository_UnexpectedException_UnitTestExceptionMessage, exception);
                }

                if (null == expected)
                {
                    throw new UnitTestException(Resources.Repository_ExpectExceptionWhenRecordIncomplete_UnitTestExceptionMessage.FormatWith("Update", "URN"));
                }
            }
        }
    }
}