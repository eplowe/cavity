namespace Cavity.Collections.Generic
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot("item")]
    public sealed class TestXmlSerializableCollectionItem : IEquatable<TestXmlSerializableCollectionItem>
    {
        public TestXmlSerializableCollectionItem()
        {
        }

        public TestXmlSerializableCollectionItem(int value)
        {
            Value = value;
        }

        [XmlAttribute("value")]
        public int Value { get; set; }

        public bool Equals(TestXmlSerializableCollectionItem other)
        {
            return null != other && Value == other.Value;
        }
    }
}