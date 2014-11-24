namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class TextBodyFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<TextBody>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IHttpMessageBody>()
                            .Result);
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new TextBody("example"));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.NotNull(new TextBody(string.Empty));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.NotNull(new TextBody(null));
        }

        [Fact]
        public void op_Write_StreamNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TextBody("example").Write(null));
        }

        [Fact]
        public void prop_Text()
        {
            Assert.True(new PropertyExpectations<TextBody>(x => x.Text)
                            .IsAutoProperty<string>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}