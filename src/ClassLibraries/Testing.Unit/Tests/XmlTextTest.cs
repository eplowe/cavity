namespace Cavity.Tests
{
    using System;
    using System.Reflection;
    using System.Xml.Serialization;
    using Cavity.Properties;

    public sealed class XmlTextTest : MemberTestBase
    {
        public XmlTextTest(MemberInfo info)
            : base(info)
        {
        }

        public override bool Check()
        {
            if (null == Attribute.GetCustomAttribute(Member, typeof(XmlTextAttribute), false) as XmlTextAttribute)
            {
                throw new UnitTestException(Resources.XmlTextDecorationTestException_Message.FormatWith(Member.Name));
            }

            return true;
        }
    }
}