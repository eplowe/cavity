namespace Cavity.Tests
{
    using Cavity.Fluent;
    using Cavity.Properties;

    public sealed class ConcreteClassTest<T> : ITestExpectation
    {
        public bool Check()
        {
            if (typeof(T).IsAbstract)
            {
                throw new UnitTestException(Resources.ConcreteClassTestException_Message.FormatWith(typeof(T).Name));
            }

            return true;
        }
    }
}