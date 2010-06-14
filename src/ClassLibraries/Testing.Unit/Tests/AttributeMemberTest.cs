namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using Cavity.Properties;

    public class AttributeMemberTest : MemberTest
    {
        public AttributeMemberTest(MemberInfo member, Type attribute)
            : base(member)
        {
            this.Attribute = attribute;
        }

        public Type Attribute
        {
            get;
            set;
        }

        public override bool Check()
        {
            string message = null;
            if (null == this.Attribute)
            {
                if (0 != this.Member.GetCustomAttributes(false).Length)
                {
                    message = string.Format(
                        CultureInfo.CurrentUICulture,
                        Resources.DecorationTestException_UnexpectedMessage,
                        this.Member.Name);
                    throw new TestException(message);
                }
            }
            else
            {
                if (null == System.Attribute.GetCustomAttribute(this.Member, this.Attribute, false))
                {
                    message = string.Format(
                        CultureInfo.InvariantCulture,
                        Resources.DecorationTestException_MissingMessage,
                        this.Member.Name,
                        this.Attribute.Name);
                    throw new TestException(message);
                }
            }

            return true;
        }
    }
}