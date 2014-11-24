namespace Cavity.Globalization
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using Xunit;

    public sealed class TranslationExceptionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<TranslationException>().DerivesFrom<Exception>()
                                                                    .IsConcreteClass()
                                                                    .IsSealed()
                                                                    .HasDefaultConstructor()
                                                                    .Serializable()
                                                                    .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new TranslationException());
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            var expected = new TranslationException("test");
            TranslationException actual;

            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new TranslationException("test"));
                stream.Position = 0;
                actual = (TranslationException)formatter.Deserialize(stream);
            }

            Assert.Equal(expected.Message, actual.Message);
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new TranslationException("message"));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.NotNull(new TranslationException(string.Empty));
        }

        [Fact]
        public void ctor_stringEmpty_ExceptionNull()
        {
            Assert.NotNull(new TranslationException(string.Empty, null));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.NotNull(new TranslationException(null));
        }

        [Fact]
        public void ctor_stringNull_ExceptionNull()
        {
            Assert.NotNull(new TranslationException(null, null));
        }

        [Fact]
        public void ctor_string_Exception()
        {
            Assert.NotNull(new TranslationException("message", new InvalidOperationException()));
        }

        [Fact]
        public void ctor_string_ExceptionNull()
        {
            Assert.NotNull(new TranslationException("message", null));
        }
    }
}