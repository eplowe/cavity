namespace Cavity.Tests
{
    using Cavity.Fluent;
    using Cavity.Properties;

    public sealed class InterfaceTest<T> : ITestExpectation
    {
        public bool Check()
        {
            if (typeof(T).IsInterface)
            {
                return true;
            }

            throw new UnitTestException(Resources.InterfaceTestException_Message.FormatWith(typeof(T).Name));
        }
    }
}