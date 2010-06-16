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
            this.Interface = @interface;
        }

        public Type Interface
        {
            get;
            set;
        }

        public bool Check()
        {
            if (null == this.Interface)
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
                if (!typeof(T).Implements(this.Interface))
                {
                    throw new TestException(string.Format(
                        CultureInfo.CurrentUICulture,
                        Resources.ImplementationTestException_NoneMessage,
                        typeof(T).Name,
                        this.Interface.Name));
                }
            }

            return true;
        }
    }
}