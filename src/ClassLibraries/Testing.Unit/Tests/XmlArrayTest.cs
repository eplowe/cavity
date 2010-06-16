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
            if (null == attribute)
            {
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlArrayDecorationTestException_Message1,
                    this.Member.Name));
            }
            else if (this.ArrayElementName != attribute.ElementName)
            {
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlArrayDecorationTestException_Message2,
                    this.Member.Name,
                    this.ArrayElementName));
            }
        }

        private void CheckArrayItems()
        {
            XmlArrayItemAttribute attribute = Attribute.GetCustomAttribute(this.Member, typeof(XmlArrayItemAttribute), false) as XmlArrayItemAttribute;
            if (null == attribute)
            {
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlArrayDecorationTestException_Message3,
                    this.Member.Name));
            }
            else if (this.ArrayItemElementName != attribute.ElementName)
            {
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlArrayDecorationTestException_Message4,
                    this.Member.Name,
                    this.ArrayItemElementName));
            }
        }
    }
}