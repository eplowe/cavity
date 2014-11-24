namespace Cavity.Net
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using Cavity.Security.Cryptography;
    using Xunit;

    public sealed class EntityTagFacts
    {
        private const string _emptyEtag = "\"1B2M2Y8AsgTpgAmY7PhCfg==\"";

        private const string _jigsawEtag = "\"0TMnkhCZtrIjdTtJk6x3+Q==\"";

        private const string _nullEtag = "\"\"";

        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<EntityTag>().IsValueType()
                                                         .Implements<ISerializable>()
                                                         .Implements<IComparable>()
                                                         .Implements<IComparable<EntityTag>>()
                                                         .Implements<IEquatable<EntityTag>>()
                                                         .Serializable()
                                                         .IsDecoratedWith<ImmutableObjectAttribute>()
                                                         .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new EntityTag());
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            EntityTag expected = _jigsawEtag;
            EntityTag actual;

            using (Stream stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, (EntityTag)_jigsawEtag);
                stream.Position = 0;
                actual = (EntityTag)formatter.Deserialize(stream);
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new EntityTag(_jigsawEtag));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.Throws<FormatException>(() => new EntityTag(string.Empty));
        }

        [Fact]
        public void ctor_stringMissingEndQuote()
        {
            Assert.Throws<FormatException>(() => new EntityTag("\"abc"));
        }

        [Fact]
        public void ctor_stringMissingStartQuote()
        {
            Assert.Throws<FormatException>(() => new EntityTag("abc\""));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.NotNull(new EntityTag(null));
        }

        [Fact]
        public void ctor_stringNullEtag()
        {
            Assert.NotNull(new EntityTag(_nullEtag));
        }

        [Fact]
        public void ctor_stringOnlyQuote()
        {
            Assert.Throws<FormatException>(() => new EntityTag("\""));
        }

        [Fact]
        public void ctor_stringOnlyWQuote()
        {
            Assert.Throws<FormatException>(() => new EntityTag("W/\""));
        }

        [Fact]
        public void ctor_stringWeakHash()
        {
            Assert.NotNull(new EntityTag("W/\"abc\""));
        }

        [Fact]
        public void opEquality_EntityTag_EntityTag()
        {
            var obj = new EntityTag();
            var comparand = new EntityTag();

            Assert.True(obj == comparand);
        }

        [Fact]
        public void opGreaterThan_EntityTag_EntityTag()
        {
            EntityTag jigsaw = _jigsawEtag;
            EntityTag empty = _emptyEtag;

            Assert.True(empty > jigsaw);
        }

        [Fact]
        public void opImplicit_EntityTag_MD5Hash()
        {
            EntityTag expected = _emptyEtag;
            EntityTag actual = MD5Hash.Compute(string.Empty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opInequality_EntityTag_EntityTag()
        {
            var obj = new EntityTag();
            var comparand = new EntityTag();

            Assert.False(obj != comparand);
        }

        [Fact]
        public void opLessThan_EntityTag_EntityTag()
        {
            EntityTag jigsaw = _jigsawEtag;
            EntityTag empty = _emptyEtag;

            Assert.True(jigsaw < empty);
        }

        [Fact]
        public void op_CompareTo_EntityTag()
        {
            EntityTag empty = _emptyEtag;
            EntityTag jigsaw = _jigsawEtag;

            const long expected = -1;
            var actual = jigsaw.CompareTo(empty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_object()
        {
            EntityTag jigsaw = _jigsawEtag;
            object empty = (EntityTag)_emptyEtag;

            const long expected = -1;
            var actual = jigsaw.CompareTo(empty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectInvalidCast()
        {
            var obj = new Uri("http://example.com/");

            Assert.Throws<InvalidCastException>(() => new EntityTag().CompareTo(obj));
        }

        [Fact]
        public void op_CompareTo_objectNull()
        {
            Assert.Throws<ArgumentNullException>(() => new EntityTag().CompareTo(null as object));
        }

        [Fact]
        public void op_Compare_EntityTagEmpty_EntityTagJigsaw()
        {
            const long expected = 1;
            var actual = EntityTag.Compare(_emptyEtag, _jigsawEtag);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compare_EntityTagJigsaw_EntityTagEmpty()
        {
            const long expected = -1;
            var actual = EntityTag.Compare(_jigsawEtag, _emptyEtag);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compare_EntityTag_EntityTag()
        {
            const long expected = 0;
            var actual = EntityTag.Compare(_jigsawEtag, _jigsawEtag);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Equals_EntityTag()
        {
            EntityTag obj = _nullEtag;

            Assert.True(new EntityTag().Equals(obj));
        }

        [Fact]
        public void op_Equals_object()
        {
            object obj = (EntityTag)_nullEtag;

            Assert.True(new EntityTag().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectDiffers()
        {
            EntityTag obj = _jigsawEtag;

            Assert.False(new EntityTag().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectInvalidCast()
        {
            var obj = new Uri("http://example.com/");

            Assert.Throws<InvalidCastException>(() => new EntityTag().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new EntityTag().Equals(null as object));
        }

        [Fact]
        public void op_GetHashCode()
        {
            var expected = _jigsawEtag.GetHashCode();
            var actual = new EntityTag(_jigsawEtag).GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetHashCode_whenDefault()
        {
            const int expected = 0;
            var actual = new EntityTag().GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetObjectData_SerializationInfoNull_StreamingContext()
        {
            var context = new StreamingContext(StreamingContextStates.All);

            ISerializable value = (EntityTag)_jigsawEtag;

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => value.GetObjectData(null, context));

            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Fact]
        public void op_GetObjectData_SerializationInfo_StreamingContext()
        {
            var info = new SerializationInfo(typeof(EntityTag), new FormatterConverter());
            var context = new StreamingContext(StreamingContextStates.All);

            const string expected = _jigsawEtag;

            ISerializable value = (EntityTag)_jigsawEtag;

            value.GetObjectData(info, context);

            var actual = info.GetString("_value");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = _jigsawEtag;
            var actual = new EntityTag(_jigsawEtag).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_whenDefault()
        {
            const string expected = _nullEtag;
            var actual = new EntityTag().ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Null()
        {
            var expected = new EntityTag();
            var actual = EntityTag.Null;

            Assert.Equal(expected, actual);
        }
    }
}