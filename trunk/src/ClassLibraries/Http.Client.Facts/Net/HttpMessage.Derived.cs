namespace Cavity.Net
{
    using System.IO;

    public sealed class DerivedHttpMessage : HttpMessage
    {
        public override void Read(StreamReader reader)
        {
            base.Read(reader);
        }
    }
}