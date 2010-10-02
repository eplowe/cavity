namespace Cavity.Tests
{
    using Cavity.Fluent;
    using Cavity.Properties;

    public sealed class ValueTypeTest<T> : ITestExpectation
    {
        public bool Check()
        {
            if (typeof(T).IsValueType)
            {
                return true;
            }

            throw new UnitTestException(Resources.ValueTypeTestException_Message.FormatWith(typeof(T).Name));
        }
    }
}