namespace Cavity.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Xml;
    using Cavity.Globalization;

    public class AcceptLanguage
    {
        public AcceptLanguage()
        {
            Languages = new Collection<Language>();
        }

        public AcceptLanguage(IEnumerable<Language> languages)
        {
            Languages = new Collection<Language>(languages.ToList());
        }

        public Collection<Language> Languages { get; private set; }

        public static implicit operator AcceptLanguage(string value)
        {
            return FromString(value);
        }

        public static AcceptLanguage FromString(string value)
        {
            return new AcceptLanguage(Split(string.IsNullOrEmpty(value) ? "*" : value));
        }

        public ActionResult Negotiate(HttpRequestBase request, 
                                      Type controller)
        {
            if (null == request)
            {
                throw new ArgumentNullException("request");
            }

            ActionResult result = null;

            var supported = SupportedContentTypes(controller).ToList();
            foreach (var language in Languages.Where(supported.Contains))
            {
                var location = string.Concat(request.Path, '.', language, request.RawQueryString());
                result = new SeeOtherResult(location);
                break;
            }

            return result ?? new NotAcceptableResult();
        }

        private static IEnumerable<Language> Split(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                yield return "*";
                yield break;
            }

            if (!string.IsNullOrEmpty(value))
            {
                var any = false;
                var languages = new Dictionary<string, decimal>();
                var parts = value.Split(new[]
                {
                    ','
                }, 
                                        StringSplitOptions.RemoveEmptyEntries);
                for (var i = 0; i < parts.Length; i++)
                {
                    var part = parts[i];
                    var index = part.IndexOf(';');
                    decimal q = 1;
                    if (-1 != index)
                    {
                        q = XmlConvert.ToDecimal(part.Substring(index).RemoveFromStart(";q=", StringComparison.OrdinalIgnoreCase).Trim());
                        if (0 == q)
                        {
                            continue;
                        }

                        part = part.Substring(0, index);
                    }

                    part = part.Trim();
                    if ("*".Equals(part, StringComparison.Ordinal))
                    {
                        any = true;
                        continue;
                    }

                    if (!languages.ContainsKey(part))
                    {
                        languages.Add(part, q);
                    }
                }

                foreach (var rank in new[]
                {
                    1m, 0.9m, 0.8m, 0.7m, 0.6m, 0.5m, 0.4m, 0.3m, 0.2m, 0.1m
                })
                {
                    var rank1 = rank;
                    foreach (var language in languages.Where(x => rank1 == x.Value).OrderByDescending(x => x.Key))
                    {
                        yield return language.Key;
                    }
                }

                if (any)
                {
                    yield return "*";
                }
            }
        }

        private static IEnumerable<Language> SupportedContentTypes(Type controller)
        {
            if (null == controller)
            {
                throw new ArgumentNullException("controller");
            }

            if (!controller.IsSubclassOf(typeof(LanguageController)))
            {
                throw new ArgumentOutOfRangeException("controller");
            }

            var lanneg = Activator.CreateInstance(controller) as LanguageController;
            if (null == lanneg)
            {
                yield break;
            }

            foreach (var language in lanneg.Languages)
            {
                yield return language;
            }
        }
    }
}