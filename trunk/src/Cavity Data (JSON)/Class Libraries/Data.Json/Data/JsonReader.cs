namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;

    public class JsonReader : DisposableObject
    {
        private StreamReader _reader;

        public JsonReader(Stream stream)
            : this()
        {
            if (null == stream)
            {
                throw new ArgumentNullException("stream");
            }

            _reader = new StreamReader(stream);
        }

        private JsonReader()
        {
            Nesting = new Stack<string>();
        }

        public bool IsEmptyArray { get; private set; }

        public bool IsEmptyObject { get; private set; }

        public string Name { get; private set; }

        public JsonNodeType NodeType { get; private set; }

        public string Value { get; private set; }

        private Stack<string> Nesting { get; set; }

        public bool Read()
        {
            while (!_reader.EndOfStream)
            {
                var p = (char)_reader.Peek();
                switch (p)
                {
                    case ' ':
                    case '\r':
                    case '\n':
                    case '\t':
                        _reader.Read();
                        continue;
                    case '{':
                        _reader.Read();
                        BeginObject();
                        break;
                    case '}':
                        _reader.Read();
                        EndObject();
                        break;
                    case '[':
                        _reader.Read();
                        BeginArray();
                        return true;
                    case ']':
                        _reader.Read();
                        EndArray();
                        break;
                    case '"':
                        if (NodeType != JsonNodeType.Object && null != Name)
                        {
                            break;
                        }

                        _reader.Read();
                        NodeType = JsonNodeType.Name;
                        Name = ReadQuoted();
                        return true;
                    case ':':
                        _reader.Read();
                        if (PeekNext().In('{', '['))
                        {
                            continue;
                        }

                        Value = ReadValue();
                        return true;
                    case ',':
                        _reader.Read();
                        if (NodeType == JsonNodeType.EndObject && 0 != Nesting.Count)
                        {
                            Name = Nesting.Peek();
                            continue;
                        }

                        NodeType = JsonNodeType.None;
                        Value = null;
                        if (Name != (0 == Nesting.Count ? null : Nesting.Peek()))
                        {
                            Name = null;
                        }

                        continue;
                }

                if (!NodeType.In(JsonNodeType.Object, JsonNodeType.EndObject) && 0 != Nesting.Count)
                {
                    Name = Nesting.Peek();
                    Value = ReadValue();
                    return true;
                }

                PeekNext();
                return true;
            }

            return !_reader.EndOfStream;
        }

        protected override void OnDispose()
        {
            if (null == _reader)
            {
                return;
            }

            _reader.Dispose();
            _reader = null;
        }

        private void BeginArray()
        {
            NodeType = JsonNodeType.Array;
            if (']' == PeekNext())
            {
                IsEmptyArray = true;
            }

            Nesting.Push(Name);
        }

        private void BeginObject()
        {
            NodeType = JsonNodeType.Object;
            IsEmptyObject = '}' == PeekNext();
        }

        private void EndArray()
        {
            NodeType = JsonNodeType.EndArray;
            IsEmptyArray = false;
            Nesting.Pop();
            Name = null;
            Value = null;
        }

        private void EndObject()
        {
            NodeType = JsonNodeType.EndObject;
            IsEmptyObject = false;
            Name = null;
            Value = null;
        }

        private char PeekNext()
        {
            while (!_reader.EndOfStream)
            {
                var p = (char)_reader.Peek();
                if (p.In(' ', '\r', '\n'))
                {
                    _reader.Read();
                    continue;
                }

                return p;
            }

            return '\0';
        }

        private bool ReadFalse()
        {
            var value = new MutableString();
            var position = _reader.BaseStream.Position;
            while (!_reader.EndOfStream)
            {
                var p = (char)_reader.Peek();
                if (!p.In('f', 'a', 'l', 's', 'e'))
                {
                    break;
                }

                value += (char)_reader.Read();
            }

            if ("false" == value)
            {
                return true;
            }

            _reader.BaseStream.Position = position;
            return false;
        }

        private bool ReadNull()
        {
            var value = new MutableString();
            var position = _reader.BaseStream.Position;
            while (!_reader.EndOfStream)
            {
                var p = (char)_reader.Peek();
                if (!p.In('n', 'u', 'l'))
                {
                    break;
                }

                value += (char)_reader.Read();
            }

            if ("null" == value)
            {
                return true;
            }

            _reader.BaseStream.Position = position;
            return false;
        }

        private string ReadNumber()
        {
            var value = new MutableString();
            while (!_reader.EndOfStream)
            {
                var p = (char)_reader.Peek();
                if (!char.IsDigit(p) && !p.In('.', '+', '-', 'e', 'E'))
                {
                    break;
                }

                value += (char)_reader.Read();
            }

            return value;
        }

        private string ReadQuoted()
        {
            var value = new MutableString();
            var escape = false;
            while (!_reader.EndOfStream)
            {
                var c = (char)_reader.Read();
                if (!escape && '"' == c)
                {
                    break;
                }

                if (!escape && '\\' == c)
                {
                    escape = true;
                    continue;
                }

                if (escape)
                {
                    switch (c)
                    {
                        case 'b':
                            c = '\b';
                            break;
                        case 'f':
                            c = '\f';
                            break;
                        case 'n':
                            c = '\n';
                            break;
                        case 'r':
                            c = '\r';
                            break;
                        case 't':
                            c = '\t';
                            break;
                        case 'u':
                            var unicode = new MutableString();
                            for (var i = 0; i < 4; i++)
                            {
                                unicode += (char)_reader.Read();
                            }

                            c = Convert.ToChar(int.Parse(unicode, NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo));
                            break;
                    }
                }

                value += c;
                escape = false;
            }

            return value;
        }

        private bool ReadTrue()
        {
            var value = new MutableString();
            var position = _reader.BaseStream.Position;
            while (!_reader.EndOfStream)
            {
                var p = (char)_reader.Peek();
                if (!p.In('t', 'r', 'u', 'e'))
                {
                    break;
                }

                value += (char)_reader.Read();
            }

            if ("true" == value)
            {
                return true;
            }

            _reader.BaseStream.Position = position;
            return false;
        }

        private string ReadValue()
        {
            while (!_reader.EndOfStream)
            {
                var p = PeekNext();
                if ('"' == p)
                {
                    _reader.Read();
                    NodeType = JsonNodeType.StringValue;
                    return ReadQuoted();
                }

                if (ReadNull())
                {
                    NodeType = JsonNodeType.NullValue;
                    return null;
                }

                if (ReadTrue())
                {
                    NodeType = JsonNodeType.TrueValue;
                    return "true";
                }

                if (ReadFalse())
                {
                    NodeType = JsonNodeType.FalseValue;
                    return "false";
                }

                NodeType = JsonNodeType.NumberValue;
                return ReadNumber();
            }

            return null;
        }
    }
}