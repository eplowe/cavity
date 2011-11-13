namespace Cavity
{
    using System;
    using System.Globalization;
    using System.Net;
    using Cavity.Net;

    public class HttpExpectation : IHttpExpectation
    {
        public HttpExchange Exchange { get; set; }

        public static HttpWebRequest GetRequest(HttpRequest request, 
                                                CookieContainer cookies)
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

            foreach (var header in request.Headers.List)
            {
                switch (header.Name)
                {
                    case "Accept":
                        result.Accept = header.Value;
                        break;

                    case "Content-Type":
                        result.ContentType = header.Value;
                        break;

                    case "Date":
                        result.Date = DateTime.Parse(header.Value, CultureInfo.InvariantCulture);
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

        public static HttpWebResponse GetResponse(HttpRequest request, 
                                                  CookieContainer cookies)
        {
            try
            {
                return (HttpWebResponse)GetRequest(request, cookies).GetResponse();
            }
            catch (WebException exception)
            {
                return (HttpWebResponse)exception.Response;
            }
        }

        public bool Verify(CookieContainer cookies)
        {
            if (null == cookies)
            {
                throw new ArgumentNullException("cookies");
            }

            if (null == Exchange)
            {
                throw new InvalidOperationException();
            }

            if (null == Exchange.Response)
            {
                throw new InvalidOperationException();
            }

            using (var response = GetResponse(Exchange.Request, cookies))
            {
                cookies = new CookieContainer();
                foreach (Cookie cookie in response.Cookies)
                {
                    cookies.Add(cookie);
                }

                if (Exchange.Response.Line.Code != (int)response.StatusCode ||
                    Exchange.Response.Line.Reason != response.StatusDescription)
                {
                    var message = "\"{0} {1}\" was expected, but \"{2} {3}\" was actually recieved.".FormatWith(Exchange.Response.Line.Code, 
                                                                                                                Exchange.Response.Line.Reason, 
                                                                                                                (int)response.StatusCode, 
                                                                                                                response.StatusDescription);
                    throw new HttpTestException(message);
                }

                foreach (var header in Exchange.Response.Headers.List)
                {
                    if (response.Headers[header.Name] ==
                        header.Value)
                    {
                        continue;
                    }

                    var message = "\"{0}\" was expected, but \"{1}: {2}\" was actually recieved.".FormatWith(header, 
                                                                                                             header.Name, 
                                                                                                             response.Headers[header.Name]);
                    throw new HttpTestException(message);
                }
            }

            return true;
        }
    }
}