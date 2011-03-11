namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;
    using Moq;

    public sealed class RepositoryUpdateRecordCacheabilityNull<T> : IVerifyRepository<T>
    {
        public RepositoryUpdateRecordCacheabilityNull()
        {
            AbsoluteUri urn = "urn://example.com/" + Guid.NewGuid();

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
                .Returns(urn);
            Record = record;

            var record2 = new Mock<IRecord<T>>();
            record2
                .SetupProperty(x => x.Cacheability);
            record2
                .SetupGet(x => x.Expiration)
                .Returns("P1D");
            record2
                .SetupProperty(x => x.Key);
            record2
                .SetupGet(x => x.Status)
                .Returns(200);
            record2
                .SetupGet(x => x.Urn)
                .Returns(urn);
            Record2 = record2;
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is required for mocking.")]
        public Mock<IRecord<T>> Record { get; set; }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is required for mocking.")]
        public Mock<IRecord<T>> Record2 { get; set; }

        public void Verify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            using (new TransactionScope())
            {
                Record2.Object.Key = repository.Insert(Record.Object).Key;

                RepositoryException expected = null;
                try
                {
                    repository.Update(Record2.Object);
                }
                catch (RepositoryException exception)
                {
                    expected = exception;
                }
                catch (Exception exception)
                {
                    throw new UnitTestException(Resources.Repository_Update_RecordCacheabilityNull_UnitTestExceptionMessage, exception);
                }

                if (null == expected)
                {
                    throw new UnitTestException(Resources.Repository_Update_RecordCacheabilityNull_UnitTestExceptionMessage);
                }
            }
        }
    }
}