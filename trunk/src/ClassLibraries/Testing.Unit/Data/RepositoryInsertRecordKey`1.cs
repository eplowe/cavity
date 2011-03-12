namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Cavity.Properties;
    using Cavity.Tests;
    using Moq;

    public sealed class RepositoryInsertRecordKey<T> : VerifyRepositoryBase<T>
    {
        public RepositoryInsertRecordKey()
        {
            Record.Object.Key = AlphaDecimal.Random();
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is required for mocking.")]
        public Mock<IRecord<T>> Duplicate { get; set; }

        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            RepositoryException expected = null;
            try
            {
                repository.Insert(Record.Object);
            }
            catch (RepositoryException exception)
            {
                expected = exception;
            }
            catch (Exception exception)
            {
                throw new UnitTestException(Resources.Repository_Insert_RecordKeyExists_UnitTestExceptionMessage, exception);
            }

            if (null == expected)
            {
                throw new UnitTestException(Resources.Repository_Insert_RecordKeyExists_UnitTestExceptionMessage);
            }
        }
    }
}