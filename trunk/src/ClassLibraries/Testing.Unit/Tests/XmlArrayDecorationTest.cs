namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Xml.Serialization;
    using Cavity.Properties;

    public sealed class XmlArrayDecorationTest : AttributePropertyTest
    {
        public XmlArrayDecorationTest(MemberInfo info)
            : base(info)
        {
        }

        public string Name
        {
            get;
            set;
        }

        public string Items
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
            XmlArrayAttribute attribute = Attribute.GetCustomAttribute(this.MemberInfo, typeof(XmlArrayAttribute), false) as XmlArrayAttribute;
            string message = null;
            if (null == attribute)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlArrayDecorationTestException_Message1,
                    this.MemberInfo.Name);
                throw new TestException(message);
            }
            else if (this.Name != attribute.ElementName)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlArrayDecorationTestException_Message2,
                    this.MemberInfo.Name,
                    this.Name);
                throw new TestException(message);
            }
        }

        private void CheckArrayItems()
        {
            XmlArrayItemAttribute attribute = Attribute.GetCustomAttribute(this.MemberInfo, typeof(XmlArrayItemAttribute), false) as XmlArrayItemAttribute;
            string message = null;
            if (null == attribute)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlArrayDecorationTestException_Message3,
                    this.MemberInfo.Name);
                throw new TestException(message);
            }
            else if (this.Items != attribute.ElementName)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlArrayDecorationTestException_Message4,
                    this.MemberInfo.Name,
                    this.Items);
                throw new TestException(message);
            }
        }
    }
}