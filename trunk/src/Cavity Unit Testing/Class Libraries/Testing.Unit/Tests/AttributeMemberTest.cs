namespace Cavity.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
#if !NET20
    using System.Linq;
#endif
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
                if (0 != Member.GetCustomAttributes(false).Count(x => !(x is SuppressMessageAttribute)))
                {
                    throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Resources.DecorationTestException_UnexpectedMessage, Member.Name));
                }
            }
            else
            {
                if (null == System.Attribute.GetCustomAttribute(Member, Attribute, false))
                {
                    throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Resources.DecorationTestException_MissingMessage, Member.Name, Attribute.Name));
                }
            }

            return true;
        }
    }
}