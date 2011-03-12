namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryUpsertUpdateRecord<T> : VerifyRepositoryBase<T> where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            var record = repository.Insert(Record.Object);
            if (record.Key ==
                repository.Upsert(record).Key)
            {
                return;
            }

            throw new UnitTestException(Resources.Repository_ExpectTrueWhenExistingRecord_UnitTestExceptionMessage.FormatWith("Upsert", "updated"));
        }
    }
}