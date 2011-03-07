namespace Cavity.Data
{
    using System;
    using Cavity.Net;
    using Moq;
    using Xunit;

    public sealed class IRecordFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IRecord>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void prop_Cacheability_get()
        {
            var expected = CacheControl.Public;

            var mock = new Mock<IRecord>();
            mock
                .SetupGet(x => x.Cacheability)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Cacheability;

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Cacheability_set()
        {
            var value = CacheControl.Public;

            var mock = new Mock<IRecord>();
            mock
                .SetupSet(x => x.Cacheability = value)
                .Verifiable();

            mock.Object.Cacheability = value;

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Created_get()
        {
            var expected = DateTime.UtcNow;

            var mock = new Mock<IRecord>();
            mock
                .SetupGet(x => x.Created)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Created;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Created_set()
        {
            var value = DateTime.UtcNow;

            var mock = new Mock<IRecord>();
            mock
                .SetupSet(x => x.Created = value)
                .Verifiable();

            mock.Object.Created = value;

            mock.VerifyAll();
        }
    }
}