namespace Cavity.Data
{
    using System.IO;
    using Moq;
    using Xunit;

    public sealed class IJsonSerializableFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IJsonSerializable>()
                            .IsInterface()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void op_ReadJson_JsonReader()
        {
            using (var stream = new MemoryStream())
            {
                using (var reader = new JsonReader(stream))
                {
                    // ReSharper disable AccessToDisposedClosure
                    var mock = new Mock<IJsonSerializable>();
                    mock
                        .Setup(x => x.ReadJson(reader))
                        .Verifiable();

                    // ReSharper restore AccessToDisposedClosure
                    mock.Object.ReadJson(reader);

                    mock.VerifyAll();
                }
            }
        }

        [Fact]
        public void op_WriteJson_JsonWriter()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new JsonWriter(stream))
                {
                    // ReSharper disable AccessToDisposedClosure
                    var mock = new Mock<IJsonSerializable>();
                    mock
                        .Setup(x => x.WriteJson(writer))
                        .Verifiable();

                    // ReSharper restore AccessToDisposedClosure
                    mock.Object.WriteJson(writer);

                    mock.VerifyAll();
                }
            }
        }
    }
}