namespace Cavity.Collections
{
    using System.Xml.Serialization;

    public abstract class PathItem
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }

        public override string ToString()
        {
#if NET20
            return StringExtensionMethods.FormatWith("{0}: {1}", Name, Value);
#else
            return "{0}: {1}".FormatWith(Name, Value);
#endif
        }
    }
}