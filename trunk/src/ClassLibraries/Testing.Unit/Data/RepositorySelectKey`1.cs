﻿namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositorySelectKey<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            Record1 = repository.Insert(Record1);
            if (!Record1.Key.HasValue)
            {
                throw new InvalidOperationException();
            }

            var record = repository.Select(Record1.Key.Value);

            if (null == record)
            {
                throw new UnitTestException(Resources.Repository_ExpectResult_UnitTestExceptionMessage.FormatWith("Select", "record"));
            }

            if (Record1.Key !=
                record.Key)
            {
                throw new UnitTestException(Resources.Repository_ExpectCorrectRecordValue_UnitTestExceptionMessage.FormatWith("Select", "key"));
            }

            if (!Record1.Urn.Equals(record.Urn))
            {
                throw new UnitTestException(Resources.Repository_ExpectCorrectRecordValue_UnitTestExceptionMessage.FormatWith("Select", "URN"));
            }

            if (ReferenceEquals(Record1.Value, null))
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

            if (!Record1.Value.Equals(record.Value))
            {
                throw new UnitTestException(Resources.Repository_ExpectCorrectRecordValue_UnitTestExceptionMessage.FormatWith("Select", "value"));
            }
        }
    }
}