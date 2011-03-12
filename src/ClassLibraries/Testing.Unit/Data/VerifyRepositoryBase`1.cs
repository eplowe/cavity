namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Transactions;
    using Moq;

    public abstract class VerifyRepositoryBase<T> : IVerifyRepository<T>
    {
        protected VerifyRepositoryBase()
        {
            var record = new Mock<IRecord<T>>();
            record
                .SetupGet(x => x.Cacheability)
                .Returns("public");
            record
                .SetupProperty(x => x.Created);
            record
                .SetupGet(x => x.Expiration)
                .Returns("P1D");
            record
                .SetupProperty(x => x.Key);
            record
                .SetupProperty(x => x.Modified);
            record
                .SetupGet(x => x.Status)
                .Returns(200);
            record
                .SetupGet(x => x.Urn)
                .Returns("urn://example.com/" + Guid.NewGuid());
            Record = record;
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is required for mocking.")]
        public Mock<IRecord<T>> Record { get; set; }

        public void Verify(IRepository<T> repository)
        {
            using (new TransactionScope())
            {
                OnVerify(repository);
            }
        }

        protected abstract void OnVerify(IRepository<T> repository);
    }
}