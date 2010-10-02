namespace Cavity.Net
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using Cavity;
    using Xunit;

    public sealed class HttpTestExceptionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpTestException>()
                .DerivesFrom<TestException>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .Serializable()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new HttpTestException());
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            var expected = new HttpTestException("test");
            HttpTestException actual;

            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new HttpTestException("test"));
                stream.Position = 0;
                actual = (HttpTestException)formatter.Deserialize(stream);
            }

            Assert.Equal(expected.Message, actual.Message);
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new HttpTestException("message"));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.NotNull(new HttpTestException(string.Empty));
        }

        [Fact]
        public void ctor_stringEmpty_ExceptionNull()
        {
            Assert.NotNull(new HttpTestException(string.Empty, null));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.NotNull(new HttpTestException(null));
        }

        [Fact]
        public void ctor_stringNull_ExceptionNull()
        {
            Assert.NotNull(new HttpTestException(null, null));
        }

        [Fact]
        public void ctor_string_Exception()
        {
            Assert.NotNull(new HttpTestException("message", new Exception()));
        }

        [Fact]
        public void ctor_string_ExceptionNull()
        {
            Assert.NotNull(new HttpTestException("message", null));
        }
    }
}