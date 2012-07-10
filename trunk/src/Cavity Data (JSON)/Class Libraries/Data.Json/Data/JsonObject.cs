namespace Cavity.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
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
            JsonArray array = null;
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
                        array = new JsonArray();
                        continue;

                    case JsonNodeType.EndArray:
                        value = array;
                        array = null;
                        break;

                    case JsonNodeType.Object:
                        var obj = new JsonObject();
                        obj.ReadJson(reader);
                        value = obj;
                        break;

                    case JsonNodeType.EndObject:
                        reader.Read();
                        return;
                }

                if (null != array)
                {
                    array.Values.Add(value);
                    continue;
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

            writer.Object();
            foreach (var pair in this)
            {
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
                    writer.Pair(pair.Name, null);
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
        
        private static void WriteJsonArray(JsonWriter writer, string name, JsonArray value)
        {
            writer.Array(name);
            foreach (var item in value.Values)
            {
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

                ////var array = item as JsonArray;
                ////if (null != array)
                ////{
                ////    WriteJsonArray(writer, array);
                ////    continue;
                ////}

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