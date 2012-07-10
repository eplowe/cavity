namespace Cavity.Data
{
    using System;

    public sealed class JsonPair
    {
        private string _name;

        private JsonValue _value;

        public JsonPair(string name, 
                        string value)
            : this(name, new JsonString(value))
        {
        }

        public JsonPair(string name, 
                        JsonValue value)
            : this()
        {
            Name = name;
            Value = value;
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