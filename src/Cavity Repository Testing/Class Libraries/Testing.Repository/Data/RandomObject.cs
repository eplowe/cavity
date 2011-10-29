namespace Cavity.Data
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;

    [XmlRoot("random")]
    public sealed class RandomObject : ComparableObject
    {
        public RandomObject()
        {
            Value = DateTime.UtcNow.Ticks;
        }

        [XmlAttribute("value")]
        public long Value { get; set; }

        public override string ToString()
        {
            return XmlConvert.ToString(Value);
        }
    }
}