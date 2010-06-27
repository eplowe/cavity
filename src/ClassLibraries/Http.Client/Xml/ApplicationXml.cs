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
            this.ContentType = new ContentType("application/xml");
        }

        public ApplicationXml(IXPathNavigable xml)
            : this()
        {
            this.Xml = xml;
        }

        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Caching 'as' result prevents differentiating null from unexpected type.")]
        public object Content
        {
            get
            {
                return this.Xml;
            }

            set
            {
                if (null == value || value is IXPathNavigable)
                {
                    this.Xml = value as IXPathNavigable;
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
                return this._contentType;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this._contentType = value;
            }
        }

        public IXPathNavigable Xml
        {
            get;
            set;
        }

        public static implicit operator ApplicationXml(string value)
        {
            return object.ReferenceEquals(null, value) ? null as ApplicationXml : ApplicationXml.FromString(value);
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

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(value);

            return new ApplicationXml(xml);
        }

        public IContent ToContent(TextReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            return -1 == reader.Peek() ? new ApplicationXml() : ApplicationXml.FromString(reader.ReadToEnd());
        }

        public void Write(TextWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }

            writer.Write(this.ToString());
        }

        public override string ToString()
        {
            return null == this.Xml ? null as string : this.Xml.CreateNavigator().OuterXml;
        }
    }
}