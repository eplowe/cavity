namespace Cavity.Net
{
    using Cavity.Net.Mime;

    public sealed class DerivedHttpMessage : HttpMessage
    {
        public DerivedHttpMessage(IHttpHeaderCollection headers, IContent body)
            : base(headers, body)
        {
        }

        private DerivedHttpMessage()
            : base()
        {
        }
    }
}