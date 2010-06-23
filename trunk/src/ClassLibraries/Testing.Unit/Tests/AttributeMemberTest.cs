namespace Cavity.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using Cavity.Properties;

    public sealed class AttributeMemberTest : MemberTestBase
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
            if (null == this.Attribute)
            {
                if (0 != this.Member.GetCustomAttributes(false).Where(x => !(x is SuppressMessageAttribute)).Count())
                {
                    throw new TestException(string.Format(
                        CultureInfo.CurrentUICulture,
                        Resources.DecorationTestException_UnexpectedMessage,
                        this.Member.Name));
                }
            }
            else
            {
                if (null == System.Attribute.GetCustomAttribute(this.Member, this.Attribute, false))
                {
                    throw new TestException(string.Format(
                        CultureInfo.InvariantCulture,
                        Resources.DecorationTestException_MissingMessage,
                        this.Member.Name,
                        this.Attribute.Name));
                }
            }

            return true;
        }
    }
}