namespace Cavity.Net
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Net.Mime;
    using System.Text;
    using Cavity.Net.Mime;

    public sealed class HttpHeaderCollection : ComparableObject, ICollection<IHttpHeader>, IContentType
    {
        private Collection<IHttpHeader> _collection;
        
        public HttpHeaderCollection()
        {
            this._collection = new Collection<IHttpHeader>();
        }

        public ContentType ContentType
        {
            get
            {
                string value = this["Content-Type"];

                return null == value ? null as ContentType : new ContentType(value);
            }
        }

        public int Count
        {
            get
            {
                return this._collection.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public string this[string name]
        {
            get
            {
                if (null == name)
                {
                    throw new ArgumentNullException("name");
                }

                string value = null;

                foreach (var item in this._collection.Where(x => string.Equals(x.Name, name, StringComparison.Ordinal)))
                {
                    if (null == value)
                    {
                        value = item.Value;
                    }
                    else
                    {
                        value += ", " + item.Value;
                    }
                }

                return value;
            }
        }

        public static implicit operator HttpHeaderCollection(string value)
        {
            return object.ReferenceEquals(null, value) ? null as HttpHeaderCollection : HttpHeaderCollection.FromString(value);
        }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "This is an odd rule that seems to be impossible to actually pass.")]
        public static HttpHeaderCollection FromString(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            var result = new HttpHeaderCollection();

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(value);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new StreamReader(stream))
                    {
                        result.Read(reader);
                    }
                }
            }

            return result;
        }

        public void Add(IHttpHeader item)
        {
            if (null == item)
            {
                throw new ArgumentNullException("item");
            }

            this._collection.Add(item);
        }

        public void Clear()
        {
            this._collection.Clear();
        }

        public bool Contains(IHttpHeader item)
        {
            return this._collection.Contains(item);
        }

        public bool ContainsName(Token name)
        {
            if (null == name)
            {
                throw new ArgumentNullException("name");
            }

            return 0 != this._collection.Where(x => x.Name.Equals(name)).Count();
        }

        public void CopyTo(IHttpHeader[] array, int arrayIndex)
        {
            this._collection.CopyTo(array, arrayIndex);
        }

        public IEnumerator<IHttpHeader> GetEnumerator()
        {
            return this._collection.GetEnumerator();
        }

        public void Read(TextReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            while (true)
            {
                int peek = reader.Peek();
                string line = reader.ReadLine();
                if (-1 == peek || 13 == peek)
                {
                    break; // EOF or CR
                }
                else
                {
                    while (9 == reader.Peek() || 32 == reader.Peek())
                    {
                        line += Environment.NewLine + reader.ReadLine(); // HT or SPACE
                    }

                    this.Add(HttpHeader.FromString(line));
                }
            }
        }

        public bool Remove(IHttpHeader item)
        {
            return this._collection.Remove(item);
        }

        public override string ToString()
        {
            StringBuilder buffer = new StringBuilder();

            foreach (var item in this.ToDictionary())
            {
                buffer.AppendLine(string.Concat(item.Key, ": ", item.Value));
            }

            return buffer.ToString();
        }

        public void Write(TextWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }

            foreach (var item in this.ToDictionary())
            {
                writer.WriteLine(string.Concat(item.Key, ": ", item.Value));
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this._collection as IEnumerable).GetEnumerator();
        }

        private IDictionary<string, string> ToDictionary()
        {
            var result = new Dictionary<string, string>();

            foreach (var item in this._collection)
            {
                if (result.ContainsKey(item.Name))
                {
                    result[item.Name] = string.Concat(result[item.Name], ", ", item.Value);
                }
                else
                {
                    result.Add(item.Name, item.Value);
                }
            }
            
            return result;
        }
    }
}