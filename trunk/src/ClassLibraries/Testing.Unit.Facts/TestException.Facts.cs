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
            Assert.NotNull(new TestException(null as string));
        }

        [Fact]
        public void ctor_string_Exception()
        {
            Assert.NotNull(new TestException("message", new Exception()));
        }

        [Fact]
        public void ctor_string_ExceptionNull()
        {
            Assert.NotNull(new TestException("message", null as Exception));
        }

        [Fact]
        public void ctor_stringEmpty_ExceptionNull()
        {
            Assert.NotNull(new TestException(string.Empty, null as Exception));
        }

        [Fact]
        public void ctor_stringNull_ExceptionNull()
        {
            Assert.NotNull(new TestException(null as string, null as Exception));
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            TestException expected = new TestException("test");
            TestException actual = null;

            using (var stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, new TestException("test"));
                stream.Position = 0; /// Reset stream position
                actual = (TestException)formatter.Deserialize(stream);
            }

            Assert.Equal<string>(expected.Message, actual.Message);
        }
    }
}