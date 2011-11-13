namespace Cavity.Net
{
    public class HttpRequest : HttpMessage
    {
        public HttpRequestLine Line { get; set; }

        public static HttpRequest FromString(string value)
        {
            var result = new HttpRequest();
            result.Line = HttpRequestLine.FromString(result.Load(value));

            return result;
        }
    }
}