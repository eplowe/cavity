namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositorySelectUrn<T> : VerifyRepositoryBase<T>
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

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

            if (!Record.Object.Urn.Equals(record.Urn))
            {
                throw new UnitTestException(Resources.Repository_Select_ReturnsIncorrectUrn_UnitTestExceptionMessage);
            }

            if (ReferenceEquals(Record.Object.Value, null))
            {
                if (ReferenceEquals(record.Value, null))
                {
                    return;
                }
                else
                {
                    throw new UnitTestException(Resources.Repository_Select_ReturnsIncorrectValue_UnitTestExceptionMessage);
                }
            }

            if (!Record.Object.Value.Equals(record.Value))
            {
                throw new UnitTestException(Resources.Repository_Select_ReturnsIncorrectValue_UnitTestExceptionMessage);
            }
        }
    }
}