namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Xml.Serialization;
    using Cavity.Properties;

    public sealed class XmlElementTest : MemberTest
    {
        public XmlElementTest(MemberInfo info)
            : base(info)
        {
        }

        public string Name
        {
            get;
            set;
        }

        public string Namespace
        {
            get;
            set;
        }

        public override bool Check()
        {
            XmlElementAttribute attribute = Attribute.GetCustomAttribute(this.Member, typeof(XmlElementAttribute), false) as XmlElementAttribute;
            string message = null;
            if (null == attribute)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlElementDecorationTestException_Message1,
                    this.Member.Name);
                throw new TestException(message);
            }
            else if (this.Name != attribute.ElementName)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlElementDecorationTestException_Message2,
                    this.Member.Name,
                    this.Name);
                throw new TestException(message);
            }
            else if (this.Namespace != attribute.Namespace)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlElementDecorationTestException_Message3,
                    this.Member.Name,
                    this.Name,
                    this.Namespace);
                throw new TestException(message);
            }

            return true;
        }
    }
}