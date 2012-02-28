namespace Cavity.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Web;
    using Cavity.Security.Cryptography;

    public sealed class WrappedStream : Stream
    {
        private static readonly Regex _htmlWhitespace = new Regex(@"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\r\n])\s{2,}");

        public WrappedStream(Stream stream)
        {
            if (null == stream)
            {
                throw new ArgumentNullException("stream");
            }

            UnderlyingStream = stream;
            Bytes = new List<byte>();
        }

        public override bool CanRead
        {
            get
            {
                return UnderlyingStream.CanRead;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return UnderlyingStream.CanSeek;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return UnderlyingStream.CanWrite;
            }
        }

        public string ContentMD5
        {
            get
            {
                return MD5Hash.Compute(Bytes.ToArray());
            }
        }

        public override long Length
        {
            get
            {
                return UnderlyingStream.Length;
            }
        }

        public override long Position
        {
            get
            {
                return UnderlyingStream.Position;
            }

            set
            {
                UnderlyingStream.Position = value;
            }
        }

        public Stream UnderlyingStream { get; set; }

        private List<byte> Bytes { get; set; }

        public override void Flush()
        {
            UnderlyingStream.Flush();
            Bytes = new List<byte>();
        }

        public override int Read(byte[] buffer, 
                                 int offset, 
                                 int count)
        {
            return UnderlyingStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, 
                                  SeekOrigin origin)
        {
            return UnderlyingStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            UnderlyingStream.SetLength(value);
        }

        public override void Write(byte[] buffer, 
                                   int offset, 
                                   int count)
        {
            var context = Static<HttpContextBase>.Instance ?? new HttpContextWrapper(HttpContext.Current);
            var response = context.Response;
            var prefixes = new[]
            {
                "text/html", "application/xhtml+xml", "application/atom+xml", "application/rdf+xml"
            };
#if NET20
            if (StringExtensionMethods.StartsWithAny(response.ContentType, StringComparison.OrdinalIgnoreCase, prefixes))
#else
            if (response.ContentType.StartsWithAny(StringComparison.OrdinalIgnoreCase, prefixes))
#endif
            {
                buffer = response
                    .ContentEncoding
                    .GetBytes(_htmlWhitespace.Replace(response.ContentEncoding.GetString(buffer), string.Empty));
            }

            UnderlyingStream.Write(buffer, offset, buffer.Length);

            if (0 == offset)
            {
                Bytes.AddRange(buffer);
                return;
            }

            if (Bytes.Count == offset)
            {
                Bytes.AddRange(buffer);
                return;
            }

            for (var i = 0; i < buffer.Length; i++)
            {
                Bytes[offset + i] = buffer[i];
            }
        }
    }
}