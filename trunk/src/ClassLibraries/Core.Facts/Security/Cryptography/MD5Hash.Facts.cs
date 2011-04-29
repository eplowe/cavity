namespace Cavity.Security.Cryptography
{
    using System;
    using System.IO;
    using System.Net.Sockets;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using Xunit;

    public sealed class MD5HashFacts
    {
        private const string EmptyHash = "1B2M2Y8AsgTpgAmY7PhCfg==";

        private const string JigsawHash = "0TMnkhCZtrIjdTtJk6x3+Q==";

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

        private const string NullHash = "";

        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<MD5Hash>()
                            .IsValueType()
                            .Implements<ISerializable>()
                            .Implements<IComparable>()
                            .Implements<IComparable<MD5Hash>>()
                            .Implements<IEquatable<MD5Hash>>()
                            .Serializable()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new MD5Hash());
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            MD5Hash expected = Convert.FromBase64String(JigsawHash);
            MD5Hash actual;

            using (Stream stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, (MD5Hash)Convert.FromBase64String(JigsawHash));
                stream.Position = 0;
                actual = (MD5Hash)formatter.Deserialize(stream);
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_bytes()
        {
            Assert.NotNull(new MD5Hash(Convert.FromBase64String(JigsawHash)));
        }

        [Fact]
        public void ctor_bytesEmpty()
        {
            Assert.NotNull(new MD5Hash(new byte[]
            {
            }));
        }

        [Fact]
        public void ctor_bytesNull()
        {
            Assert.NotNull(new MD5Hash(null));
        }

        [Fact]
        public void opEquality_MD5Hash_MD5Hash()
        {
            var obj = new MD5Hash();
            var comparand = new MD5Hash();

            Assert.True(obj == comparand);
        }

        [Fact]
        public void opGreater_MD5Hash_MD5Hash()
        {
            MD5Hash jigsaw = Convert.FromBase64String(JigsawHash);
            MD5Hash empty = Convert.FromBase64String(EmptyHash);

            Assert.True(empty > jigsaw);
        }

        [Fact]
        public void opInequality_MD5Hash_MD5Hash()
        {
            var obj = new MD5Hash();
            var comparand = new MD5Hash();

            Assert.False(obj != comparand);
        }

        [Fact]
        public void opLesser_MD5Hash_MD5Hash()
        {
            MD5Hash jigsaw = Convert.FromBase64String(JigsawHash);
            MD5Hash empty = Convert.FromBase64String(EmptyHash);

            Assert.True(jigsaw < empty);
        }

        [Fact]
        public void op_CompareTo_MD5Hash()
        {
            MD5Hash empty = Convert.FromBase64String(EmptyHash);
            MD5Hash jigsaw = Convert.FromBase64String(JigsawHash);

            const long expected = -1;
            var actual = jigsaw.CompareTo(empty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_object()
        {
            MD5Hash jigsaw = Convert.FromBase64String(JigsawHash);
            object empty = (MD5Hash)Convert.FromBase64String(EmptyHash);

            const long expected = -1;
            var actual = jigsaw.CompareTo(empty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectInvalidCast()
        {
            var obj = new Uri("http://example.com/");

            Assert.Throws<InvalidCastException>(() => new MD5Hash().CompareTo(obj));
        }

        [Fact]
        public void op_CompareTo_objectNull()
        {
            object value = null;

            Assert.Throws<ArgumentNullException>(() => new MD5Hash().CompareTo(value));
        }

        [Fact]
        public void op_Compare_MD5HashEmpty_MD5HashJigsaw()
        {
            const long expected = 1;
            var actual = MD5Hash.Compare(
                Convert.FromBase64String(EmptyHash),
                Convert.FromBase64String(JigsawHash));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compare_MD5HashJigsaw_MD5HashEmpty()
        {
            const long expected = -1;
            var actual = MD5Hash.Compare(
                Convert.FromBase64String(JigsawHash),
                Convert.FromBase64String(EmptyHash));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compare_MD5Hash_MD5Hash()
        {
            const long expected = 0;
            var actual = MD5Hash.Compare(
                Convert.FromBase64String(JigsawHash),
                Convert.FromBase64String(JigsawHash));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compute_FileSystemInfo()
        {
            MD5Hash expected = Convert.FromBase64String(JigsawHash);
            var actual = MD5Hash.Compute(new FileInfo(@"Security\Cryptography\jigsaw.html"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compute_FileSystemInfoEmpty()
        {
            MD5Hash expected = Convert.FromBase64String(EmptyHash);
            var actual = MD5Hash.Compute(new FileInfo(@"Security\Cryptography\empty.html"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compute_FileSystemInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => MD5Hash.Compute(null as FileSystemInfo));
        }

        [Fact]
        public void op_Compute_Uri()
        {
            try
            {
                MD5Hash expected = Convert.FromBase64String(JigsawHash);
                var actual = MD5Hash.Compute(new Uri("http://jigsaw.w3.org/HTTP/h-content-md5.html"));

                Assert.Equal(expected, actual);
            }
            catch (SocketException)
            {
            }
        }

        [Fact]
        public void op_Compute_UriNull()
        {
            Assert.Throws<ArgumentNullException>(() => MD5Hash.Compute(null as Uri));
        }

        [Fact]
        public void op_Compute_byte()
        {
            MD5Hash expected = Convert.FromBase64String(JigsawHash);
            var actual = MD5Hash.Compute(Encoding.Default.GetBytes(JigsawHtml));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compute_byteEmpty()
        {
            MD5Hash expected = Convert.FromBase64String(EmptyHash);
            var actual = MD5Hash.Compute(new byte[]
            {
            });

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compute_byteNull()
        {
            Assert.Throws<ArgumentNullException>(() => MD5Hash.Compute(null as byte[]));
        }

        [Fact]
        public void op_Compute_stream()
        {
            MD5Hash expected = Convert.FromBase64String(JigsawHash);
            MD5Hash actual;

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(JigsawHtml);
                    writer.Flush();
                    stream.Position = 0;

                    actual = MD5Hash.Compute(stream);
                }
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compute_streamEmpty()
        {
            MD5Hash expected = Convert.FromBase64String(EmptyHash);
            MD5Hash actual;

            using (var stream = new MemoryStream())
            {
                actual = MD5Hash.Compute(stream);
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compute_streamNull()
        {
            Assert.Throws<ArgumentNullException>(() => MD5Hash.Compute(null as Stream));
        }

        [Fact]
        public void op_Compute_string()
        {
            MD5Hash expected = Convert.FromBase64String(JigsawHash);
            var actual = MD5Hash.Compute(JigsawHtml);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compute_stringEmpty()
        {
            MD5Hash expected = Convert.FromBase64String(EmptyHash);
            var actual = MD5Hash.Compute(string.Empty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compute_stringEmpty_Encoding()
        {
            MD5Hash expected = Convert.FromBase64String(EmptyHash);
            var actual = MD5Hash.Compute(string.Empty, Encoding.Default);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compute_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => MD5Hash.Compute(null as string));
        }

        [Fact]
        public void op_Compute_stringNull_Encoding()
        {
            Assert.Throws<ArgumentNullException>(() => MD5Hash.Compute(null, Encoding.Default));
        }

        [Fact]
        public void op_Compute_string_Encoding()
        {
            MD5Hash expected = Convert.FromBase64String(JigsawHash);
            var actual = MD5Hash.Compute(JigsawHtml, Encoding.Default);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compute_string_EncodingNull()
        {
            Assert.Throws<ArgumentNullException>(() => MD5Hash.Compute("test", null));
        }

        [Fact]
        public void op_Equals_MD5Hash()
        {
            MD5Hash obj = Convert.FromBase64String(NullHash);

            Assert.True(new MD5Hash().Equals(obj));
        }

        [Fact]
        public void op_Equals_object()
        {
            object obj = (MD5Hash)Convert.FromBase64String(NullHash);

            Assert.True(new MD5Hash().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectDiffers()
        {
            MD5Hash obj = Convert.FromBase64String(JigsawHash);

            Assert.False(new MD5Hash().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectInvalidCast()
        {
            var obj = new Uri("http://example.com/");

            Assert.Throws<InvalidCastException>(() => new MD5Hash().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            object other = null;

            Assert.False(new MD5Hash().Equals(other));
        }

        [Fact]
        public void op_GetHashCode()
        {
            var expected = JigsawHash.GetHashCode();
            var actual = new MD5Hash(Convert.FromBase64String(JigsawHash)).GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetHashCode_whenDefault()
        {
            const int expected = 0;
            var actual = new MD5Hash().GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetObjectData_SerializationInfoNull_StreamingContext()
        {
            var context = new StreamingContext(StreamingContextStates.All);

            ISerializable value = (MD5Hash)Convert.FromBase64String(JigsawHash);

            Assert.Throws<ArgumentNullException>(() => value.GetObjectData(null, context));
        }

        [Fact]
        public void op_GetObjectData_SerializationInfo_StreamingContext()
        {
            var info = new SerializationInfo(typeof(MD5Hash), new FormatterConverter());
            var context = new StreamingContext(StreamingContextStates.All);

            const string expected = JigsawHash;

            ISerializable value = (MD5Hash)Convert.FromBase64String(JigsawHash);

            value.GetObjectData(info, context);

            var actual = info.GetString("_value");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = JigsawHash;
            var actual = new MD5Hash(Convert.FromBase64String(JigsawHash)).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_whenDefault()
        {
            const string expected = NullHash;
            var actual = new MD5Hash().ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_whenEmptyBytes()
        {
            const string expected = NullHash;
            var actual = new MD5Hash(new byte[]
            {
            }).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Null()
        {
            var expected = new MD5Hash();
            var actual = MD5Hash.Null;

            Assert.Equal(expected, actual);
        }
    }
}