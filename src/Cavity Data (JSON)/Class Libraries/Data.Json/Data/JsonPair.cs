namespace Cavity.Data
{
    using System;
    using System.Xml;

    public sealed class JsonPair
    {
        private string _name;

        private JsonValue _value;

        public JsonPair(string name,
                        bool value)
            : this(name)
        {
            if (value)
            {
                Value = new JsonTrue();
            }
            else
            {
                Value = new JsonFalse();
            }
        }

        public JsonPair(string name,
                        char value)
            : this(name)
        {
            if ((char)0 == value)
            {
                Value = new JsonNull();
            }
            else
            {
                Value = new JsonString(XmlConvert.ToString(value));
            }
        }

        public JsonPair(string name,
                        DateTime value)
            : this(name, XmlConvert.ToString(value, XmlDateTimeSerializationMode.Utc))
        {
        }

        public JsonPair(string name,
                        DateTimeOffset value)
            : this(name, XmlConvert.ToString(value))
        {
        }

        public JsonPair(string name,
                        decimal value)
            : this(name, new JsonNumber(value))
        {
        }

        public JsonPair(string name,
                        double value)
            : this(name, new JsonNumber(value))
        {
        }

        public JsonPair(string name,
                        long value)
            : this(name, new JsonNumber(value))
        {
        }

        public JsonPair(string name,
                        string value)
            : this(name, new JsonString(value))
        {
        }

        public JsonPair(string name,
                        JsonValue value)
            : this(name)
        {
            Value = value;
        }

        private JsonPair(string name)
            : this()
        {
            Name = name;
        }

        private JsonPair()
        {
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                _name = value;
            }
        }

        public JsonValue Value
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

        public override string ToString()
        {
            return "{0}: {1}".FormatWith(Name, Value);
        }
    }
}