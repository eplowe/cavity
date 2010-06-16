namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Xml.Serialization;
    using Cavity.Properties;

    public sealed class XmlArrayTest : MemberTestBase
    {
        public XmlArrayTest(MemberInfo info)
            : base(info)
        {
        }

        public string ArrayElementName
        {
            get;
            set;
        }

        public string ArrayItemElementName
        {
            get;
            set;
        }

        public override bool Check()
        {
            this.CheckArray();
            this.CheckArrayItems();

            return true;
        }

        private void CheckArray()
        {
            XmlArrayAttribute attribute = Attribute.GetCustomAttribute(this.Member, typeof(XmlArrayAttribute), false) as XmlArrayAttribute;
            string message = null;
            if (null == attribute)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlArrayDecorationTestException_Message1,
                    this.Member.Name);
                throw new TestException(message);
            }
            else if (this.ArrayElementName != attribute.ElementName)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlArrayDecorationTestException_Message2,
                    this.Member.Name,
                    this.ArrayElementName);
                throw new TestException(message);
            }
        }

        private void CheckArrayItems()
        {
            XmlArrayItemAttribute attribute = Attribute.GetCustomAttribute(this.Member, typeof(XmlArrayItemAttribute), false) as XmlArrayItemAttribute;
            string message = null;
            if (null == attribute)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlArrayDecorationTestException_Message3,
                    this.Member.Name);
                throw new TestException(message);
            }
            else if (this.ArrayItemElementName != attribute.ElementName)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlArrayDecorationTestException_Message4,
                    this.Member.Name,
                    this.ArrayItemElementName);
                throw new TestException(message);
            }
        }
    }
}