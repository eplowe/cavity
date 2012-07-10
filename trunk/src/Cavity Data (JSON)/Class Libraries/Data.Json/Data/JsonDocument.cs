namespace Cavity.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This naming is intentional.")]
    public class JsonDocument : IEnumerable<JsonObject>
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

            var result = new JsonDocument();

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        JsonObject obj = null;
                        while (reader.Read())
                        {
                            if (JsonNodeType.Object == reader.NodeType)
                            {
                                obj = new JsonObject();
                                obj.ReadJson(reader);
                            }

                            if (JsonNodeType.EndObject == reader.NodeType)
                            {
                                result.Objects.Add(obj);
                            }
                        }
                    }
                }
            }

            return result;
        }

        public void Add(JsonObject item)
        {
            if (null == item)
            {
                throw new ArgumentNullException("item");
            }

            Objects.Add(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<JsonObject> GetEnumerator()
        {
            return ((IEnumerable<JsonObject>)Objects).GetEnumerator();
        }
    }
}