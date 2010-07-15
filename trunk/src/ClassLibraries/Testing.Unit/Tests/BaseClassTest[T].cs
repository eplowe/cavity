namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using Cavity.Fluent;
    using Cavity.Properties;

    public sealed class BaseClassTest<T> : ITestExpectation
    {
        public BaseClassTest(Type @is)
        {
            Is = @is;
        }

        public Type Is { get; set; }

        public bool Check()
        {
            if (!typeof(T).IsSubclassOf(Is))
            {
                throw new TestException(string.Format(
                    CultureInfo.CurrentUICulture,
                    Resources.BaseClassTestException_Message,
                    typeof(T).Name,
                    Is.Name));
            }

            return true;
        }
    }
}