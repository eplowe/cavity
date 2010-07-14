namespace Cavity
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using Xunit;

    public sealed class TestExceptionFacts
    {
        [Fact]
        public void is_Exception()
        {
            Assert.IsAssignableFrom<Exception>(new TestException());
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new TestException());
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new TestException("message"));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.NotNull(new TestException(string.Empty));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.NotNull(new TestException(null));
        }

        [Fact]
        public void ctor_string_Exception()
        {
            Assert.NotNull(new TestException("message", new Exception()));
        }

        [Fact]
        public void ctor_string_ExceptionNull()
        {
            Assert.NotNull(new TestException("message", null));
        }

        [Fact]
        public void ctor_stringEmpty_ExceptionNull()
        {
            Assert.NotNull(new TestException(string.Empty, null));
        }

        [Fact]
        public void ctor_stringNull_ExceptionNull()
        {
            Assert.NotNull(new TestException(null, null));
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            var expected = new TestException("test");
            TestException actual;

            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new TestException("test"));
                stream.Position = 0;
                actual = (TestException)formatter.Deserialize(stream);
            }

            Assert.Equal(expected.Message, actual.Message);
        }
    }
}