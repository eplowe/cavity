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
            var record = new Mock<IRecord>()
                .SetupProperty(x => x.Key);
            record
                .SetupGet(x => x.Urn)
                .Returns("urn://example.com/" + Guid.NewGuid());
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
                var record = repository.Insert(Record.Object);
                if (record.Key.HasValue)
                {
                    return;
                }

                throw new UnitTestException(Resources.RepositoryInsertRecord_UnitTestExceptionMessage);
            }
        }
    }
}