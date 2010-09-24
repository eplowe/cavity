namespace Cavity.Tests
{
    using System.Globalization;
    using Cavity.Fluent;
    using Cavity.Properties;

    public sealed class InterfaceTest<T> : ITestExpectation
    {
        public bool Check()
        {
            if (!typeof(T).IsInterface)
            {
                throw new TestException(string.Format(
                    CultureInfo.CurrentUICulture,
                    Resources.InterfaceTestException_Message,
                    typeof(T).Name));
            }

            return true;
        }
    }
}