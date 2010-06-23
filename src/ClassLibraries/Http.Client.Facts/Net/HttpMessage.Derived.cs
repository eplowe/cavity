namespace Cavity.Net
{
    using System.IO;
    using Cavity.Net.Mime;

    public sealed class DerivedHttpMessage : HttpMessage
    {
        public DerivedHttpMessage()
            : base()
        {
        }

        public DerivedHttpMessage(HttpHeaderCollection headers, IContent body)
            : base(headers, body)
        {
        }

        public override void Read(StreamReader reader)
        {
            base.Read(reader);
        }
    }
}