namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositorySelectKey<T> : VerifyRepositoryBase<T>
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            var key = repository.Insert(Record.Object).Key;
            if (!Record.Object.Key.HasValue)
            {
                throw new InvalidOperationException();
            }

            var record = repository.Select(Record.Object.Key.Value);

            if (null == record)
            {
                throw new UnitTestException(Resources.Repository_ExpectResult_UnitTestExceptionMessage.FormatWith("Select", "record"));
            }

            if (key != record.Key)
            {
                throw new UnitTestException(Resources.Repository_ExpectCorrectRecordValue_UnitTestExceptionMessage.FormatWith("Select", "key"));
            }

            if (!Record.Object.Urn.Equals(record.Urn))
            {
                throw new UnitTestException(Resources.Repository_ExpectCorrectRecordValue_UnitTestExceptionMessage.FormatWith("Select", "URN"));
            }

            if (ReferenceEquals(Record.Object.Value, null))
            {
                if (ReferenceEquals(record.Value, null))
                {
                    return;
                }
                else
                {
                    throw new UnitTestException(Resources.Repository_ExpectCorrectRecordValue_UnitTestExceptionMessage.FormatWith("Select", "value"));
                }
            }

            if (!Record.Object.Value.Equals(record.Value))
            {
                throw new UnitTestException(Resources.Repository_ExpectCorrectRecordValue_UnitTestExceptionMessage.FormatWith("Select", "value"));
            }
        }
    }
}