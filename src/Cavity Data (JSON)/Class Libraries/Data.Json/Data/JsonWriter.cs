namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class JsonWriter : DisposableObject
    {
        private StreamWriter _writer;

        public JsonWriter(Stream stream)
            : this()
        {
            if (null == stream)
            {
                throw new ArgumentNullException("stream");
            }

            _writer = new StreamWriter(stream);
        }

        private JsonWriter()
        {
            Nesting = new Stack<JsonWriterState>();
            Nesting.Push(new JsonWriterState(JsonNodeType.None));
        }

        private Stack<JsonWriterState> Nesting { get; set; }

        public void Array()
        {
            if (JsonNodeType.None != Nesting.Peek().Parent)
            {
                throw new InvalidOperationException("A nameless array is only permitted as a root container.");
            }

            _writer.Write('[');

            Nesting.Push(new JsonWriterState(JsonNodeType.Array));
        }

        public void Array(string name)
        {
            if (JsonNodeType.None == Nesting.Peek().Parent)
            {
                throw new InvalidOperationException("A named array is not permitted as a root container.");
            }

            if (null == name)
            {
                throw new ArgumentNullException("name");
            }

            _writer.Write("\"{0}\": [".FormatWith(name));
            Nesting.Push(new JsonWriterState(JsonNodeType.Array));
        }

        public void ArrayNull()
        {
            Array("null", JsonNodeType.NullValue);
        }

        public void ArrayNumber(string value)
        {
            if (null == value)
            {
                ArrayNull();
                return;
            }

            value = value.Trim();
            if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            Array(value, JsonNodeType.NumberValue);
        }

        public void ArrayValue(bool value)
        {
            Array(XmlConvert.ToString(value), value ? JsonNodeType.TrueValue : JsonNodeType.FalseValue);
        }

        public void ArrayValue(decimal value)
        {
            ArrayNumber(XmlConvert.ToString(value));
        }

        public void ArrayValue(double value)
        {
            ArrayNumber(XmlConvert.ToString(value));
        }

        public void ArrayValue(long value)
        {
            ArrayNumber(XmlConvert.ToString(value));
        }

        public void ArrayValue(string value)
        {
            if (null == value)
            {
                ArrayNull();
                return;
            }

            Array("\"{0}\"".FormatWith(value), JsonNodeType.StringValue);
        }

        public void BooleanPair(string name, 
                                bool value)
        {
            Pair(name, XmlConvert.ToString(value), value ? JsonNodeType.TrueValue : JsonNodeType.FalseValue);
        }

        public void EndArray()
        {
            if (JsonNodeType.Array != Nesting.Peek().Parent)
            {
                throw new InvalidOperationException("The current context is not an array.");
            }

            _writer.Write(']');
            Nesting.Pop();
            Nesting.Peek().Previous = JsonNodeType.EndArray;
        }

        public void EndObject()
        {
            if (JsonNodeType.Object != Nesting.Peek().Parent)
            {
                throw new InvalidOperationException("The current context is not an object.");
            }

            _writer.Write('}');
            Nesting.Pop();
            Nesting.Peek().Previous = JsonNodeType.EndObject;
        }

        public void NullPair(string name)
        {
            Pair(name, "null", JsonNodeType.NullValue);
        }

        public void NumberPair(string name, 
                               decimal value)
        {
            NumberPair(name, XmlConvert.ToString(value));
        }

        public void NumberPair(string name, 
                               double value)
        {
            NumberPair(name, XmlConvert.ToString(value));
        }

        public void NumberPair(string name, 
                               long value)
        {
            NumberPair(name, XmlConvert.ToString(value));
        }

        public void NumberPair(string name, 
                               string value)
        {
            if (null == value)
            {
                NullPair(name);
                return;
            }

            value = value.Trim();
            if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            Pair(name, value, JsonNodeType.NumberValue);
        }

        public void Object()
        {
            if (JsonNodeType.Array == Nesting.Peek().Parent)
            {
                switch (Nesting.Peek().Previous)
                {
                    case JsonNodeType.EndObject:
                    case JsonNodeType.NullValue:
                    case JsonNodeType.TrueValue:
                    case JsonNodeType.FalseValue:
                    case JsonNodeType.NumberValue:
                    case JsonNodeType.StringValue:
                        _writer.Write(", ");
                        break;
                }
            }
            else if (JsonNodeType.None != Nesting.Peek().Previous)
            {
                throw new InvalidOperationException("Objects can only be started after the previous object has been ended or in an array.");
            }

            _writer.Write('{');
            Nesting.Peek().Previous = JsonNodeType.Object;
            Nesting.Push(new JsonWriterState(JsonNodeType.Object));
        }

        public void StringPair(string name, 
                               string value)
        {
            if (null == value)
            {
                NullPair(name);
                return;
            }

            Pair(name, "\"{0}\"".FormatWith(value), JsonNodeType.StringValue);
        }

        protected override void OnDispose()
        {
            if (null == _writer)
            {
                return;
            }

            _writer.Flush();
            _writer.BaseStream.Position = 0;
            _writer = null;
        }

        private void Array(string value, 
                           JsonNodeType type)
        {
            if (JsonNodeType.Array != Nesting.Peek().Parent)
            {
                throw new InvalidOperationException("Array values can only be added to an array.");
            }

            _writer.Write("{0}{1}".FormatWith(Nesting.Peek().Previous.In(JsonNodeType.None) ? string.Empty : ", ", value));
            Nesting.Peek().Previous = type;
        }

        public void Pair(string name,
                               string value, JsonNodeType type)
        {
            if (null == name)
            {
                throw new ArgumentNullException("name");
            }

            if (JsonNodeType.Array == Nesting.Peek().Parent)
            {
                throw new InvalidOperationException("Named values cannot be added to an array.");
            }

            _writer.Write("\"{0}\": {1}".FormatWith(name, value));
            Nesting.Peek().Previous = type;
        }
    }
}