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

            ActionResult result = null;

            var supported = SupportedContentTypes(controller);
            foreach (var type in ContentTypes.Where(supported.ContainsKey))
            {
                var location = string.Concat(request.Path, '.', supported[type], request.RawQueryString());
                result = new SeeOtherResult(location);
                break;
            }

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
                }
                else if (type.MediaType.EndsWith("/*", StringComparison.OrdinalIgnoreCase))
                {
                    result.ContentTypes.Add(type);
                }
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
                var parts = value.Split(new[]
                {
                    ','
                }, 
                                        StringSplitOptions.RemoveEmptyEntries);
                for (var i = 0; i < parts.Length; i++)
                {
                    var part = parts[i];
                    var index = part.IndexOf(';');
                    part = -1 == index ? part.Trim() : part.Substring(0, index).Trim();
                    if ("*".Equals(part, StringComparison.Ordinal))
                    {
                        part = "*/*";
                    }

                    var type = new ContentType(part);
                    if (!result.Contains(type))
                    {
                        result.Add(type);
                    }
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
                foreach (object attribute in method.GetCustomAttributes(typeof(ContentNegotiationAttribute), true))
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