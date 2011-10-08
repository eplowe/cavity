namespace Cavity.Tests
{
    using System.Globalization;
    using Cavity.Fluent;
    using Cavity.Properties;

    public sealed class ConcreteClassTest<T> : ITestExpectation
    {
        public bool Check()
        {
            if (typeof(T).IsAbstract)
            {
                throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Resources.ConcreteClassTestException_Message, typeof(T).Name));
            }

            return true;
        }
    }
}