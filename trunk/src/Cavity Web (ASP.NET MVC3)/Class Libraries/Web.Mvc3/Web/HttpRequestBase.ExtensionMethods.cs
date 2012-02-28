namespace Cavity.Web
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Web;

    public static class HttpRequestBaseExtensionMethods
    {
        public static string RawQueryString(this HttpRequestBase request)
        {
            if (null == request)
            {
                throw new ArgumentNullException("request");
            }

            var index = request.RawUrl.IndexOf('?');

            return (-1 == index)
                ? string.Empty
                : request.RawUrl.Substring(index);
        }

        public static string RawQueryString(this HttpRequestBase request, 
                                            AlphaDecimal? token)
        {
            return request.RawQueryString(token, null);
        }

        public static string RawQueryString(this HttpRequestBase request, 
                                            AlphaDecimal? token, 
                                            string whence)
        {
            if (!string.IsNullOrEmpty(whence))
            {
                whence = HttpUtility.UrlEncode(whence);
            }

            var buffer = new StringBuilder();
            if (token.HasValue)
            {
                buffer.Append("[{0}]".FormatWith(token.Value));
            }

            var parts = RawQueryStringParts(request);
            if (0 != parts.Length)
            {
                if ("[]" != parts[0])
                {
                    var existing = Token(parts[0]);
                    if (!existing.HasValue)
                    {
                        buffer.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}", 0 == buffer.Length ? string.Empty : "&", parts[0]);
                    }
                }

                for (var i = 1; i < parts.Length; i++)
                {
                    if (!parts[i].StartsWith("whence=", StringComparison.OrdinalIgnoreCase))
                    {
                        buffer.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}", 0 == buffer.Length ? string.Empty : "&", parts[i]);
                    }
                }
            }

            if (!string.IsNullOrEmpty(whence))
            {
                buffer.AppendFormat(CultureInfo.InvariantCulture, "{0}whence={1}", 0 == buffer.Length ? string.Empty : "&", whence);
            }

            return (0 == buffer.Length ? string.Empty : "?") + buffer;
        }

        public static AlphaDecimal? Token(this HttpRequestBase request)
        {
            var parts = RawQueryStringParts(request);
            return 0 == parts.Length
                       ? null
                       : Token(parts[0]);
        }

        private static string[] RawQueryStringParts(HttpRequestBase request)
        {
            var query = request.RawQueryString();
            query = query.StartsWith("?", StringComparison.OrdinalIgnoreCase)
                ? query.Substring(1)
                : query;
            return query.Split('&', StringSplitOptions.RemoveEmptyEntries);
        }

        private static AlphaDecimal? Token(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            if (3 > value.Length)
            {
                return null;
            }

            if (value.Contains("="))
            {
                return null;
            }

            if (!value.StartsWith("[", StringComparison.Ordinal))
            {
                return null;
            }

            if (!value.EndsWith("]", StringComparison.Ordinal))
            {
                return null;
            }

            AlphaDecimal? result = null;

            try
            {
                result = AlphaDecimal.FromString(value.Substring(1, value.Length - 2));
            }
            catch (FormatException)
            {
            }

            return result;
        }
    }
}