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
        public void prop_Debug()
        {
            var settings = JsonWriterSettings.Debug;

            Assert.Equal(" ", settings.ColonPadding);
            Assert.Equal(Environment.NewLine, settings.CommaPadding);
            Assert.Equal("·", settings.Indent);
            Assert.Equal(string.Empty, settings.ItemsName);
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
        public void prop_ItemsName()
        {
            Assert.True(new PropertyExpectations<JsonWriterSettings>(x => x.ItemsName)
                            .IsNotDecorated()
                            .TypeIs<string>()
                            .DefaultValueIs(string.Empty)
                            .ArgumentNullException()
                            .Result);
        }

        [Fact]
        public void prop_Pretty()
        {
            var settings = JsonWriterSettings.Pretty;

            Assert.Equal(" ", settings.ColonPadding);
            Assert.Equal(Environment.NewLine, settings.CommaPadding);
            Assert.Equal("\t", settings.Indent);
            Assert.Equal(string.Empty, settings.ItemsName);
        }

        [Fact]
        public void prop_Terse()
        {
            var settings = JsonWriterSettings.Terse;

            Assert.Equal(string.Empty, settings.ColonPadding);
            Assert.Equal(string.Empty, settings.CommaPadding);
            Assert.Equal(string.Empty, settings.Indent);
            Assert.Equal(string.Empty, settings.ItemsName);
        }
    }
}