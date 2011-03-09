namespace Cavity.Data
{
    using System;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;
    using Moq;

    public sealed class RepositoryInsertRecordUrnNull : IExpectRepository
    {
        public RepositoryInsertRecordUrnNull()
        {
            var record = new Mock<IRecord>()
                .SetupProperty(x => x.Key);
            record
                .SetupGet(x => x.Urn)
                .Returns(null as AbsoluteUri);
            Record = record;
        }

        public Mock<IRecord> Record { get; set; }

        public void Verify(IRepository repository)
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
                    throw new UnitTestException(Resources.RepositoryInsertRecordUrnNull_UnitTestExceptionMessage, exception);
                }

                if (null == expected)
                {
                    throw new UnitTestException(Resources.RepositoryInsertRecordUrnNull_UnitTestExceptionMessage);
                }
            }
        }
    }
}