namespace Cavity.Tests
{
    using System.Globalization;
    using Cavity.Fluent;
    using Cavity.Properties;

    public class InterfaceTest<T> : ITestExpectation
    {
        public bool Check()
        {
            if (!typeof(T).IsInterface)
            {
                string message = string.Format(
                    CultureInfo.CurrentUICulture,
                    Resources.InterfaceTestException_Message,
                    typeof(T).Name);
                throw new TestException(message);
            }

            return true;
        }
    }
}