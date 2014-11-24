namespace Cavity.Text
{
    using System;
    using System.Net.Mime;
    using System.Text;

    public static class EncodingExtensionMethods
    {
#if NET20
        public static ContentType ToContentType(Encoding encoding, 
                                                string type)
#else
        public static ContentType ToContentType(this Encoding encoding,
                                                string type)
#endif
        {
            if (null == type)
            {
                throw new ArgumentNullException("type");
            }

            if (0 == type.Length)
            {
                throw new ArgumentOutOfRangeException("type");
            }

            return new ContentType(string.Concat(type, (null == encoding) ? null : "; charset=" + encoding.WebName));
        }
    }
}