namespace Cavity.Tests
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using Xunit;

    public sealed class UnitTestExceptionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<UnitTestException>()
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
            Assert.NotNull(new UnitTestException());
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            var expected = new UnitTestException("test");
            UnitTestException actual;

            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new UnitTestException("test"));
                stream.Position = 0;
                actual = (UnitTestException)formatter.Deserialize(stream);
            }

            Assert.Equal(expected.Message, actual.Message);
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new UnitTestException("message"));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.NotNull(new UnitTestException(string.Empty));
        }

        [Fact]
        public void ctor_stringEmpty_ExceptionNull()
        {
            Assert.NotNull(new UnitTestException(string.Empty, null));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.NotNull(new UnitTestException(null));
        }

        [Fact]
        public void ctor_stringNull_ExceptionNull()
        {
            Assert.NotNull(new UnitTestException(null, null));
        }

        [Fact]
        public void ctor_string_Exception()
        {
            Assert.NotNull(new UnitTestException("message", new Exception()));
        }

        [Fact]
        public void ctor_string_ExceptionNull()
        {
            Assert.NotNull(new UnitTestException("message", null));
        }
    }
}