namespace Cavity.Tests
{
    using System.Globalization;
    using Cavity.Fluent;
    using Cavity.Properties;

    public class ValueTypeTest<T> : ITestExpectation
    {
        public bool Check()
        {
            if (!typeof(T).IsValueType)
            {
                string message = string.Format(
                    CultureInfo.CurrentUICulture,
                    Resources.ValueTypeTestException_Message,
                    typeof(T).Name);
                throw new TestException(message);
            }

            return true;
        }
    }
}