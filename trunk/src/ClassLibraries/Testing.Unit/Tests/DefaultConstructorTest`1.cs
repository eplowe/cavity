namespace Cavity.Tests
{
    using System;
    using Cavity.Fluent;
    using Cavity.Properties;

    public sealed class DefaultConstructorTest<T> : ITestExpectation
    {
        public bool Check()
        {
            if (null == typeof(T).GetConstructor(Type.EmptyTypes))
            {
                throw new UnitTestException(Resources.DefaultConstructorTestException_Message.FormatWith(typeof(T).Name));
            }

            return true;
        }
    }
}