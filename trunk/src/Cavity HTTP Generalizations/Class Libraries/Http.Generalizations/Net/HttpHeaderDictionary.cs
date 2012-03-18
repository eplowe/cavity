namespace Cavity.Net
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public IEnumerable<HttpHeader> List
        {
            get
            {
                return this.Select(item => new HttpHeader(item.Key, item.Value));
            }
        }

        public static HttpHeaderDictionary FromString(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            var result = new HttpHeaderDictionary();
            var lines = value.Split(Environment.NewLine, StringSplitOptions.None).ToQueue();
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

        public void Add(HttpHeader header)
        {
            if (null == header)
            {
                throw new ArgumentNullException("header");
            }

            Add(header.Name, header.Value);
        }
    }
}