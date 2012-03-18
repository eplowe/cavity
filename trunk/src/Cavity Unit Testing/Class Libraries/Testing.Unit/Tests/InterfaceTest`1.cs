namespace Cavity.Tests
{
    using System.Globalization;

    using Cavity.Fluent;
    using Cavity.Properties;

    public sealed class InterfaceTest<T> : ITestExpectation
    {
        public bool Check()
        {
            if (typeof(T).IsInterface)
            {
                return true;
            }

            throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Resources.InterfaceTestException_Message, typeof(T).Name));
        }
    }
}