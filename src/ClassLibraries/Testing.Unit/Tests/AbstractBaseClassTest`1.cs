namespace Cavity.Tests
{
    using Cavity.Fluent;
    using Cavity.Properties;

    public sealed class AbstractBaseClassTest<T> : ITestExpectation
    {
        public bool Check()
        {
            if (!typeof(T).IsAbstract ||
                typeof(T).IsSealed)
            {
                throw new UnitTestException(Resources.AbstractBaseClassTestException_Message.FormatWith(typeof(T).Name));
            }

            return true;
        }
    }
}