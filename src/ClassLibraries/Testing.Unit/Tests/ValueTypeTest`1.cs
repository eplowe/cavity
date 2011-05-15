namespace Cavity.Tests
{
    using System.Globalization;
    using Cavity.Fluent;
    using Cavity.Properties;

    public sealed class ValueTypeTest<T> : ITestExpectation
    {
        public bool Check()
        {
            if (typeof(T).IsValueType)
            {
                return true;
            }

            throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Resources.ValueTypeTestException_Message, typeof(T).Name));
        }
    }
}