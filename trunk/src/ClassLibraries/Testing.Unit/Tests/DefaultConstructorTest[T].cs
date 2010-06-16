namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using Cavity.Fluent;
    using Cavity.Properties;

    public class DefaultConstructorTest<T> : ITestExpectation
    {
        public virtual bool Check()
        {
            if (null == typeof(T).GetConstructor(Type.EmptyTypes))
            {
                string message = string.Format(
                    CultureInfo.CurrentUICulture,
                    Resources.DefaultConstructorTestException_Message,
                    typeof(T).Name);
                throw new TestException(message);
            }

            return true;
        }
    }
}