﻿namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;
    using Moq;

    public sealed class RepositoryInsertRecord<T> : IVerifyRepository<T>
    {
        public RepositoryInsertRecord()
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
                var record = repository.Insert(Record.Object);
                if (record.Key.HasValue)
                {
                    return;
                }

                throw new UnitTestException(Resources.Repository_Insert_ReturnsIncorrectKey_UnitTestExceptionMessage);
            }
        }
    }
}