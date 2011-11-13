namespace Cavity.Net
{
    using System;
    using System.IO;

    public sealed class TextBody : IHttpMessageBody
    {
        public TextBody(string text)
            : this()
        {
            Text = text;
        }

        private TextBody()
        {
        }

        public string Text { get; set; }

        public void Write(Stream stream)
        {
            if (null == stream)
            {
                throw new ArgumentNullException("stream");
            }

            throw new NotImplementedException();
        }
    }
}