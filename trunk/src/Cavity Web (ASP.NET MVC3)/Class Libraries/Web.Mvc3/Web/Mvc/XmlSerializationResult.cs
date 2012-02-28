namespace Cavity.Web.Mvc
{
    using System;
    using System.Net.Mime;
    using System.Text;
    using System.Web.Mvc;
    using System.Xml;

    public sealed class XmlSerializationResult : ContentResult
    {
        public XmlSerializationResult(object model)
            : this(model, "application/xml")
        {
        }

        public XmlSerializationResult(object model, 
                                      string contentType)
        {
            if (null == model)
            {
                throw new ArgumentNullException("model");
            }

            if (null == contentType)
            {
                throw new ArgumentNullException("contentType");
            }

            if (0 == contentType.Length)
            {
                throw new ArgumentOutOfRangeException("contentType");
            }

            ContentEncoding = Encoding.UTF8;
            ContentType = new ContentType(contentType).MediaType;

            var xml = model.XmlSerialize() as XmlDocument;
            if (null == xml)
            {
                return;
            }

            xml.CreateXmlDeclaration("1.0", "utf-8", null);
            xml.PreserveWhitespace = false;

            Content = xml.OuterXml;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (null == context)
            {
                throw new ArgumentNullException("context");
            }

            base.ExecuteResult(context);
        }
    }
}