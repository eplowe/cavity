namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;
    using Moq;

    public sealed class RepositorySelectUrn<T> : IVerifyRepository<T>
    {
        public RepositorySelectUrn()
        {
            var record = new Mock<IRecord<T>>()
                .SetupProperty(x => x.Key);
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
                var key = repository.Insert(Record.Object).Key;
                var record = repository.Select(Record.Object.Urn);

                if (null == record)
                {
                    throw new UnitTestException(Resources.Repository_Select_ReturnsNull_UnitTestExceptionMessage);
                }

                if (key != record.Key)
                {
                    throw new UnitTestException(Resources.Repository_Select_ReturnsIncorrectKey_UnitTestExceptionMessage);
                }

                if (Record.Object.Urn !=
                    record.Urn)
                {
                    throw new UnitTestException(Resources.Repository_Select_ReturnsIncorrectUrn_UnitTestExceptionMessage);
                }

                if (!Record.Object.Value.Equals(record.Value))
                {
                    throw new UnitTestException(Resources.Repository_Select_ReturnsIncorrectValue_UnitTestExceptionMessage);
                }
            }
        }
    }
}