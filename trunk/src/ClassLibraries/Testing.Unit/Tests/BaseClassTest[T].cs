﻿namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using Cavity.Fluent;
    using Cavity.Properties;

    public sealed class BaseClassTest<T> : ITestExpectation
    {
        public BaseClassTest(Type @is)
        {
            this.Is = @is;
        }

        public Type Is
        {
            get;
            set;
        }

        public bool Check()
        {
            if (!typeof(T).IsSubclassOf(this.Is))
            {
                throw new TestException(string.Format(
                    CultureInfo.CurrentUICulture,
                    Resources.BaseClassTestException_Message,
                    typeof(T).Name,
                    this.Is.Name));
            }

            return true;
        }
    }
}