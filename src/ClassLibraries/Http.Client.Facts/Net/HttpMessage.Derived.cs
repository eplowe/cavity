namespace Cavity.Net
{
    using System.IO;

    public sealed class DerivedHttpMessage : HttpMessage
    {
        public override void Read(TextReader reader)
        {
            base.Read(reader);
        }
    }
}