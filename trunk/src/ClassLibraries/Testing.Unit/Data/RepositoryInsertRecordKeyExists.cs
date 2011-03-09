namespace Cavity.Data
{
    using System;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;
    using Moq;

    public sealed class RepositoryInsertRecordKeyExists : IExpectRepository
    {
        public RepositoryInsertRecordKeyExists()
        {
            var key = AlphaDecimal.Random();

            var record = new Mock<IRecord>();
            record
                .SetupGet(x => x.Key)
                .Returns(key)
                .Verifiable();
            Record = record;

            var duplicate = new Mock<IRecord>()
                .SetupProperty(x => x.Key);
            duplicate.Object.Key = key;

            Duplicate = duplicate;
        }

        public Mock<IRecord> Duplicate { get; set; }

        public Mock<IRecord> Record { get; set; }

        public void Verify(IRepository repository)
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
                    throw new UnitTestException(Resources.Repository_UnexpectedException_Message, exception);
                }

                RepositoryException expected = null;
                try
                {
                    repository.Insert(Duplicate.Object);
                    Record.VerifyAll();
                    Duplicate.VerifyAll();
                }
                catch (RepositoryException exception)
                {
                    expected = exception;
                }
                catch (Exception exception)
                {
                    throw new UnitTestException(Resources.RepositoryInsertRecordKeyExists_UnitTestExceptionMessage, exception);
                }

                if (null == expected)
                {
                    throw new UnitTestException(Resources.RepositoryInsertRecordKeyExists_UnitTestExceptionMessage);
                }
            }
        }
    }
}