namespace Cavity.Net
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
    using System.Net.Mime;
    using System.Text;

    using Cavity.Net.Mime;

    public sealed class HttpHeaderCollection : ComparableObject, 
                                               ICollection<IHttpHeader>, 
                                               IContentType
    {
        private readonly Collection<IHttpHeader> _collection;

        public HttpHeaderCollection()
        {
            _collection = new Collection<IHttpHeader>();
        }

        public ContentType ContentType
        {
            get
            {
                var value = this["Content-Type"];

                return null == value
                           ? null
                           : new ContentType(value);
            }
        }

        public int Count
        {
            get
            {
                return _collection.Count;
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

                foreach (var item in _collection
                    .Where(x => string.Equals(x.Name, name, StringComparison.Ordinal)))
                {
                    if (null == value)
                    {
                        value = item.Value;
                        continue;
                    }

                    value += ", " + item.Value;
                }

                return value;
            }
        }

        public static implicit operator HttpHeaderCollection(string value)
        {
            return ReferenceEquals(null, value)
                       ? null
                       : FromString(value);
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

        public bool ContainsName(Token name)
        {
            if (null == name)
            {
                throw new ArgumentNullException("name");
            }

            return 0 != _collection.Count(x => x.Name.Equals(name));
        }

        public void Read(TextReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            while (true)
            {
                var peek = reader.Peek();
                var line = reader.ReadLine();
                if (peek.EqualsOneOf(-1, 13))
                {
                    break; // EOF or CR
                }

                while (reader.Peek().EqualsOneOf(9, 32))
                {
                    line += Environment.NewLine + reader.ReadLine(); // HT or SPACE
                }

                Add(HttpHeader.FromString(line));
            }
        }

        public override string ToString()
        {
            var buffer = new StringBuilder();

            foreach (var item in ToDictionary())
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

            foreach (var item in ToDictionary())
            {
                writer.WriteLine(string.Concat(item.Key, ": ", item.Value));
            }
        }

        public void Add(IHttpHeader item)
        {
            if (null == item)
            {
                throw new ArgumentNullException("item");
            }

            _collection.Add(item);
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains(IHttpHeader item)
        {
            return _collection.Contains(item);
        }

        public void CopyTo(IHttpHeader[] array, 
                           int arrayIndex)
        {
            _collection.CopyTo(array, arrayIndex);
        }

        public bool Remove(IHttpHeader item)
        {
            return _collection.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (_collection as IEnumerable).GetEnumerator();
        }

        public IEnumerator<IHttpHeader> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        private IEnumerable<KeyValuePair<string, string>> ToDictionary()
        {
            var result = new Dictionary<string, string>();

            foreach (var item in _collection)
            {
                if (result.ContainsKey(item.Name))
                {
                    result[item.Name] = string.Concat(result[item.Name], ", ", item.Value);
                    continue;
                }

                result.Add(item.Name, item.Value);
            }

            return result;
        }
    }
}