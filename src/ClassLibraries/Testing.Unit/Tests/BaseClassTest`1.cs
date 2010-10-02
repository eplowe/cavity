namespace Cavity.Tests
{
    using System;
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
            if (typeof(T).IsSubclassOf(Is))
            {
                return true;
            }

            throw new UnitTestException(Resources.BaseClassTestException_Message.FormatWith(typeof(T).Name, Is.Name));
        }
    }
}