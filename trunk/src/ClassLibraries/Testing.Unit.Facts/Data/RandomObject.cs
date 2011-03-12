namespace Cavity.Data
{
    using System.Xml.Serialization;

    [XmlRoot("random")]
    public sealed class RandomObject : ComparableObject
    {
        public RandomObject()
        {
            Value = AlphaDecimal.Random();
        }

        [XmlAttribute("value")]
        public AlphaDecimal Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}