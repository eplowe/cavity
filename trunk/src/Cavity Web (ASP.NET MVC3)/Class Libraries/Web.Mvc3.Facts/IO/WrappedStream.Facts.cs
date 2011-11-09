namespace Cavity.IO
{
    using System;
    using System.IO;
    using System.Text;
    using System.Web;
    using Moq;
    using Xunit;

    public sealed class WrappedStreamFacts
    {
        private const string EmptyHash = "1B2M2Y8AsgTpgAmY7PhCfg==";

        ////private const string JigsawHash = "0TMnkhCZtrIjdTtJk6x3+Q==";

        //// http://jigsaw.w3.org/HTTP/h-content-md5.html
        private const string JigsawHtml = "<HTML>\n"
                                          + "<HEAD>\n  <!-- Created with 'cat' and 'vi'  -->\n"
                                          + "<TITLE>Retry-After header</TITLE>\n"
                                          + "</HEAD>\n"
                                          + "<BODY>\n"
                                          + "<P>\n"
                                          + "<A HREF=\"..\"><IMG SRC=\"/icons/jigsaw\" ALT=\"Jigsaw\" BORDER=\"0\" WIDTH=\"212\"\n    HEIGHT=\"49\"></A>\n"
                                          + "<H1>\nThe <I>Content-MD5</I> header\n</H1>\n"
                                          + "<P>This pages is served along with its MD5 digest, you take\na look at the headers, as it is quite difficult to do an auto-referent\npage about its md5 signature :)\n"
                                          + "</P>\n  <HR>\n<BR>\n"
                                          + "<A HREF=\"mailto:jigsaw@w3.org\">jigsaw@w3.org</A>\n"
                                          + "</BODY></HTML>\n \n";

        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<WrappedStream>()
                            .DerivesFrom<Stream>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_Stream()
        {
            using (var stream = new MemoryStream())
            {
                Assert.NotNull(new WrappedStream(stream));
            }
        }

        [Fact]
        public void ctor_StreamNull()
        {
            Assert.Throws<ArgumentNullException>(() => new WrappedStream(null));
        }

        [Fact]
        public void op_Flush()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write("a");
                    var obj = new WrappedStream(stream);
                    obj.Flush();
                    Assert.Equal(0, obj.Length);
                }
            }
        }

        [Fact]
        public void op_Read_bytes_int_int()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write("abc");
                    writer.Flush();
                    stream.Position = 0;

                    var buffer = new byte[1];
                    Assert.Equal(1, new WrappedStream(stream).Read(buffer, 0, 1));
                    Assert.Equal((byte)'a', buffer[0]);
                }
            }
        }

        [Fact]
        public void op_Seek_long_SeekOrigin()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write("abc");
                    writer.Flush();
                    stream.Position = 0;

                    Assert.Equal(1, new WrappedStream(stream).Seek(1, SeekOrigin.Current));
                }
            }
        }

        [Fact]
        public void op_SetLength_long()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write("abc");
                    writer.Flush();
                    stream.Position = 0;

                    var obj = new WrappedStream(stream);
                    obj.SetLength(10);
                    Assert.Equal(10, obj.Length);
                }
            }
        }

        [Fact]
        public void op_Write_bytes_int_int()
        {
            try
            {
                const string expected = "<title>Test</title>";
                string actual;

                var encoding = Encoding.UTF8;

                var response = new Mock<HttpResponseBase>(MockBehavior.Strict);
                response
                    .Setup(x => x.ContentEncoding)
                    .Returns(encoding)
                    .Verifiable();
                response
                    .Setup(x => x.ContentType)
                    .Returns("text/html")
                    .Verifiable();

                var context = new Mock<HttpContextBase>(MockBehavior.Strict);
                context
                    .Setup(x => x.Response)
                    .Returns(response.Object)
                    .Verifiable();

                Static<HttpContextBase>.Instance = context.Object;

                using (var stream = new MemoryStream())
                {
                    var obj = new WrappedStream(stream);

                    var bytes = encoding.GetBytes("<title>Test</title>");
                    obj.Write(bytes, 0, bytes.Length);

                    bytes = encoding.GetBytes("</title>");
                    obj.Write(bytes, 0, bytes.Length);

                    stream.Position = 0;

                    var raw = new byte[expected.Length];
                    obj.Read(raw, 0, expected.Length);

                    actual = encoding.GetString(raw);
                }

                Assert.Equal(expected, actual);

                context.VerifyAll();
            }
            finally
            {
                Static<HttpContextBase>.Reset();
            }
        }

        [Fact]
        public void prop_CanRead_get()
        {
            using (var stream = new MemoryStream())
            {
                Assert.Equal(stream.CanRead, new WrappedStream(stream).CanRead);
            }
        }

        [Fact]
        public void prop_CanSeek_get()
        {
            using (var stream = new MemoryStream())
            {
                Assert.Equal(stream.CanSeek, new WrappedStream(stream).CanSeek);
            }
        }

        [Fact]
        public void prop_CanWrite_get()
        {
            using (var stream = new MemoryStream())
            {
                Assert.Equal(stream.CanWrite, new WrappedStream(stream).CanWrite);
            }
        }

        [Fact]
        public void prop_ContentMD5_get()
        {
            const string expected = EmptyHash;
            string actual;

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(JigsawHtml);
                    writer.Flush();
                    stream.Position = 0;

                    actual = new WrappedStream(stream).ContentMD5;
                }
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_ContentMD5_getEmpty()
        {
            const string expected = EmptyHash;
            string actual;

            using (var stream = new MemoryStream())
            {
                actual = new WrappedStream(stream).ContentMD5;
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Length_get()
        {
            using (var stream = new MemoryStream())
            {
                Assert.Equal(stream.Length, new WrappedStream(stream).Length);
            }
        }

        [Fact]
        public void prop_Position_get()
        {
            using (var stream = new MemoryStream())
            {
                Assert.Equal(stream.Position, new WrappedStream(stream).Position);
            }
        }

        [Fact]
        public void prop_Position_set()
        {
            using (var stream = new MemoryStream())
            {
                var obj = new WrappedStream(stream)
                {
                    Position = 0
                };

                Assert.Equal(stream.Position, obj.Position);
            }
        }
    }
}