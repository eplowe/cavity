namespace Cavity.Data
{
    using System;
    using System.Linq;
    using System.Xml.XPath;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryQueryExpression<T> : VerifyRepositoryBase<T> where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            repository.Insert(Record1);
            repository.Insert(Record2);

            var records = repository.Query(XPathExpression.Compile("/*"));

            if (null == records)
            {
                throw new UnitTestException(Resources.Repository_QueryReturnsNull_UnitTestExceptionMessage);
            }

            if (0 == records.Count())
            {
                throw new UnitTestException(Resources.Repository_QueryReturnsEmpty_UnitTestExceptionMessage);
            }
        }
    }
}