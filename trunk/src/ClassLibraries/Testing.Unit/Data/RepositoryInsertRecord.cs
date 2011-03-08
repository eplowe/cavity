namespace Cavity.Data
{
    using System;
    using System.Transactions;
    using Cavity.Tests;
    using Moq;

    public sealed class RepositoryInsertRecord : IExpectRepository
    {
        public RepositoryInsertRecord()
        {
        }

        public RepositoryInsertRecord(IRecord record)
        {
            Record = record;
        }

        public IRecord Record { get; set; }

        public void Verify(IRepository repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            var record = Record ?? new Mock<IRecord>().SetupProperty(x => x.Key).Object;

            using (new TransactionScope())
            {
                record = repository.Insert(record);
                if (record.Key.HasValue)
                {
                    return;
                }

                throw new UnitTestException("Insert(IRecord) should populate the key.");
            }
        }
    }
}