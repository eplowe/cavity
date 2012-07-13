namespace Cavity.Data
{
    using System;
    using System.Xml;

    public class JsonNumber : JsonValue
    {
        private string _value;

        public JsonNumber(decimal value)
            : this(XmlConvert.ToString(value))
        {
        }

        public JsonNumber(double value)
            : this(XmlConvert.ToString(value))
        {
        }

        public JsonNumber(long value)
            : this(XmlConvert.ToString(value))
        {
        }

        public JsonNumber(string value)
            : this()
        {
            Value = value;
        }

        private JsonNumber()
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

        public virtual decimal ToDecimal()
        {
            return XmlConvert.ToDecimal(Value);
        }

        public virtual double ToDouble()
        {
            return XmlConvert.ToDouble(Value);
        }

        public virtual short ToInt16()
        {
            return XmlConvert.ToInt16(Value);
        }

        public virtual int ToInt32()
        {
            return XmlConvert.ToInt32(Value);
        }

        public virtual long ToInt64()
        {
            return XmlConvert.ToInt64(Value);
        }

        public virtual float ToSingle()
        {
            return XmlConvert.ToSingle(Value);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}