namespace Cavity.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mime;

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class ContentNegotiationAttribute : Attribute
    {
        private string _extension;

        private string _mediaTypes;

        public ContentNegotiationAttribute(string extension, 
                                           string mediaTypes)
            : this()
        {
            Extension = extension;
            MediaTypes = mediaTypes;
        }

        private ContentNegotiationAttribute()
        {
        }

        public string Extension
        {
            get
            {
                return _extension;
            }

            private set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                if (0 == value.Length)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                _extension = value.StartsWith(".", StringComparison.OrdinalIgnoreCase)
                                 ? value.Substring(1)
                                 : value;
            }
        }

        public string MediaTypes
        {
            get
            {
                return _mediaTypes;
            }

            private set
            {
                if (0 == ToContentTypes(value).Count())
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                _mediaTypes = value;
            }
        }

        public IEnumerable<ContentType> ToContentTypes()
        {
            return ToContentTypes(MediaTypes);
        }

        private static IEnumerable<ContentType> ToContentTypes(string mediaTypes)
        {
            if (null == mediaTypes)
            {
                throw new ArgumentNullException("mediaTypes");
            }

            var parts = mediaTypes.Split(new[]
            {
                ','
            }, 
            StringSplitOptions.RemoveEmptyEntries);

            return parts.Select(part => new ContentType(part.Trim())).ToList();
        }
    }
}