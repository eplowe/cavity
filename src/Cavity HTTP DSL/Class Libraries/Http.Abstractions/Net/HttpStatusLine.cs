namespace Cavity.Net
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net;
    using System.Xml;
    using Cavity.Collections;

    public class HttpStatusLine : ComparableObject
    {
        public HttpStatusLine(HttpStatusCode code)
            : this(HttpVersion.Latest, code)
        {
        }

        public HttpStatusLine(HttpVersion version, 
                              HttpStatusCode code)
            : this(version, (int)code, ReasonPhase(code))
        {
        }

        public HttpStatusLine(int code, 
                              string reason)
            : this(HttpVersion.Latest, code, reason)
        {
        }

        public HttpStatusLine(HttpVersion version, 
                              int code, 
                              string reason)
            : this()
        {
            if (null == version)
            {
                throw new ArgumentNullException("version");
            }

            if (0 > code)
            {
                throw new ArgumentOutOfRangeException("code");
            }

            if (null == reason)
            {
                throw new ArgumentNullException("reason");
            }

            if (0 == reason.Length)
            {
                throw new ArgumentOutOfRangeException("reason");
            }

            Version = version;
            Code = code;
            Reason = reason;
        }

        private HttpStatusLine()
        {
        }

        public int Code { get; set; }

        public string Reason { get; set; }

        public HttpVersion Version { get; private set; }

        public static implicit operator string(HttpStatusLine obj)
        {
            return ReferenceEquals(null, obj) ? null : obj.ToString();
        }

        public static HttpStatusLine FromString(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            var parts = value.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToQueue();
            var version = parts.Peek().StartsWith("HTTP/", StringComparison.Ordinal)
                              ? (HttpVersion)parts.Dequeue()
                              : HttpVersion.Latest;

            if (0 == parts.Count)
            {
                throw new FormatException();
            }

            var code = XmlConvert.ToInt32(parts.Dequeue());
            if (0 == parts.Count)
            {
                return new HttpStatusLine(version, (HttpStatusCode)code);
            }

            var reason = parts.Aggregate(string.Empty, 
                                         (current, 
                                          part) => current + (" " + part));

            return new HttpStatusLine(version, code, reason.Trim());
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Reducing complexity here is not practical.")]
        public static string ReasonPhase(HttpStatusCode code)
        {
            switch (code)
            {
                case HttpStatusCode.SwitchingProtocols:
                    return "Switching Protocols";

                case HttpStatusCode.NonAuthoritativeInformation:
                    return "Non-Authoritative Information";

                case HttpStatusCode.NoContent:
                    return "No Content";

                case HttpStatusCode.ResetContent:
                    return "Reset Content";

                case HttpStatusCode.PartialContent:
                    return "Partial Content";

                case HttpStatusCode.MultipleChoices:
                    return "Multiple Choices";

                case HttpStatusCode.MovedPermanently:
                    return "Moved Permanently";

                case HttpStatusCode.SeeOther:
                    return "See Other";

                case HttpStatusCode.NotModified:
                    return "Not Modified";

                case HttpStatusCode.UseProxy:
                    return "Use Proxy";

                case HttpStatusCode.TemporaryRedirect:
                    return "Temporary Redirect";

                case HttpStatusCode.BadRequest:
                    return "Bad Request";

                case HttpStatusCode.PaymentRequired:
                    return "Payment Required";

                case HttpStatusCode.NotFound:
                    return "Not Found";

                case HttpStatusCode.MethodNotAllowed:
                    return "Method Not Allowed";

                case HttpStatusCode.NotAcceptable:
                    return "Not Acceptable";

                case HttpStatusCode.ProxyAuthenticationRequired:
                    return "Proxy Authentication Required";

                case HttpStatusCode.RequestTimeout:
                    return "Request Time-out";

                case HttpStatusCode.LengthRequired:
                    return "Length Required";

                case HttpStatusCode.PreconditionFailed:
                    return "Precondition Failed";

                case HttpStatusCode.RequestEntityTooLarge:
                    return "Request Entity Too Large";

                case HttpStatusCode.RequestUriTooLong:
                    return "Request-URI Too Large";

                case HttpStatusCode.UnsupportedMediaType:
                    return "Unsupported Media Type";

                case HttpStatusCode.RequestedRangeNotSatisfiable:
                    return "Requested range not satisfiable";

                case HttpStatusCode.ExpectationFailed:
                    return "Expectation Failed";

                case HttpStatusCode.InternalServerError:
                    return "Internal Server Error";

                case HttpStatusCode.NotImplemented:
                    return "Not Implemented";

                case HttpStatusCode.BadGateway:
                    return "Bad Gateway";

                case HttpStatusCode.ServiceUnavailable:
                    return "Service Unavailable";

                case HttpStatusCode.GatewayTimeout:
                    return "Gateway Time-out";

                case HttpStatusCode.HttpVersionNotSupported:
                    return "HTTP Version not supported";

                case HttpStatusCode.Continue:
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                case HttpStatusCode.Accepted:
                case HttpStatusCode.Found:
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                case HttpStatusCode.Conflict:
                case HttpStatusCode.Gone:
                    return code.ToString("G");

                default:
                    return null;
            }
        }

        public override string ToString()
        {
            return "{0} {1} {2}".FormatWith(Version, Code, Reason);
        }
    }
}