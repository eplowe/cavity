namespace Cavity.Net
{
    public class HttpResponse : HttpMessage
    {
        public HttpStatusLine Line { get; set; }

        public static HttpResponse FromString(string value)
        {
            var result = new HttpResponse();
            result.Line = HttpStatusLine.FromString(result.Load(value));

            return result;
        }
    }
}