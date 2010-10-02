namespace Cavity.Tests
{
    using Cavity.Fluent;
    using Cavity.Properties;

    public sealed class SealedClassTest<T> : ITestExpectation
    {
        public SealedClassTest(bool value)
        {
            Value = value;
        }

        public bool Value { get; set; }

        public bool Check()
        {
            if (Value == typeof(T).IsSealed)
            {
                return true;
            }

            if (Value)
            {
                throw new UnitTestException(Resources.SealedClassTestException_UnsealedMessage.FormatWith(typeof(T).Name));
            }
            else
            {
                throw new UnitTestException(Resources.SealedClassTestException_SealedMessage.FormatWith(typeof(T).Name));
            }
        }
    }
}