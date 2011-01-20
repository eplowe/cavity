namespace Cavity.Xml
{
    using System;

    public sealed class XmlNamespace : ComparableObject
    {
        private string _prefix;

        private AbsoluteUri _uri;

        public XmlNamespace(string prefix,
                            AbsoluteUri uri)
        {
            Prefix = prefix;
            Uri = uri;
        }

        private XmlNamespace()
        {
        }

        public string Prefix
        {
            get
            {
                return _prefix;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                if (0 == value.Length)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                _prefix = value;
            }
        }

        public AbsoluteUri Uri
        {
            get
            {
                return _uri;
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                _uri = value;
            }
        }

        public override string ToString()
        {
            return "xmlns:{0}=\"{1}\"".FormatWith(Prefix, Uri);
        }
    }
}