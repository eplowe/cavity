namespace Cavity.Xml.Serialization
{
    using Xunit;

    public sealed class IXmlSerializableCommandFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IXmlSerializableCommand>()
                            .IsInterface()
                            .Implements<ICommand>()
                            .Result);
        }
    }
}