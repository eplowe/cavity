namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;

    public sealed class JsonArray : JsonValue
    {
        public JsonArray()
        {
            Values = new List<JsonValue>();
        }

        public IList<JsonValue> Values { get; private set; }

        public bool Boolean(int index)
        {
            var value = Values[index];
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

        public bool IsNull(int index)
        {
            return Values[index] is JsonNull;
        }

        public JsonNumber Number(int index)
        {
            if (IsNull(index))
            {
                return null;
            }

            return (JsonNumber)Values[index];
        }

        public JsonObject Object(int index)
        {
            if (IsNull(index))
            {
                return null;
            }

            return (JsonObject)Values[index];
        }

        public JsonString String(int index)
        {
            if (IsNull(index))
            {
                return null;
            }

            return (JsonString)Values[index];
        }
    }
}