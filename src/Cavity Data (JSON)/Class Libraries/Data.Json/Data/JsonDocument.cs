namespace Cavity.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This naming is intentional.")]
    public class JsonDocument : IEnumerable<JsonObject>, 
                                IJsonSerializable
    {
        public JsonDocument()
        {
            Objects = new List<JsonObject>();
        }

        public int Count
        {
            get
            {
                return Objects.Count;
            }
        }

        private List<JsonObject> Objects { get; set; }

        public JsonObject this[int index]
        {
            get
            {
                return Objects[index];
            }
        }

        public static JsonDocument Load(string json)
        {
            if (null == json)
            {
                throw new ArgumentNullException("json");
            }

            var document = new JsonDocument();

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        document.ReadJson(reader);
                    }
                }
            }

            return document;
        }

        public void Add(JsonObject item)
        {
            if (null == item)
            {
                throw new ArgumentNullException("item");
            }

            Objects.Add(item);
        }

        public override string ToString()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
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

        public IEnumerator<JsonObject> GetEnumerator()
        {
            return ((IEnumerable<JsonObject>)Objects).GetEnumerator();
        }

        public void ReadJson(JsonReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            if (JsonNodeType.None != reader.NodeType)
            {
                throw new InvalidOperationException();
            }

            while (reader.Read())
            {
                if (JsonNodeType.Object != reader.NodeType)
                {
                    continue;
                }

                var obj = new JsonObject();
                obj.ReadJson(reader);
                Add(obj);
            }
        }

        public void WriteJson(JsonWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }

            if (0 == Count)
            {
                return;
            }

            if (1 < Count)
            {
                writer.Array();
            }

            foreach (var item in this)
            {
                writer.Object();
                item.WriteJson(writer);
                ////writer.EndObject();
            }

            if (1 < Count)
            {
                writer.EndArray();
            }
        }
    }
}