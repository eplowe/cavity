namespace Cavity.Tests
{
    using System.Globalization;

    using Cavity.Fluent;
    using Cavity.Properties;

    public sealed class AbstractBaseClassTest<T> : ITestExpectation
    {
        public bool Check()
        {
            if (!typeof(T).IsAbstract ||
                typeof(T).IsSealed)
            {
                throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Resources.AbstractBaseClassTestException_Message, typeof(T).Name));
            }

            return true;
        }
    }
}