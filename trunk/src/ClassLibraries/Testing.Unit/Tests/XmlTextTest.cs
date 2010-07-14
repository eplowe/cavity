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
            var attribute = Attribute.GetCustomAttribute(Member, typeof(XmlTextAttribute), false) as XmlTextAttribute;

            if (null == attribute)
            {
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlTextDecorationTestException_Message,
                    Member.Name));
            }

            return true;
        }
    }
}