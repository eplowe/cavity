namespace Cavity.Tests
{
    using System.Globalization;
    using Cavity.Fluent;
    using Cavity.Properties;

    public class SealedClassTest<T> : ITestExpectation
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

        public virtual bool Check()
        {
            if (this.Value != typeof(T).IsSealed)
            {
                string message = null;
                if (this.Value)
                {
                    message = string.Format(CultureInfo.CurrentUICulture, Resources.SealedClassTestException_UnsealedMessage, typeof(T).Name);
                }
                else
                {
                    message = string.Format(CultureInfo.CurrentUICulture, Resources.SealedClassTestException_SealedMessage, typeof(T).Name);
                }

                throw new TestException(message);
            }

            return true;
        }
    }
}