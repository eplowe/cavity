namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;
    using Moq;

    public sealed class RepositoryInsertRecordUrnNull<T> : IVerifyRepository<T>
    {
        public RepositoryInsertRecordUrnNull()
        {
            var record = new Mock<IRecord<T>>();
            record
                .SetupProperty(x => x.Key);
            record
                .SetupGet(x => x.Urn)
                .Returns(null as AbsoluteUri);
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
                RepositoryException expected = null;
                try
                {
                    repository.Insert(Record.Object);
                }
                catch (RepositoryException exception)
                {
                    expected = exception;
                }
                catch (Exception exception)
                {
                    throw new UnitTestException(Resources.Repository_Insert_RecordUrnNull_UnitTestExceptionMessage, exception);
                }

                if (null == expected)
                {
                    throw new UnitTestException(Resources.Repository_Insert_RecordUrnNull_UnitTestExceptionMessage);
                }
            }
        }
    }
}