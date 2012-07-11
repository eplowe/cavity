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
            : this(stream, null)
        {
        }

        public JsonWriter(Stream stream, 
                          JsonWriterSettings settings)
            : this()
        {
            if (null == stream)
            {
                throw new ArgumentNullException("stream");
            }

            _writer = new StreamWriter(stream);
            Indent = new MutableString();
            Settings = settings ?? new JsonWriterSettings();
        }

        private JsonWriter()
        {
            Nesting = new Stack<JsonWriterState>();
            Nesting.Push(new JsonWriterState(JsonNodeType.None));
        }

        private MutableString Indent { get; set; }

        private Stack<JsonWriterState> Nesting { get; set; }

        private string Punctuation
        {
            get
            {
                if (JsonNodeType.None == Nesting.Peek().Previous)
                {
                    return Nesting.Peek().Parent.In(JsonNodeType.Array, JsonNodeType.Object)
                               ? "{0}{1}".FormatWith(Settings.CommaPadding, Indent)
                               : string.Empty;
                }

                return ",{0}{1}".FormatWith(Settings.CommaPadding, Indent);
            }
        }

        private JsonWriterSettings Settings { get; set; }

        public void Array()
        {
            if (!Nesting.Peek().Parent.In(JsonNodeType.None, JsonNodeType.Array))
            {
                throw new InvalidOperationException("A nameless array is only permitted as a root container or within another array.");
            }

            _writer.Write("{0}[".FormatWith(Punctuation));

            Nesting.Push(new JsonWriterState(JsonNodeType.Array));
            if (0 != Settings.Indent.Length)
            {
                Indent += Settings.Indent;
            }
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

            _writer.Write("{0}\"{1}\":{2}[".FormatWith(Punctuation, name, Settings.ColonPadding));
            Nest(JsonNodeType.Array);
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

        public void ArrayValue(DateTime value)
        {
            ArrayValue(XmlConvert.ToString(value, XmlDateTimeSerializationMode.Utc));
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

            Array("\"{0}\"".FormatWith(Encode(value)), JsonNodeType.StringValue);
        }

        public void ArrayValue(TimeSpan value)
        {
            ArrayValue(XmlConvert.ToString(value));
        }

        public void EndArray()
        {
            if (JsonNodeType.Array != Nesting.Peek().Parent)
            {
                throw new InvalidOperationException("The current context is not an array.");
            }

            End(']', JsonNodeType.EndArray);
        }

        public void EndObject()
        {
            if (JsonNodeType.Object != Nesting.Peek().Parent)
            {
                throw new InvalidOperationException("The current context is not an object.");
            }

            End('}', JsonNodeType.EndObject);
        }

        public void NullPair(string name)
        {
            Pair(name, "null", JsonNodeType.NullValue);
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
                        _writer.Write(',');
                        break;
                }
            }
            else if (JsonNodeType.None != Nesting.Peek().Previous)
            {
                throw new InvalidOperationException("Objects can only be started after the previous object has been ended or in an array.");
            }

            if (JsonNodeType.None == Nesting.Peek().Parent &&
                JsonNodeType.None == Nesting.Peek().Previous)
            {
                _writer.Write('{');
            }
            else
            {
                _writer.Write("{0}{1}{2}".FormatWith(Settings.CommaPadding, Indent, '{'));
            }

            Nesting.Peek().Previous = JsonNodeType.Object;
            Nest(JsonNodeType.Object);
        }

        public void Object(string name)
        {
            if (JsonNodeType.Object != Nesting.Peek().Parent)
            {
                throw new InvalidOperationException("Named objects can only be started inside an object.");
            }

            switch (Nesting.Peek().Previous)
            {
                case JsonNodeType.EndObject:
                case JsonNodeType.NullValue:
                case JsonNodeType.TrueValue:
                case JsonNodeType.FalseValue:
                case JsonNodeType.NumberValue:
                case JsonNodeType.StringValue:
                    _writer.Write(',');
                    break;
            }

            _writer.Write("{0}{1}\"{2}\":{3}".FormatWith(Settings.CommaPadding, Indent, name, '{'));

            Nesting.Peek().Previous = JsonNodeType.Object;
            Nest(JsonNodeType.Object);
        }

        public void Pair(string name, 
                         bool value)
        {
            Pair(name, XmlConvert.ToString(value), value ? JsonNodeType.TrueValue : JsonNodeType.FalseValue);
        }

        public void Pair(string name, 
                         char value)
        {
            if ((char)0 == value)
            {
                NullPair(name);
                return;
            }

            Pair(name, XmlConvert.ToString(value));
        }

        public void Pair(string name, 
                         DateTime value)
        {
            Pair(name, XmlConvert.ToString(value, XmlDateTimeSerializationMode.Utc));
        }

        public void Pair(string name, 
                         DateTimeOffset value)
        {
            Pair(name, XmlConvert.ToString(value));
        }

        public void Pair(string name, 
                         decimal value)
        {
            NumberPair(name, XmlConvert.ToString(value));
        }

        public void Pair(string name, 
                         double value)
        {
            NumberPair(name, XmlConvert.ToString(value));
        }

        public void Pair(string name, 
                         Guid value)
        {
            Pair(name, XmlConvert.ToString(value));
        }

        public void Pair(string name, 
                         long value)
        {
            NumberPair(name, XmlConvert.ToString(value));
        }

        public void Pair(string name, 
                         TimeSpan value)
        {
            Pair(name, XmlConvert.ToString(value));
        }

        public void Pair(string name, 
                         string value)
        {
            if (null == value)
            {
                NullPair(name);
                return;
            }

            Pair(name, "\"{0}\"".FormatWith(Encode(value)), JsonNodeType.StringValue);
        }

        public void Pair(string name, 
                         IJsonSerializable value)
        {
            if (null == value)
            {
                NullPair(name);
                return;
            }

            _writer.Write("{0}\"{1}\":".FormatWith(Punctuation, name));
            Nesting.Peek().Previous = JsonNodeType.None;
            value.WriteJson(this);
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

        private static string Encode(MutableString value)
        {
            return value
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("\b", "\\b")
                .Replace("\f", "\\f")
                .Replace("\n", "\\n")
                .Replace("\r", "\\r")
                .Replace("\t", "\\t");
        }

        private void Array(string value, 
                           JsonNodeType type)
        {
            if (JsonNodeType.Array != Nesting.Peek().Parent)
            {
                throw new InvalidOperationException("Array values can only be added to an array.");
            }

            _writer.Write("{0}{1}".FormatWith(Punctuation, value));
            Nesting.Peek().Previous = type;
        }

        private void End(char value, 
                         JsonNodeType previous)
        {
            Nesting.Pop();
            Nesting.Peek().Previous = previous;
            if (0 != Settings.Indent.Length)
            {
                Indent.RemoveFromEnd(Settings.Indent);
            }

            _writer.Write("{0}{1}{2}".FormatWith(Settings.CommaPadding, Indent, value));
        }

        private void Nest(JsonNodeType type)
        {
            Nesting.Push(new JsonWriterState(type));
            if (0 != Settings.Indent.Length)
            {
                Indent += Settings.Indent;
            }
        }

        private void Pair(string name, 
                          string value, 
                          JsonNodeType type)
        {
            if (null == name)
            {
                throw new ArgumentNullException("name");
            }

            if (JsonNodeType.Array == Nesting.Peek().Parent)
            {
                throw new InvalidOperationException("Named values cannot be added to an array.");
            }

            _writer.Write("{0}\"{1}\":{2}{3}".FormatWith(Punctuation, name, Settings.ColonPadding, value));
            Nesting.Peek().Previous = type;
        }
    }
}