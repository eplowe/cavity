namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using Cavity.Fluent;
    using Cavity.Properties;

    public class DecorationTest : ITestExpectation
    {
        private MemberInfo _info;

        public DecorationTest(MemberInfo info, Type attribute)
        {
            this.MemberInfo = info;
            this.Attribute = attribute;
        }

        public Type Attribute
        {
            get;
            set;
        }

        public MemberInfo MemberInfo
        {
            get
            {
                return this._info;
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this._info = value;
            }
        }

        public bool Check()
        {
            string message = null;
            if (null == this.Attribute)
            {
                if (0 != this.MemberInfo.GetCustomAttributes(false).Length)
                {
                    message = string.Format(CultureInfo.CurrentUICulture, Resources.DecorationTestException_UnexpectedMessage, this.MemberInfo.Name);
                    throw new TestException(message);
                }
            }
            else
            {
                if (null == System.Attribute.GetCustomAttribute(this.MemberInfo, this.Attribute, false))
                {
                    message = string.Format(CultureInfo.InvariantCulture, Resources.DecorationTestException_MissingMessage, this.MemberInfo.Name, this.Attribute.Name);
                    throw new TestException(message);
                }
            }

            return true;
        }
    }
}