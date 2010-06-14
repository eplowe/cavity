namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using Cavity.Fluent;
    using Cavity.Properties;

    public class ImplementationTest<T> : ITestExpectation
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
            string message = null;
            if (null == this.Interface)
            {
                if (0 != typeof(T).GetInterfaces().Length)
                {
                    message = string.Format(
                        CultureInfo.CurrentUICulture,
                        Resources.ImplementationTestException_UnexpectedMessage,
                        typeof(T).Name);
                    throw new TestException(message);
                }
            }
            else
            {
                if (!typeof(T).Implements(this.Interface))
                {
                    message = string.Format(
                        CultureInfo.CurrentUICulture,
                        Resources.ImplementationTestException_NoneMessage,
                        typeof(T).Name,
                        this.Interface.Name);
                    throw new TestException(message);
                }
            }

            return true;
        }
    }
}