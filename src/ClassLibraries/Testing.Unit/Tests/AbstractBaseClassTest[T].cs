namespace Cavity.Tests
{
    using System.Globalization;
    using Cavity.Fluent;
    using Cavity.Properties;

    public class AbstractBaseClassTest<T> : ITestExpectation
    {
        public bool Check()
        {
            if (!typeof(T).IsAbstract || typeof(T).IsSealed)
            {
                string message = string.Format(
                    CultureInfo.CurrentUICulture,
                    Resources.AbstractBaseClassTestException_Message,
                    typeof(T).Name);
                throw new TestException(message);
            }

            return true;
        }
    }
}