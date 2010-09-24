namespace Cavity.Tests
{
    using System.Globalization;
    using Cavity.Fluent;
    using Cavity.Properties;

    public sealed class ValueTypeTest<T> : ITestExpectation
    {
        public bool Check()
        {
            if (!typeof(T).IsValueType)
            {
                throw new TestException(string.Format(
                    CultureInfo.CurrentUICulture,
                    Resources.ValueTypeTestException_Message,
                    typeof(T).Name));
            }

            return true;
        }
    }
}