namespace Cavity.Net
{
    using System.Collections.Generic;
    using Moq;
    using Xunit;

    public sealed class IHttpMessageHeadersFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IHttpMessageHeaders>()
                            .IsInterface()
                            .Implements<IEnumerable<HttpHeader>>()
                            .Result);
        }

        [Fact]
        public void index_string_get()
        {
            const string expected = "value";
            var mock = new Mock<IHttpMessageHeaders>();
            mock
                .Setup(x => x["name"])
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object["name"];

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void index_string_set()
        {
            var mock = new Mock<IHttpMessageHeaders>();
            mock
                .SetupSet(x => x["name"] = "value")
                .Verifiable();

            mock.Object["name"] = "value";

            mock.VerifyAll();
        }

        [Fact]
        public void op_Add_HttpHeader()
        {
            var header = new HttpHeader("name", "value");
            var mock = new Mock<IHttpMessageHeaders>();
            mock
                .Setup(x => x.Add(header))
                .Verifiable();

            mock.Object.Add(header);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Contains_Token()
        {
            Token name = "name";
            var mock = new Mock<IHttpMessageHeaders>();
            mock
                .Setup(x => x.Contains(name))
                .Verifiable();

            mock.Object.Contains(name);

            mock.VerifyAll();
        }
    }
}