namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Transactions;
    using Moq;

    public abstract class VerifyRepositoryBase<T> : IVerifyRepository<T> where T : new()
    {
        protected VerifyRepositoryBase()
        {
            Record = NewRecord();
            Record2 = NewRecord();
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is required for mocking.")]
        public Mock<IRecord<T>> Record { get; set; }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is required for mocking.")]
        public Mock<IRecord<T>> Record2 { get; set; }

        public void Verify(IRepository<T> repository)
        {
            using (new TransactionScope())
            {
                OnVerify(repository);
            }
        }

        protected abstract void OnVerify(IRepository<T> repository);

        private static Mock<IRecord<T>> NewRecord()
        {
            var record = new Mock<IRecord<T>>()
                .SetupProperty(x => x.Cacheability)
                .SetupProperty(x => x.Created)
                .SetupProperty(x => x.Etag)
                .SetupProperty(x => x.Expiration)
                .SetupProperty(x => x.Key)
                .SetupProperty(x => x.Modified)
                .SetupProperty(x => x.Status)
                .SetupProperty(x => x.Urn)
                .SetupProperty(x => x.Value);

            record.Object.Cacheability = "public";
            record.Object.Etag = "\"abc\"";
            record.Object.Expiration = "P1D";
            record.Object.Status = 200;
            record.Object.Urn = "urn://example.com/" + Guid.NewGuid();
            record.Object.Value = Activator.CreateInstance<T>();

            return record;
        }
    }
}