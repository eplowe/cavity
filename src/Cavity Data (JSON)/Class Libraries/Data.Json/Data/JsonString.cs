namespace Cavity.Data
{
    using System;
    using System.Globalization;
    using System.Xml;

    public class JsonString : JsonValue
    {
        private static readonly DateTime _unix = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        private string _value;

        public JsonString(DateTime value)
            : this(value.ToXmlString())
        {
        }

        public JsonString(string value)
            : this()
        {
            Value = value;
        }

        private JsonString()
        {
        }

        public string Value
        {
            get
            {
                return _value;
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                _value = value;
            }
        }

        public virtual DateTime ToDateTime()
        {
            if (Value.StartsWith("\\/Date(", StringComparison.Ordinal) &&
                Value.EndsWith(")\\/", StringComparison.Ordinal))
            {
                var seconds = Value
                    .RemoveFromStart("\\/Date(", StringComparison.Ordinal)
                    .RemoveFromEnd(")\\/", StringComparison.Ordinal);
                return _unix.AddMilliseconds(XmlConvert.ToDouble(seconds));
            }

            return DateTime.Parse(Value, CultureInfo.InvariantCulture).ToUniversalTime();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}