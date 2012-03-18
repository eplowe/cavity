namespace Cavity.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net.Mime;
    using System.Web;
    using System.Web.Mvc;

    public class Accept
    {
        public Accept()
        {
            ContentTypes = new Collection<ContentType>();
        }

        public Collection<ContentType> ContentTypes { get; private set; }

        public static implicit operator Accept(string value)
        {
            return FromString(value);
        }

        public static Accept FromString(string value)
        {
            return Parse(Split(string.IsNullOrEmpty(value) ? "*/*" : value).ToList());
        }

        public ActionResult Negotiate(HttpRequestBase request, 
                                      Type controller)
        {
            if (null == request)
            {
                throw new ArgumentNullException("request");
            }

            var supported = SupportedContentTypes(controller);
            ActionResult result = ContentTypes
                .Where(supported.ContainsKey)
                .Select(type => request.Path.Append(".", supported[type], request.RawQueryString()))
                .Select(location => new SeeOtherResult(location))
                .FirstOrDefault();

            return result ?? new NotAcceptableResult();
        }

        private static Accept Parse(IList<ContentType> types)
        {
            var result = new Accept();

            foreach (var type in types.Where(x => !x.MediaType.EndsWith("/*", StringComparison.OrdinalIgnoreCase)))
            {
                result.ContentTypes.Add(type);
            }

            var any = false;
            foreach (var type in types)
            {
                if (type.MediaType.Equals("*/*", StringComparison.OrdinalIgnoreCase))
                {
                    any = true;
                    continue;
                }

                if (!type.MediaType.EndsWith("/*", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                result.ContentTypes.Add(type);
            }

            if (any)
            {
                result.ContentTypes.Add(new ContentType("*/*"));
            }

            return result;
        }

        private static IEnumerable<ContentType> Split(string value)
        {
            var result = new List<ContentType>();

            if (!string.IsNullOrEmpty(value))
            {
                var parts = value.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var type in (from part in parts
                                      let index = part.IndexOf(';')
                                      select -1 == index ? part.Trim() : part.Substring(0, index).Trim())
                    .Select(item => new ContentType("*".Equals(item, StringComparison.Ordinal) ? "*/*" : item))
                    .Where(type => !result.Contains(type)))
                {
                    result.Add(type);
                }
            }

            return result;
        }

        private static Dictionary<ContentType, string> SupportedContentTypes(Type controller)
        {
            if (null == controller)
            {
                throw new ArgumentNullException("controller");
            }

            if (!controller.IsSubclassOf(typeof(Controller)))
            {
                throw new ArgumentOutOfRangeException("controller");
            }

            var result = new Dictionary<ContentType, string>();

            foreach (var method in controller.GetMethods())
            {
                foreach (var attribute in method.GetCustomAttributes(typeof(ContentNegotiationAttribute), true))
                {
                    var conneg = attribute as ContentNegotiationAttribute;
                    if (null == conneg)
                    {
                        continue;
                    }

                    foreach (var type in conneg.ToContentTypes())
                    {
                        result.Add(type, conneg.Extension);
                    }
                }
            }

            return result;
        }
    }
}