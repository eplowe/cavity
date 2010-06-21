namespace Cavity.Net
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net.Mime;
    using System.Text;

    public sealed class HttpHeaderCollection : IComparable, IHttpHeaderCollection
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

        public static implicit operator string(HttpHeaderCollection value)
        {
            return object.ReferenceEquals(null, value) ? null as string : value.ToString();
        }

        public static implicit operator HttpHeaderCollection(string value)
        {
            return object.ReferenceEquals(null, value) ? null as HttpHeaderCollection : HttpHeaderCollection.Parse(value);
        }

        public static bool operator ==(HttpHeaderCollection operand1, HttpHeaderCollection operand2)
        {
            return 0 == HttpHeaderCollection.Compare(operand1, operand2);
        }

        public static bool operator !=(HttpHeaderCollection operand1, HttpHeaderCollection operand2)
        {
            return 0 != HttpHeaderCollection.Compare(operand1, operand2);
        }

        public static bool operator <(HttpHeaderCollection operand1, HttpHeaderCollection operand2)
        {
            return HttpHeaderCollection.Compare(operand1, operand2) < 0;
        }

        public static bool operator >(HttpHeaderCollection operand1, HttpHeaderCollection operand2)
        {
            return HttpHeaderCollection.Compare(operand1, operand2) > 0;
        }

        public static int Compare(HttpHeaderCollection comparand1, HttpHeaderCollection comparand2)
        {
            return object.ReferenceEquals(comparand1, comparand2)
                ? 0
                : string.Compare(
                    object.ReferenceEquals(null, comparand1) ? null as string : comparand1.ToDictionaryString(),
                    object.ReferenceEquals(null, comparand2) ? null as string : comparand2.ToDictionaryString(),
                    StringComparison.OrdinalIgnoreCase);
        }

        public static HttpHeaderCollection Parse(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            var result = new HttpHeaderCollection();

            foreach (var line in value.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
            {
                if (0 == line.Length)
                {
                    break;
                }

                result.Add(HttpHeader.Parse(line));
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

        public int CompareTo(object obj)
        {
            int comparison = 1;

            if (!object.ReferenceEquals(null, obj))
            {
                HttpHeaderCollection value = obj as HttpHeaderCollection;

                if (object.ReferenceEquals(null, value))
                {
                    throw new ArgumentOutOfRangeException("obj");
                }

                comparison = HttpHeaderCollection.Compare(this, value);
            }

            return comparison;
        }

        public bool Contains(IHttpHeader item)
        {
            return this._collection.Contains(item);
        }

        public void CopyTo(IHttpHeader[] array, int arrayIndex)
        {
            this._collection.CopyTo(array, arrayIndex);
        }

        public override bool Equals(object obj)
        {
            bool result = false;

            if (!object.ReferenceEquals(null, obj))
            {
                if (object.ReferenceEquals(this, obj))
                {
                    result = true;
                }
                else
                {
                    HttpHeaderCollection cast = obj as HttpHeaderCollection;

                    if (!object.ReferenceEquals(null, cast))
                    {
                        result = 0 == HttpHeaderCollection.Compare(this, cast);
                    }
                }
            }

            return result;
        }

        public IEnumerator<IHttpHeader> GetEnumerator()
        {
            return this._collection.GetEnumerator();
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public bool Remove(IHttpHeader item)
        {
            return this._collection.Remove(item);
        }

        public IDictionary<string, string> ToDictionary()
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

        public override string ToString()
        {
            StringBuilder buffer = new StringBuilder();

            foreach (var item in this._collection)
            {
                buffer.AppendLine(item.ToString());
            }

            return buffer.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this._collection as IEnumerable).GetEnumerator();
        }

        private string ToDictionaryString()
        {
            StringBuilder buffer = new StringBuilder();
            foreach (var item in this.ToDictionary())
            {
                buffer.AppendLine(string.Concat(item.Key, ": ", item.Value));
            }

            return buffer.ToString();
        }
    }
}