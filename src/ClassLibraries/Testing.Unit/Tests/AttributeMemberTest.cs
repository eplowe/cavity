namespace Cavity.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using Cavity.Properties;

    public sealed class AttributeMemberTest : MemberTestBase
    {
        public AttributeMemberTest(MemberInfo member,
                                   Type attribute)
            : base(member)
        {
            Attribute = attribute;
        }

        public Type Attribute { get; set; }

        public override bool Check()
        {
            if (null == Attribute)
            {
                if (0 != Member.GetCustomAttributes(false).Where(x => !(x is SuppressMessageAttribute)).Count())
                {
                    throw new UnitTestException(Resources.DecorationTestException_UnexpectedMessage.FormatWith(Member.Name));
                }
            }
            else
            {
                if (null == System.Attribute.GetCustomAttribute(Member, Attribute, false))
                {
                    throw new UnitTestException(Resources.DecorationTestException_MissingMessage.FormatWith(Member.Name, Attribute.Name));
                }
            }

            return true;
        }
    }
}