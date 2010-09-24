namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using Cavity.Fluent;
    using Cavity.Properties;

    public sealed class DefaultConstructorTest<T> : ITestExpectation
    {
        public bool Check()
        {
            if (null == typeof(T).GetConstructor(Type.EmptyTypes))
            {
                throw new TestException(string.Format(
                    CultureInfo.CurrentUICulture,
                    Resources.DefaultConstructorTestException_Message,
                    typeof(T).Name));
            }

            return true;
        }
    }
}