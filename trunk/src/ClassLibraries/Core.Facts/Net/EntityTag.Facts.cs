﻿namespace Cavity.Net
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using Cavity.Security.Cryptography;
    using Xunit;

    public sealed class EntityTagFacts
    {
        private const string EmptyEtag = "\"1B2M2Y8AsgTpgAmY7PhCfg==\"";

        private const string JigsawEtag = "\"0TMnkhCZtrIjdTtJk6x3+Q==\"";

        private const string NullEtag = "\"\"";

        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<EntityTag>()
                            .IsValueType()
                            .Implements<ISerializable>()
                            .Implements<IComparable>()
                            .Implements<IComparable<EntityTag>>()
                            .Implements<IEquatable<EntityTag>>()
                            .Serializable()
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
            EntityTag expected = JigsawEtag;
            EntityTag actual;

            using (Stream stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, (EntityTag)JigsawEtag);
                stream.Position = 0;
                actual = (EntityTag)formatter.Deserialize(stream);
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new EntityTag(JigsawEtag));
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
            Assert.NotNull(new EntityTag(NullEtag));
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
        public void opGreater_EntityTag_EntityTag()
        {
            EntityTag jigsaw = JigsawEtag;
            EntityTag empty = EmptyEtag;

            Assert.True(empty > jigsaw);
        }

        [Fact]
        public void opImplicit_EntityTag_MD5Hash()
        {
            EntityTag expected = EmptyEtag;
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
        public void opLesser_EntityTag_EntityTag()
        {
            EntityTag jigsaw = JigsawEtag;
            EntityTag empty = EmptyEtag;

            Assert.True(jigsaw < empty);
        }

        [Fact]
        public void op_CompareTo_EntityTag()
        {
            EntityTag empty = EmptyEtag;
            EntityTag jigsaw = JigsawEtag;

            const long expected = -1;
            var actual = jigsaw.CompareTo(empty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_object()
        {
            EntityTag jigsaw = JigsawEtag;
            object empty = (EntityTag)EmptyEtag;

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
            object value = null;

            Assert.Throws<ArgumentNullException>(() => new EntityTag().CompareTo(value));
        }

        [Fact]
        public void op_Compare_EntityTagEmpty_EntityTagJigsaw()
        {
            const long expected = 1;
            var actual = EntityTag.Compare(EmptyEtag, JigsawEtag);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compare_EntityTagJigsaw_EntityTagEmpty()
        {
            const long expected = -1;
            var actual = EntityTag.Compare(JigsawEtag, EmptyEtag);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compare_EntityTag_EntityTag()
        {
            const long expected = 0;
            var actual = EntityTag.Compare(JigsawEtag, JigsawEtag);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Equals_EntityTag()
        {
            EntityTag obj = NullEtag;

            Assert.True(new EntityTag().Equals(obj));
        }

        [Fact]
        public void op_Equals_object()
        {
            object obj = (EntityTag)NullEtag;

            Assert.True(new EntityTag().Equals(obj));
        }

        [Fact]
        public void op_Equals_objectDiffers()
        {
            EntityTag obj = JigsawEtag;

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
            object other = null;

            Assert.False(new EntityTag().Equals(other));
        }

        [Fact]
        public void op_GetHashCode()
        {
            var expected = JigsawEtag.GetHashCode();
            var actual = new EntityTag(JigsawEtag).GetHashCode();

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

            ISerializable value = (EntityTag)JigsawEtag;

            Assert.Throws<ArgumentNullException>(() => value.GetObjectData(null, context));
        }

        [Fact]
        public void op_GetObjectData_SerializationInfo_StreamingContext()
        {
            var info = new SerializationInfo(typeof(EntityTag), new FormatterConverter());
            var context = new StreamingContext(StreamingContextStates.All);

            const string expected = JigsawEtag;

            ISerializable value = (EntityTag)JigsawEtag;

            value.GetObjectData(info, context);

            var actual = info.GetString("_value");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = JigsawEtag;
            var actual = new EntityTag(JigsawEtag).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_whenDefault()
        {
            const string expected = NullEtag;
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