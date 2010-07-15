namespace Cavity.Xml
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Net.Mime;
    using System.Xml;
    using System.Xml.XPath;
    using Cavity.Net.Mime;

    public sealed class ApplicationXml : ComparableObject, IContent, IMediaType
    {
        private ContentType _contentType;

        public ApplicationXml()
        {
            ContentType = new ContentType("application/xml");
        }

        public ApplicationXml(IXPathNavigable xml)
            : this()
        {
            Xml = xml;
        }

        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Caching 'as' result prevents differentiating null from unexpected type.")]
        public object Content
        {
            get
            {
                return Xml;
            }

            set
            {
                if (null == value ||
                    value is IXPathNavigable)
                {
                    Xml = value as IXPathNavigable;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("value");
                }
            }
        }

        public ContentType ContentType
        {
            get
            {
                return _contentType;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                _contentType = value;
            }
        }

        public IXPathNavigable Xml { get; set; }

        public static implicit operator ApplicationXml(string value)
        {
            return ReferenceEquals(null, value)
                       ? null
                       : FromString(value);
        }

        public static ApplicationXml FromString(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }
            else if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            var xml = new XmlDocument();
            xml.LoadXml(value);

            return new ApplicationXml(xml);
        }

        public override string ToString()
        {
            return null == Xml
                       ? null
                       : Xml.CreateNavigator().OuterXml;
        }

        public void Write(TextWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }

            writer.Write(ToString());
        }

        public IContent ToContent(TextReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            return -1 == reader.Peek()
                       ? new ApplicationXml()
                       : FromString(reader.ReadToEnd());
        }
    }
}