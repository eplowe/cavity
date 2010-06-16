namespace Cavity.Tests
{
    using System.Globalization;
    using Cavity.Fluent;
    using Cavity.Properties;

    public sealed class SealedClassTest<T> : ITestExpectation
    {
        public SealedClassTest(bool value)
        {
            this.Value = value;
        }

        public bool Value
        {
            get;
            set;
        }

        public bool Check()
        {
            if (this.Value != typeof(T).IsSealed)
            {
                if (this.Value)
                {
                    throw new TestException(string.Format(
                        CultureInfo.CurrentUICulture,
                        Resources.SealedClassTestException_UnsealedMessage,
                        typeof(T).Name));
                }
                else
                {
                    throw new TestException(string.Format(
                        CultureInfo.CurrentUICulture,
                        Resources.SealedClassTestException_SealedMessage,
                        typeof(T).Name));
                }
            }

            return true;
        }
    }
}