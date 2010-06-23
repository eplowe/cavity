namespace Cavity.Net.Mime
{
    using System.Net.Mime;

    public sealed class DerivedText : Text
    {
        public DerivedText(ContentType contentType, string value)
            : base(contentType, value)
        {
        }

        private DerivedText()
        {
        }
    }
}