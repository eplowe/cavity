﻿namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;
    using Moq;

    public sealed class RepositoryDeleteKey<T> : IVerifyRepository<T>
    {
        public RepositoryDeleteKey()
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
                repository.Insert(Record.Object);
                if (!Record.Object.Key.HasValue)
                {
                    throw new InvalidOperationException();
                }

                if (!repository.Delete(Record.Object.Key.Value))
                {
                    throw new UnitTestException(Resources.Repository_Delete_ReturnsFalse_UnitTestExceptionMessage);
                }

                if (repository.Exists(Record.Object.Key.Value))
                {
                    throw new UnitTestException(Resources.Repository_Exists_ReturnsTrue_UnitTestExceptionMessage);
                }
            }
        }
    }
}