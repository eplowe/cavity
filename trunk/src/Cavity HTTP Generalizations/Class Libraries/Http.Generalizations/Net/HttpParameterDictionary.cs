namespace Cavity.Net
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;

    [Serializable]
    public class HttpParameterDictionary : Dictionary<Token, string>
    {
        public HttpParameterDictionary()
        {
        }

        protected HttpParameterDictionary(SerializationInfo info,
                                          StreamingContext context)
            : base(info, context)
        {
        }

        public Quality Quality
        {
            get
            {
                if (ContainsKey("q"))
                {
                    return this["q"];
                }

                return Quality.One;
            }

            set
            {
                this["q"] = value;
            }
        }

        public virtual void Add(HttpParameter parameter)
        {
            if (null == parameter)
            {
                throw new ArgumentNullException("parameter");
            }

            Add(parameter.Name, parameter.Value);
        }

        public override string ToString()
        {
            var buffer = new StringBuilder();
            foreach (var item in this)
            {
#if NET20
                buffer.Append(StringExtensionMethods.FormatWith(";{0}={1}", item.Key, item.Value));
#else
                buffer.Append(";{0}={1}".FormatWith(item.Key, item.Value));
#endif
            }

            return buffer.ToString();
        }
    }
}