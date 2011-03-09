namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;
    using Moq;

    public sealed class RepositoryInsertRecordKeyExists<T> : IVerifyRepository<T>
    {
        public RepositoryInsertRecordKeyExists()
        {
            var key = AlphaDecimal.Random();

            var record = new Mock<IRecord<T>>();
            record
                .SetupGet(x => x.Key)
                .Returns(key);
            record
                .SetupGet(x => x.Urn)
                .Returns("urn://example.com/" + Guid.NewGuid());
            Record = record;

            var duplicate = new Mock<IRecord<T>>();
            duplicate
                .SetupGet(x => x.Key)
                .Returns(key);
            duplicate
                .SetupGet(x => x.Urn)
                .Returns("urn://example.com/" + Guid.NewGuid());

            Duplicate = duplicate;
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is required for mocking.")]
        public Mock<IRecord<T>> Duplicate { get; set; }

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
                try
                {
                    repository.Insert(Record.Object);
                }
                catch (Exception exception)
                {
                    throw new UnitTestException(Resources.Repository_UnexpectedException_UnitTestExceptionMessage, exception);
                }

                RepositoryException expected = null;
                try
                {
                    repository.Insert(Duplicate.Object);
                }
                catch (RepositoryException exception)
                {
                    expected = exception;
                }
                catch (Exception exception)
                {
                    throw new UnitTestException(Resources.Repository_Insert_RecordKeyExists_UnitTestExceptionMessage, exception);
                }

                if (null == expected)
                {
                    throw new UnitTestException(Resources.Repository_Insert_RecordKeyExists_UnitTestExceptionMessage);
                }
            }
        }
    }
}