namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using Cavity.Fluent;
    using Cavity.Properties;

    public sealed class ImplementationTest<T> : ITestExpectation
    {
        public ImplementationTest(Type @interface)
        {
            Interface = @interface;
        }

        public Type Interface { get; set; }

        public bool Check()
        {
            if (null == Interface)
            {
                if (0 != typeof(T).GetInterfaces().Length)
                {
                    throw new TestException(string.Format(
                        CultureInfo.CurrentUICulture,
                        Resources.ImplementationTestException_UnexpectedMessage,
                        typeof(T).Name));
                }
            }
            else
            {
                if (!typeof(T).Implements(Interface))
                {
                    throw new TestException(string.Format(
                        CultureInfo.CurrentUICulture,
                        Resources.ImplementationTestException_NoneMessage,
                        typeof(T).Name,
                        Interface.Name));
                }
            }

            return true;
        }
    }
}