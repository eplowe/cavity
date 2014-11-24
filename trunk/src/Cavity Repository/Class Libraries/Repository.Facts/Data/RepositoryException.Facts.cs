namespace Cavity.Data
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using Xunit;

    public sealed class RepositoryExceptionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryException>()
                            .DerivesFrom<Exception>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .Serializable()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new RepositoryException());
        }

        [Fact]
        public void ctor_Exception()
        {
            Assert.NotNull(new RepositoryException(new InvalidOperationException()));
        }

        [Fact]
        public void ctor_ExceptionNull()
        {
            Assert.NotNull(new RepositoryException(null as Exception));
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            var expected = new RepositoryException("test");
            RepositoryException actual;

            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new RepositoryException("test"));
                stream.Position = 0;
                actual = (RepositoryException)formatter.Deserialize(stream);
            }

            Assert.Equal(expected.Message, actual.Message);
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new RepositoryException("message"));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.NotNull(new RepositoryException(string.Empty));
        }

        [Fact]
        public void ctor_stringEmpty_Exception()
        {
            Assert.NotNull(new RepositoryException(string.Empty, new InvalidOperationException()));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.NotNull(new RepositoryException(null as string));
        }

        [Fact]
        public void ctor_stringNull_Exception()
        {
            Assert.NotNull(new RepositoryException(null, new InvalidOperationException()));
        }

        [Fact]
        public void ctor_string_Exception()
        {
            Assert.NotNull(new RepositoryException("message", new InvalidOperationException()));
        }

        [Fact]
        public void ctor_string_ExceptionNull()
        {
            Assert.NotNull(new RepositoryException("message", null));
        }
    }
}