namespace Cavity.Tests
{
    using System;
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
                    throw new UnitTestException(Resources.ImplementationTestException_UnexpectedMessage.FormatWith(typeof(T).Name));
                }
            }
            else
            {
                if (!typeof(T).Implements(Interface))
                {
                    throw new UnitTestException(Resources.ImplementationTestException_NoneMessage.FormatWith(typeof(T).Name, Interface.Name));
                }
            }

            return true;
        }
    }
}