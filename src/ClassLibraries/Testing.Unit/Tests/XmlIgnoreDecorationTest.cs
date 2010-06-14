namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Xml.Serialization;
    using Cavity.Properties;

    public sealed class XmlIgnoreDecorationTest : AttributePropertyTest
    {
        public XmlIgnoreDecorationTest(MemberInfo info)
            : base(info)
        {
        }

        public override bool Check()
        {
            XmlIgnoreAttribute attribute = Attribute.GetCustomAttribute(this.MemberInfo, typeof(XmlIgnoreAttribute), false) as XmlIgnoreAttribute;

            if (null == attribute)
            {
                string message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlIgnoreDecorationTestException_Message,
                    this.MemberInfo.Name);
                throw new TestException(message);
            }

            return true;
        }
    }
}