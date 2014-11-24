namespace Cavity.Net
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Cavity.Collections;

    [Serializable]
    public class HttpHeaderDictionary : Dictionary<Token, string>,
                                        IHttpMessageHeaders
    {
        public HttpHeaderDictionary()
        {
        }

        protected HttpHeaderDictionary(SerializationInfo info,
                                       StreamingContext context)
            : base(info, context)
        {
        }

        public static HttpHeaderDictionary FromString(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            var result = new HttpHeaderDictionary();
#if NET20
            var lines = IEnumerableExtensionMethods.ToQueue(StringExtensionMethods.Split(value, Environment.NewLine, StringSplitOptions.None));
#else
            var lines = value.Split(Environment.NewLine, StringSplitOptions.None).ToQueue();
#endif
            if (0 == lines.Count)
            {
                return result;
            }

            while (0 != lines.Count)
            {
                var line = lines.Dequeue();
                if (0 == line.Length)
                {
                    break;
                }

                result.Add(HttpHeader.FromString(line));
            }

            return result;
        }

        IEnumerator<HttpHeader> IEnumerable<HttpHeader>.GetEnumerator()
        {
            foreach (var item in this)
            {
                yield return new HttpHeader(item.Key, item.Value);
            }
        }

        public void Add(HttpHeader header)
        {
            if (null == header)
            {
                throw new ArgumentNullException("header");
            }

            Add(header.Name, header.Value);
        }

        public bool Contains(Token name)
        {
            if (null == name)
            {
                throw new ArgumentNullException("name");
            }

            return ContainsKey(name);
        }
    }
}