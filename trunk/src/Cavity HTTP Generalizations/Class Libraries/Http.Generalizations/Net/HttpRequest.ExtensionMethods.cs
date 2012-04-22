namespace Cavity.Net
{
    using System;
    using System.Globalization;
    using System.Net;

    public static class HttpRequestExtensionMethods
    {
#if NET20
        public static HttpWebRequest ToWebRequest(HttpRequest request,
                                                  CookieContainer cookies)
#else
        public static HttpWebRequest ToWebRequest(this HttpRequest request,
                                                  CookieContainer cookies)
#endif
        {
            if (null == request)
            {
                throw new ArgumentNullException("request");
            }

            if (null == cookies)
            {
                throw new ArgumentNullException("cookies");
            }

            if (null == request.Line)
            {
                throw new InvalidOperationException();
            }

            var result = (HttpWebRequest)WebRequest.Create((Uri)request.Line.RequestUri);
            result.Method = request.Line.Method;
            result.CookieContainer = cookies;
            result.AllowAutoRedirect = false;
            if (null == request.Headers)
            {
                return result;
            }

            foreach (var header in request.Headers)
            {
                switch (header.Name)
                {
                    case "AllowAutoRedirect":
                        result.AllowAutoRedirect = true;
                        break;

                    case "Accept":
                        result.Accept = header.Value;
                        break;

                    case "Content-Type":
                        result.ContentType = header.Value;
                        break;

                    case "Date":
#if NET20 || NET35
#else
                        result.Date = DateTime.Parse(header.Value, CultureInfo.InvariantCulture);
#endif
                        break;

                    case "If-Modified-Since":
                        result.IfModifiedSince = DateTime.Parse(header.Value, CultureInfo.InvariantCulture);
                        break;

                    case "Range":
                        break;

                    case "Referer":
                        result.Referer = header.Value;
                        break;

                    case "Transfer-Encoding":
                        break;

                    case "User-Agent":
                        result.UserAgent = header.Value;
                        break;

                    case "Connection":
                    case "Content-Length":
                    case "Expect":
                    case "Host":
                    case "Retry-After":
                    case "Trailer":
                    case "Vary":
                        break;

                    default:
                        result.Headers.Add(header.Name, header.Value);
                        break;
                }
            }

            return result;
        }
    }
}