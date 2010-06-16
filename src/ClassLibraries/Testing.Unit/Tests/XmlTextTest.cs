namespace Cavity.Tests
{
    using System;
    using System.Globalization;
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
            XmlTextAttribute attribute = Attribute.GetCustomAttribute(this.Member, typeof(XmlTextAttribute), false) as XmlTextAttribute;

            if (null == attribute)
            {
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlTextDecorationTestException_Message,
                    this.Member.Name));
            }

            return true;
        }
    }
}