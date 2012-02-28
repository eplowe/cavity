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

            var supported = SupportedContentTypes(controller).ToList();
            ActionResult result = Languages
                .Where(supported.Contains)
                .Select(language => request.Path.Append(".", language, request.RawQueryString()))
                .Select(location => new SeeOtherResult(location))
                .FirstOrDefault();

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
                var parts = value.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var part in parts)
                {
                    var item = part;
                    var index = item.IndexOf(';');
                    decimal q = 1;
                    if (-1 != index)
                    {
                        q = XmlConvert.ToDecimal(item.Substring(index).RemoveFromStart(";q=", StringComparison.OrdinalIgnoreCase).Trim());
                        if (0 == q)
                        {
                            continue;
                        }

                        item = item.Substring(0, index);
                    }

                    item = item.Trim();
                    if ("*".Equals(item, StringComparison.Ordinal))
                    {
                        any = true;
                        continue;
                    }

                    if (!languages.ContainsKey(item))
                    {
                        languages.Add(item, q);
                    }
                }

                var ranks = new[]
                {
                    1m, 0.9m, 0.8m, 0.7m, 0.6m, 0.5m, 0.4m, 0.3m, 0.2m, 0.1m
                };
                foreach (var language in ranks.SelectMany(item => languages.Where(x => item == x.Value)
                                                                      .OrderByDescending(x => x.Key)))
                {
                    yield return language.Key;
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