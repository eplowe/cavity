namespace Cavity.Tests
{
    using System.Globalization;
    using Cavity.Fluent;
    using Cavity.Properties;

    public class ConcreteClassTest<T> : ITestExpectation
    {
        public virtual bool Check()
        {
            if (typeof(T).IsAbstract)
            {
                string message = string.Format(
                    CultureInfo.CurrentUICulture,
                    Resources.ConcreteClassTestException_Message,
                    typeof(T).Name);
                throw new TestException(message);
            }

            return true;
        }
    }
}