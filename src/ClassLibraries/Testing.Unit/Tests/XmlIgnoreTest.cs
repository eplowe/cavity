namespace Cavity.Tests
{
    using System;
    using System.Reflection;
    using System.Xml.Serialization;
    using Cavity.Properties;

    public sealed class XmlIgnoreTest : MemberTestBase
    {
        public XmlIgnoreTest(MemberInfo info)
            : base(info)
        {
        }

        public override bool Check()
        {
            if (null == Attribute.GetCustomAttribute(Member, typeof(XmlIgnoreAttribute), false) as XmlIgnoreAttribute)
            {
                throw new UnitTestException(Resources.XmlIgnoreDecorationTestException_Message.FormatWith(Member.Name));
            }

            return true;
        }
    }
}