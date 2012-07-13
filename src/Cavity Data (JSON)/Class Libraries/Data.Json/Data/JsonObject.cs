namespace Cavity.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This naming is intentional.")]
    public class JsonObject : JsonValue, 
                              IEnumerable<JsonPair>, 
                              IJsonSerializable
    {
        public JsonObject()
        {
            Objects = new List<JsonPair>();
        }

        public int Count
        {
            get
            {
                return Objects.Count;
            }
        }

        private List<JsonPair> Objects { get; set; }

        public JsonPair this[int index]
        {
            get
            {
                return Objects[index];
            }
        }

        public JsonPair this[string name]
        {
            get
            {
                var item = Objects.FirstOrDefault(x => x.Name == name);
                if (null == item)
                {
                    throw new KeyNotFoundException(name);
                }

                return item;
            }
        }

        public void Add(string name, 
                        JsonValue value)
        {
            Add(new JsonPair(name, value));
        }

        public void Add(JsonPair item)
        {
            if (null == item)
            {
                throw new ArgumentNullException("item");
            }

            Objects.Add(item);
        }

        public JsonArray Array(string name)
        {
            if (IsNull(name))
            {
                return null;
            }

            return (JsonArray)this[name].Value;
        }

        public bool Boolean(string name)
        {
            var value = this[name].Value;
            if (value is JsonFalse)
            {
                return false;
            }

            if (value is JsonTrue)
            {
                return true;
            }

            throw new InvalidCastException();
        }

        public bool IsNull(string name)
        {
            return this[name].Value is JsonNull;
        }

        public JsonNumber Number(string name)
        {
            if (IsNull(name))
            {
                return null;
            }

            return (JsonNumber)this[name].Value;
        }

        public JsonObject Object(string name)
        {
            if (IsNull(name))
            {
                return null;
            }

            return (JsonObject)this[name].Value;
        }

        public JsonString String(string name)
        {
            if (IsNull(name))
            {
                return null;
            }

            return (JsonString)this[name].Value;
        }

        public override string ToString()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    writer.Object();
                    WriteJson(writer);
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<JsonPair> GetEnumerator()
        {
            return ((IEnumerable<JsonPair>)Objects).GetEnumerator();
        }

        public virtual void ReadJson(JsonReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            if (JsonNodeType.Object != reader.NodeType)
            {
                throw new InvalidOperationException();
            }

            if (reader.IsEmptyObject)
            {
                reader.Read();
                return;
            }

            string name = null;
            while (reader.Read())
            {
                JsonValue value = null;
                switch (reader.NodeType)
                {
                    case JsonNodeType.Name:
                        name = reader.Name;
                        break;

                    case JsonNodeType.NullValue:
                        value = new JsonNull();
                        break;

                    case JsonNodeType.TrueValue:
                        value = new JsonTrue();
                        break;

                    case JsonNodeType.FalseValue:
                        value = new JsonFalse();
                        break;

                    case JsonNodeType.NumberValue:
                        value = new JsonNumber(reader.Value);
                        break;

                    case JsonNodeType.StringValue:
                        value = new JsonString(reader.Value);
                        break;

                    case JsonNodeType.Array:
                        value = ReadJsonArray(reader);
                        break;

                    case JsonNodeType.Object:
                        var obj = new JsonObject();
                        obj.ReadJson(reader);
                        value = obj;
                        break;

                    case JsonNodeType.EndObject:
                        return;
                }

                if (null != value)
                {
                    Add(name, value);
                }
            }
        }

        public void WriteJson(JsonWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }

            foreach (var pair in this)
            {
                var obj = pair.Value as IJsonSerializable;
                if (null != obj)
                {
                    writer.Pair(pair.Name, obj);
                    continue;
                }

                var number = pair.Value as JsonNumber;
                if (null != number)
                {
                    writer.NumberPair(pair.Name, number.Value);
                    continue;
                }

                var str = pair.Value as JsonString;
                if (null != str)
                {
                    writer.Pair(pair.Name, str.Value);
                    continue;
                }

                var array = pair.Value as JsonArray;
                if (null != array)
                {
                    WriteJsonArray(writer, pair.Name, array);
                    continue;
                }

                if (pair.Value is JsonNull)
                {
                    writer.NullPair(pair.Name);
                }
                else if (pair.Value is JsonTrue)
                {
                    writer.Pair(pair.Name, true);
                }
                else if (pair.Value is JsonFalse)
                {
                    writer.Pair(pair.Name, false);
                }
            }

            writer.EndObject();
        }

        private static JsonArray ReadJsonArray(JsonReader reader)
        {
            var array = new JsonArray();
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case JsonNodeType.NullValue:
                        array.Values.Add(new JsonNull());
                        break;

                    case JsonNodeType.TrueValue:
                        array.Values.Add(new JsonTrue());
                        break;

                    case JsonNodeType.FalseValue:
                        array.Values.Add(new JsonFalse());
                        break;

                    case JsonNodeType.NumberValue:
                        array.Values.Add(new JsonNumber(reader.Value));
                        break;

                    case JsonNodeType.StringValue:
                        array.Values.Add(new JsonString(reader.Value));
                        break;

                    case JsonNodeType.Array:
                        array.Values.Add(ReadJsonArray(reader));
                        break;

                    case JsonNodeType.EndArray:
                        return array;

                    case JsonNodeType.Object:
                        while (JsonNodeType.Object == reader.NodeType)
                        {
                            var obj = new JsonObject();
                            obj.ReadJson(reader);
                            array.Values.Add(obj);
                            if (JsonNodeType.EndArray == reader.NodeType)
                            {
                                return array;
                            }
                        }

                        continue;
                }
            }

            return array;
        }

        private static void WriteJsonArray(JsonWriter writer, 
                                           string name, 
                                           JsonArray value)
        {
            if (null == name)
            {
                writer.Array();
            }
            else
            {
                writer.Array(name);
            }

            foreach (var item in value.Values)
            {
                var obj = item as IJsonSerializable;
                if (null != obj)
                {
                    writer.Object();
                    obj.WriteJson(writer);
                    ////writer.EndObject();
                    continue;
                }

                var number = item as JsonNumber;
                if (null != number)
                {
                    writer.ArrayNumber(number.Value);
                    continue;
                }

                var str = item as JsonString;
                if (null != str)
                {
                    writer.ArrayValue(str.Value);
                    continue;
                }

                var array = item as JsonArray;
                if (null != array)
                {
                    WriteJsonArray(writer, null, array);
                    continue;
                }

                if (item is JsonNull)
                {
                    writer.ArrayNull();
                }
                else if (item is JsonTrue)
                {
                    writer.ArrayValue(true);
                }
                else if (item is JsonFalse)
                {
                    writer.ArrayValue(false);
                }
            }

            writer.EndArray();
        }
    }
}