namespace Cavity.Tests
{
    using System;
    using System.Globalization;
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
            var attribute = Attribute.GetCustomAttribute(Member, typeof(XmlIgnoreAttribute), false) as XmlIgnoreAttribute;

            if (null == attribute)
            {
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlIgnoreDecorationTestException_Message,
                    Member.Name));
            }

            return true;
        }
    }
}