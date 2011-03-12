namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryInsertRecord<T> : VerifyRepositoryBase<T> where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            var record = repository.Insert(Record1);
            if (!record.Key.HasValue)
            {
                throw new UnitTestException(Resources.Repository_Insert_ReturnsIncorrectKey_UnitTestExceptionMessage);
            }

            if (!record.Created.HasValue)
            {
                throw new UnitTestException(Resources.Repository_Insert_ReturnsWithoutCreated_UnitTestExceptionMessage);
            }

            if (!record.Modified.HasValue)
            {
                throw new UnitTestException(Resources.Repository_Insert_ReturnsWithoutModified_UnitTestExceptionMessage);
            }
        }
    }
}