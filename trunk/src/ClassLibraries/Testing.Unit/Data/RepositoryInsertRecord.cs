namespace Cavity.Data
{
    using System;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;
    using Moq;

    public sealed class RepositoryInsertRecord : IExpectRepository
    {
        public RepositoryInsertRecord()
        {
            Record = new Mock<IRecord>().SetupProperty(x => x.Key);
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
                var record = repository.Insert(Record.Object);
                if (record.Key.HasValue)
                {
                    Record.VerifyAll();
                    return;
                }

                throw new UnitTestException(Resources.RepositoryInsertRecord_UnitTestExceptionMessage);
            }
        }
    }
}