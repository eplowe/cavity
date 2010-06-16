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
            XmlIgnoreAttribute attribute = Attribute.GetCustomAttribute(this.Member, typeof(XmlIgnoreAttribute), false) as XmlIgnoreAttribute;

            if (null == attribute)
            {
                string message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlIgnoreDecorationTestException_Message,
                    this.Member.Name);
                throw new TestException(message);
            }

            return true;
        }
    }
}