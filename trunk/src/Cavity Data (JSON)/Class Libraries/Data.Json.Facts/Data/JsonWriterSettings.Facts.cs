namespace Cavity.Data
{
    using System;

    using Xunit;

    public sealed class JsonWriterSettingsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<JsonWriterSettings>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new JsonWriterSettings());
        }

        [Fact]
        public void prop_ColonPadding()
        {
            Assert.True(new PropertyExpectations<JsonWriterSettings>(x => x.ColonPadding)
                            .IsNotDecorated()
                            .TypeIs<string>()
                            .DefaultValueIs(string.Empty)
                            .ArgumentNullException()
                            .FormatException("_")
                            .Set(string.Empty)
                            .Set(" ")
                            .Set(Environment.NewLine)
                            .Set("\t")
                            .Result);
        }

        [Fact]
        public void prop_CommaPadding()
        {
            Assert.True(new PropertyExpectations<JsonWriterSettings>(x => x.CommaPadding)
                            .IsNotDecorated()
                            .TypeIs<string>()
                            .DefaultValueIs(string.Empty)
                            .ArgumentNullException()
                            .FormatException("_")
                            .Set(string.Empty)
                            .Set(" ")
                            .Set(Environment.NewLine)
                            .Set("\t")
                            .Result);
        }

        [Fact]
        public void prop_EncodeValues()
        {
            Assert.True(new PropertyExpectations<JsonWriterSettings>(x => x.EncodeValues)
                            .IsAutoProperty(true)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Indent()
        {
            Assert.True(new PropertyExpectations<JsonWriterSettings>(x => x.Indent)
                            .IsNotDecorated()
                            .TypeIs<string>()
                            .DefaultValueIs(string.Empty)
                            .ArgumentNullException()
                            .FormatException("_")
                            .Set(string.Empty)
                            .Set(" ")
                            .Set(Environment.NewLine)
                            .Set("\t")
                            .Result);
        }

        [Fact]
        public void prop_Pretty()
        {
            var settings = JsonWriterSettings.Pretty;

            Assert.Equal(" ", settings.ColonPadding);
            Assert.Equal(Environment.NewLine, settings.CommaPadding);
            Assert.False(settings.EncodeValues);
            Assert.Equal("\t", settings.Indent);
        }
    }
}